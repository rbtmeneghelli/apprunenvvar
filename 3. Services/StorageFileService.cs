using AppRunEnvVar._3._Services._3._1_Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRunEnvVar._3._Services
{
    public class StorageFileService : GenericStorage
    {
        public override Task<bool> EnvVarExist(string fileName)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> GetEnvVar(string fileName)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> SetEnvVar(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
