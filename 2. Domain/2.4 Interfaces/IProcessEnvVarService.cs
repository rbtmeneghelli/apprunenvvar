namespace AppRunEnvVar._2._Domain._2._4_Interfaces;

public interface IProcessEnvVarService : IDisposable
{
    public Task RunEnvVarOnLocalMachine();
}
