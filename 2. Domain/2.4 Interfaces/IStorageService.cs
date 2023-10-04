using AppRunEnvVar._3._Services;

namespace AppRunEnvVar._2._Domain._2._4_Interfaces;

public interface IStorageService : IDisposable
{
    StorageAzureService GetStorageAzureService();
    StorageFileService GetStorageFileService();
}
