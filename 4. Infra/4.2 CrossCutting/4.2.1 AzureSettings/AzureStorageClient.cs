using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;

namespace AppRunEnvVar._4._Infra._4._2_CrossCutting._4._2._1_AzureSettings;

public sealed class AzureStorageClient
{
    private static CloudFileClient GetFileStorage()
    {    
        CloudStorageAccount storageAccount = new(new StorageCredentials(AzureConstants.AccountName, AzureConstants.KeyValue), true);
        return storageAccount.CreateCloudFileClient();
    }

    public static async Task<bool> FileExist(string fileName, string shareReference = "documents")
    {
        CloudFileShare fileShare = AzureStorageClient.GetFileStorage().GetShareReference(shareReference);

        if (await fileShare.ExistsAsync())
        {
            CloudFileDirectory rootDir = fileShare.GetRootDirectoryReference();
            CloudFile file = rootDir.GetFileReference(fileName);
            if (await file.ExistsAsync())
            {
                Console.WriteLine($"\n O arquivo {fileName} foi encontrado no storage do Azure com sucesso \n");
                return true;
            }
        }

        Console.WriteLine($"\n O arquivo {fileName} não foi encontrado no storage do Azure. \n");
        return true;
    }

    public static async Task<Stream> DownloadFile(string fileName, string shareReference = "documents")
    {
        try
        {
            CloudFileShare fileShare = AzureStorageClient.GetFileStorage().GetShareReference(shareReference);

            if (await fileShare.ExistsAsync())
            {
                CloudFileDirectory rootDir = fileShare.GetRootDirectoryReference();
                CloudFile file = rootDir.GetFileReference(fileName);
                if (await file.ExistsAsync())
                {
                    return await file.OpenReadAsync();
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
