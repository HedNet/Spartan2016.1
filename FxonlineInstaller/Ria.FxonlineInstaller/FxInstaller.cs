using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Ria.FxonlineInstaller
{
    public partial class FxInstaller : Form
    {
        private IncompatibleApps.IncompatibleAppCollection iApps;
        private LibrariesInstaller.LibraryInstallerCollection lInst;
        private RegisterEdit.RegisterEditCollection reList;
        private string RiaShortcuts;

        public FxInstaller()
        {
            InitializeComponent();

            /* Initialize installer */
            // System.Security.Permissions.FileIOPermission;
            RiaShortcuts = System.IO.Directory.GetCurrentDirectory() + "\\RiaShortcuts.vbs";
            File.WriteAllText(RiaShortcuts, String.Format(Properties.Resources.RiaShortcuts, System.IO.Directory.GetCurrentDirectory()));

            using (FileStream ms = File.Create("logmein.ico"))
            { Properties.Resources.logmein.Save(ms); }

            using (FileStream ms = File.Create("ria.ico"))
            { Properties.Resources.ria.Save(ms); }

            InitializeIncompatibleApps();   // Initializes the detection of all incompatible software
            InitializeLibraryInstaller();   // Initializes application installers like Java or FoxIt
            InitializeRegEdit();            // Initializes the Registry process
            InitializeProgressBar();        // Initializes the installer progress bar
            this.Visible = true;
            /* Start installer */
            InitializeInstallation();       // Starts installation
            timer1.Enabled = true;
        }

        private void InitializeInstallation()
        {
            if (MessageBox.Show("FX-Online installer is about to start. Would you like to continue?",
                "FX-Online Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            { return; }
            foreach (AppInfo ai in this.iApps)
            {
                label1.Text = String.Format("Checking for {0}", ai.ProcessName);
                Application.DoEvents();
                iApps.IncompatibleApp(ai.ProcessName, ai.ProcessCaption, ai.RebootNeeded);
                progressBar1.Value++;
            }
            foreach (LibrariesInstaller.AbstractLibraryInstaller ali in this.lInst)
            {
                label1.Text = String.Format("Checking for {0} installation", ali.AppName);
                Application.DoEvents();
                ali.InitializeInstaller();
                progressBar1.Value++;
            }
            label1.Text = "Configuring internet ActiveX";
            foreach (RegisterEdit.RegisterInfo ri in this.reList)
            {
                Registry.SetValue(ri.KeyName, ri.ValueName, ri.value);
                progressBar1.Value++;
                Application.DoEvents();
            }
            createIcons();
            label1.Text = "Installation is finished...";
            MessageBox.Show("FX-Online Installer", "FX Online installation finished. Thank you for choosing Ria Money Transfer.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void createIcons()
        {
            System.Diagnostics.Process.Start("wscript", RiaShortcuts);            
        }

        private void InitializeProgressBar()
        {
            this.progressBar1.Maximum= this.iApps.Count + this.lInst.Count + this.reList.Count;
            this.progressBar1.Value = 0;
        }

        private void InitializeRegEdit()
        {
            this.reList = new RegisterEdit.RegisterEditCollection();

            
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\Currentversion\\Internet Settings\\ZoneMap\\Domains\\riaenvia.net", "*", 2, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\Currentversion\\Internet Settings\\ZoneMap\\Domains\\riaenvia.net", "*", 2, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\New Windows\\Allow", "*.riaenvia.net", 0, "REG_DWORD");

            // Turn on ActiveX options
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1001", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1004", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1200", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1201", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1208", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1209", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "120A", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "120B", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1405", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "2000", 0, "REG_DWORD");
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "2201", 0, "REG_DWORD");

            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1001", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1004", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1200", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1201", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1208", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1209", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "120A", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "120B", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1405", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "2000", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "2201", 0, "REG_DWORD");

            // Disable Pop-up blocker for Trusted Sites
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1809", 3, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\2", "1809", 3, "REG_DWORD");

            // Display Mixed content
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\3", "1609", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\3", "1609", 0, "REG_DWORD");

            // Dont save history
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Url History", "DaysToKeep", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Url History", "DaysToKeep", 0, "REG_DWORD");

            // Erase temp files
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Cache", "Persistent", 0, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Cache", "Persistent", 0, "REG_DWORD");

            // Pull from website every time
            this.reList.Add("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "SyncMode5", 3, "REG_DWORD");
            this.reList.Add("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "SyncMode5", 3, "REG_DWORD");

        }

        private void InitializeLibraryInstaller()
        {
            this.lInst = new LibrariesInstaller.LibraryInstallerCollection();

            /* iniciamos la lista de instaladores */

            lInst.Add(new LibrariesInstaller.IE9Installer(this.progressBar2));
            lInst.Add(new LibrariesInstaller.JavaInstaller(this.progressBar2));
            lInst.Add(new LibrariesInstaller.FoxitInstaller(this.progressBar2));
            lInst.Add(new LibrariesInstaller.ScriptXInstaller(this.progressBar2));
            lInst.Add(new LibrariesInstaller.YubikeyInstaller(this.progressBar2));

            /* Finalizamos la lista */
        }

        private void InitializeIncompatibleApps()
        {
            this.iApps = new IncompatibleApps.IncompatibleAppCollection();

            /* Iniciamos la lista de apps incompatibles */

            this.iApps.Add("DF5Serv", "Deep Freeze", true);

            /* Finalizamos la lista de apps */

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
