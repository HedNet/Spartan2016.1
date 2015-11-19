using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ria.FxonlineInstaller.LibrariesInstaller
{
    public class FoxitInstaller : AbstractLibraryInstaller
    {
        public FoxitInstaller(ProgressBar progressBar) : base(progressBar) { }
        public override string AppName
        {
            get { return "Foxit 4.2"; }
        }

        public override string RegistryLocalKey
        {
            get { return "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\foxitreader.exe"; }
        }

        protected override bool BeforeDownload()
        {
            this.Arguments = "/disallow /dt /ql /fp /ebay /foxitbar";
            AcrobatReaderChecker arc = new AcrobatReaderChecker(this.progressBar);
            return !arc.check();
        }

        protected override void AfterInstall() { }

        public override string AppURL
        {
            get { return "https://github.com/HedNet/Spartan2016.1/raw/master/apis/FoxitReader42_enu_Setup.exe"; }
        }
    }

    public class AcrobatReaderChecker : AbstractLibraryInstaller
    {
        public AcrobatReaderChecker(ProgressBar progressBar) : base(progressBar) { }
        public override string AppName { get { return ""; } }

        public override string RegistryLocalKey { get { return "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\AcroRd32.exe"; } }

        protected override bool BeforeDownload()
        { return false; }

        protected override void AfterInstall() { }

        internal bool check() { return this.isInstalled(); }

        public override string AppURL
        { get { return "https://github.com/HedNet/Spartan2016.1/raw/master/apis/FoxitReader42_enu_Setup.exe"; } }
            
    }
}
