using AppRunEnvVar._3._Services._3._1_Generic;
using AppRunEnvVar._4._Infra._4._2_CrossCutting._4._2._1_AzureSettings;
using Azure.Storage.Files.Shares;
using Azure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunEnvVar._3._Services
{
    public sealed class StorageAzureService : GenericStorage
    {
        public override async Task<bool> EnvVarExist(string fileName)
        {
            return await AzureStorageClient.FileExist(fileName);
        }

        public override async Task<bool> GetEnvVar(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    IDictionary variablesFromMachine = Environment.GetEnvironmentVariables();

                    foreach (DictionaryEntry envVariable in variablesFromMachine)
                    {
                        writer.WriteLine($"{envVariable.Key}||{envVariable.Value}");
                    }
                }


                ShareServiceClient serviceClient = new ShareServiceClient(AzureConstants.ConnectionString);
                ShareClient shareClient = serviceClient.GetShareClient("documents");

                if (!await shareClient.ExistsAsync())
                {
                    await shareClient.CreateAsync();
                }

                ShareDirectoryClient directoryClient = shareClient.GetRootDirectoryClient();
                ShareFileClient fileClient = directoryClient.GetFileClient(fileName);

                using (FileStream stream = File.OpenRead(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName)))
                {
                    if (await fileClient.ExistsAsync() == false)
                    {
                        await fileClient.CreateAsync(stream.Length);
                    }

                    await fileClient.UploadRangeAsync(new HttpRange(0, stream.Length), stream);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override async Task<bool> SetEnvVar(string fileName)
        {
            try
            {
                var streamFile = await AzureStorageClient.DownloadFile(fileName);

                if (streamFile is null)
                    return false;

                using (StreamReader reader = new StreamReader(streamFile))
                {
                    string fileContent = reader.ReadToEnd();
                    string[] lines = fileContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split("||");

                        if (parts.Length == 2)
                        {
                            string key = parts[0];
                            string value = parts[1];
                            Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.User);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
