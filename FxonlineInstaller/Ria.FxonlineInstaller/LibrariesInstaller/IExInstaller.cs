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

        protected override bool BeforeDownload() { return this.AppURL != null; }
        protected override void AfterInstall() { }

        public override string AppURL
        { get { return ieLink(); } }

        private string ieLink()
        {

            string IE8WinXP = "http://download.microsoft.com/download/C/C/0/CC0BD555-33DD-411E-936B-73AC6F95AE11/IE8-WindowsXP-x86-ENU.exe";
            // string IE8WinServer2003_32 = "http://download.microsoft.com/download/7/5/0/7507EBD5-0193-4B7F-9F14-9014C7EB5C67/IE8-WindowsServer2003-x86-ENU.exe";
            // string IE8WinServer2003_64 = "http://download.microsoft.com/download/7/5/4/754D6601-662D-4E39-9788-6F90D8E5C097/IE8-WindowsServer2003-x64-ENU.exe";
            // string IE8WinVista_32 = "http://download.microsoft.com/download/F/8/8/F88F09A2-A315-44C0-848E-48476A9E1577/IE8-WindowsVista-x86-ENU.exe";
            // string IE8WinVista_64 = "http://download.microsoft.com/download/D/C/F/DCF5DACB-313F-40C6-889C-AD1F8546099D/IE8-WindowsVista-x64-ENU.exe";
            string IE9WinVista_32 = "http://download.microsoft.com/download/0/8/7/08768091-35BC-48E0-9F7F-B9802A0EE2D6/IE9-WindowsVista-x86-enu.exe";
            string IE9WinVista_64 = "http://download.microsoft.com/download/7/C/3/7C3BA535-1D8C-4A87-9F1D-163BBA971CA9/IE9-WindowsVista-x64-enu.exe";
            // string IE8WinServer2008_32 = "http://download.microsoft.com/download/F/8/8/F88F09A2-A315-44C0-848E-48476A9E1577/IE8-WindowsVista-x86-ENU.exe";
            // string IE8WinServer2008_64 = "http://download.microsoft.com/download/D/C/F/DCF5DACB-313F-40C6-889C-AD1F8546099D/IE8-WindowsVista-x64-ENU.exe";
            // string IE9WinServer2008_32 = "http://download.microsoft.com/download/0/8/7/08768091-35BC-48E0-9F7F-B9802A0EE2D6/IE9-WindowsVista-x86-enu.exe";
            // string IE9WinServer2008_64 = "http://download.microsoft.com/download/7/C/3/7C3BA535-1D8C-4A87-9F1D-163BBA971CA9/IE9-WindowsVista-x64-enu.exe";
            string IE9Win7_32 = "http://download.microsoft.com/download/C/3/B/C3BF2EF4-E764-430C-BDCE-479F2142FC81/IE9-Windows7-x86-enu.exe";
            string IE9Win7_64 = "http://download.microsoft.com/download/C/1/6/C167B427-722E-4665-9A40-A37BC5222B0A/IE9-Windows7-x64-enu.exe";
            // string IE10Win7_32 = "http://download.microsoft.com/download/8/A/C/8AC7C482-BC74-492E-B978-7ED04900CEDE/IE10-Windows6.1-x86-en-us.exe";
            // string IE10Win7_64 = "http://download.microsoft.com/download/C/E/0/CE0AB8AE-E6B7-43F7-9290-F8EB0EA54FB5/IE10-Windows6.1-x64-en-us.exe";
            // string IE10WinServer2008_64 = "http://download.microsoft.com/download/C/E/0/CE0AB8AE-E6B7-43F7-9290-F8EB0EA54FB5/IE10-Windows6.1-x64-en-us.exe ";

            string result = null;

            SystemInfo si = new SystemInfo();

            string osVersion = Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor;

            switch (osVersion)
            {
                case "6.1": result = (si.CPU == "64") ? IE9Win7_64 : IE9Win7_32; break;      // ········Windows 7
                case "6.0": result = (si.CPU == "64") ? IE9WinVista_64 : IE9WinVista_32; break;      // Windows Vista
                case "5.2": result = IE8WinXP; break;      // ··········································Windows XP Pro 64
                case "5.1": result = IE8WinXP; break;      // ··········································Windows XP Home
            }

            return result;
        }
    }
}
