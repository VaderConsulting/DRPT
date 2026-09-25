using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISA.SystemCenter
{
    public class ManagementPackSealOptions
    {
        public bool SealManagementPack = false;
        public string ManagementPackSealCommandLine = "";
        public string KeyfileFilename = "";
        public string KeyCompanyName = "Your Organisation";
        public string PowerShellModulePath = "";
    }
}
