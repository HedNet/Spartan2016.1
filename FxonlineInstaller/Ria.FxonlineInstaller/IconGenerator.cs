using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ria.FxonlineInstaller
{
    public class IconGenerator
    {
        private string[] args=new string[4];
        public void Save()
        {
            string ps = String.Format("powershell '$WshShell = New-Object -comObject WScript.Shell;$Shortcut = $WshShell.CreateShortcut(\"{0}\");$Shortcut.TargetPath = \"{1}\";$Shortcut.Arguments =\"{2}\";$Shortcut.IconLocation = \"{3}\";$Shortcut.Save()'",
                this.args);
            System.Diagnostics.Process.Start(ps);
        }

        public string SavePath { get { return args[0]; } set { args[0] = value; } }
    
        public string TargetPath { get { return args[1]; } set { args[1] = value; } }

        public string Arguments { get { return args[2]; } set { args[2] = value; } }

        public string IconLocation { get { return args[3]; } set { args[3] = value; } }}
}
