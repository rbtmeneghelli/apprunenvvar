using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunEnvVar._3._Services._3._1_Generic;

public abstract class GenericStorage
{
    public abstract Task<bool> GetEnvVar(string fileName);
    public abstract Task<bool> EnvVarExist(string fileName);
    public abstract Task<bool> SetEnvVar(string fileName);
    public virtual bool ExitRunEnvVarOnLocalMachine()
    {
        Console.WriteLine($"Finalizando o processo de atualização das variaveis de ambiente na maquina {Environment.MachineName} \n");
        return false;
    }
}
