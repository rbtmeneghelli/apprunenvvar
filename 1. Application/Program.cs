using AppRunEnvVar._2._Domain._2._4_Interfaces;
using AppRunEnvVar._5._IoC;
using Microsoft.Extensions.DependencyInjection;

var services = AppConfiguration.ConfigureServices();
var processEnvVarService = services.GetService<IProcessEnvVarService>();
await processEnvVarService.RunEnvVarOnLocalMachine();