using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ria.FxonlineInstaller.LibrariesInstaller
{
    public class IE9Installer : AbstractLibraryInstaller
    {
        public override string AppName
        { get { return "Internet Explorer 9"; } }

        public override string RegistryLocalKey
        { get { return "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\IEXPLORE.EXE"; } }

        protected override bool BeforeInstall() { return true; }
        protected override void AfterInstall() { }

        public override string AppURL
        { get { return ""; } }
    }
}
