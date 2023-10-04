using AppRunEnvVar._2._Domain._2._4_Interfaces;
using AppRunEnvVar._4._Infra._4._2_CrossCutting._4._2._1_AzureSettings;
using Azure;
using Azure.Storage.Files.Shares;
using System.Collections;

namespace AppRunEnvVar._3._Services;

public class ProcessEnvVarService : IProcessEnvVarService
{
    private readonly IStorageService _storageService;

    public ProcessEnvVarService(IStorageService storageService) 
    {
        _storageService = storageService;
    }

    private bool IsValidOption(int inputOption)
    {
        return inputOption > 0 && inputOption < 8;
    }

    private bool IsValidEnv(string env)
    {
        string[] _arrEnvs = { "dev", "qa", "hml", "hf", "prd" };

        if (string.IsNullOrWhiteSpace(env) == false)
        {
            if (_arrEnvs.Contains(env))
            {
                return true;
            }
        }

        Console.WriteLine($"Ambiente informado é invalido. Tente novamente! \n\n");
        return false;
    }

    private bool IsValidOptionAndEnv(string inputOption, string inputEnv)
    {
        if (int.TryParse(inputOption, out var _inputOption))
        {
            if (IsValidOption(_inputOption))
            {
                if (_inputOption != 4 && IsValidEnv(inputEnv))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        Console.WriteLine("Opção informada é invalida, tente novamente! \n\n");
        return false;
    }

    private string GetEnvVarFileName(string env)
    {
        return string.Concat("variaveis_ambiente_", env, ".txt");
    }

    private async Task<bool> OptionsToApply(int inputOption, string inputEnv)
    {
        bool result = true;

        switch (inputOption)
        {
            case 1:
                result = await _storageService.GetStorageAzureService().GetEnvVar(GetEnvVarFileName(inputEnv));
                break;
            case 2:
                result = await _storageService.GetStorageAzureService().EnvVarExist(GetEnvVarFileName(inputEnv));
                break;
            case 3:
                result = await _storageService.GetStorageAzureService().SetEnvVar(GetEnvVarFileName(inputEnv));
                break;
            case 7:
                result = _storageService.GetStorageAzureService().ExitRunEnvVarOnLocalMachine();
                break;
        }

        return result;
    }

    public async Task RunEnvVarOnLocalMachine()
    {
        bool isValidToRun = true;
        var repeatChar = string.Concat(Enumerable.Repeat("*", 96));

        Console.WriteLine(repeatChar);
        Console.WriteLine("Escolha uma das opções abaixo para configuração da variavel de ambiente em sua maquina local");
        Console.WriteLine("1 - Criar arquivo de configuração com variaveis de ambiente");
        Console.WriteLine("2 - Verificar se arquivo de configuração existe no diretorio");
        Console.WriteLine("3 - Aplicar configurações das variaveis de ambiente em minha maquina");
        Console.WriteLine("4 - Sair");
        Console.WriteLine(repeatChar + "\n\n");

        while (isValidToRun)
        {
            Console.Write("Insira uma das opções disponiveis na linha abaixo: \n");
            var inputOption = Console.ReadLine();
            Console.Write("Agora informe o ambiente(DEV, QA, HML, HF,PRD) do arquivo que será utilizado: \n");
            var inputEnv = Console.ReadLine();

            if (IsValidOptionAndEnv(inputOption, inputEnv))
            {
                isValidToRun = await OptionsToApply(int.Parse(inputOption), inputEnv);
            }
            else
            {
                isValidToRun = false;
            }
        }

        Environment.Exit(0);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
