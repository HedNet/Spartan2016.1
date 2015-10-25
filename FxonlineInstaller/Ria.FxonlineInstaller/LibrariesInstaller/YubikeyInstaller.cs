using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ria.FxonlineInstaller.LibrariesInstaller
{
    class YubikeyInstaller : AbstractLibraryInstaller
    {
        public override bool ForceInstall { get { return true; } }

        public override string AppName
        {
            get { return "Active X"; }
        }

        public override string RegistryLocalKey 
        {
            get { return ""; }
        }

        public override string Extension { get { return "msi"; } }
        protected override bool BeforeDownload() { return true; }
        protected override string BeforeExecuting(string path)
        {
            this.Arguments = String.Format(" /qn /i {0}.msi", path);
            return "Msiexec.exe";
        }

        protected override void AfterInstall() { }

        public override string AppURL
        {
            get { return "https://github.com/HedNet/Spartan2016.1/raw/master/apis/Yubikey.exe"; }
        }
    }
}
