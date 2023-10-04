using AppRunEnvVar._2._Domain._2._4_Interfaces;

namespace AppRunEnvVar._3._Services;

public class StorageService : IStorageService
{
    private readonly StorageAzureService _storageAzureService;
    private readonly StorageFileService _storageFileService; 

    public StorageService()
    {
        _storageAzureService = new StorageAzureService();
        _storageFileService = new StorageFileService();
    }

    public StorageAzureService GetStorageAzureService() => _storageAzureService;
    public StorageFileService GetStorageFileService() => _storageFileService;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
