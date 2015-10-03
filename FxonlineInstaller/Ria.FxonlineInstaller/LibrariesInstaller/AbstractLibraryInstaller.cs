using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Ria.FxonlineInstaller.LibrariesInstaller
{
    /// <summary>
    /// Abstract class for library installation
    /// </summary>
    public abstract class AbstractLibraryInstaller
    {
        /// <summary>Installation total steps</summary>
        public int Steps { get; protected set; }

        /// <summary>Installation total steps</summary>
        public int Current { get; protected set; }

        // Resultado de la ejecución de una aplicación
        public Process Result;
        
        // Mensaje del installador
        private string MsgAppNotInstalled { get { return "{0} is not installed on your PC. Would you like to installed?"; } }

        /// <summary>
        /// Application name
        /// </summary>
        public abstract string AppName { get; }

        /// <summary>
        /// Application registration key
        /// </summary>
        public abstract string RegistryLocalKey { get; }

        /// <summary>
        /// Starts the installer
        /// </summary>
        public void InitializeInstaller()
        {
            if (BeforeInstall())
            {
                if (!isInstalled())
                {
                    if (MessageBox.Show(String.Format(MsgAppNotInstalled, AppName), AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Install((string)Download(this.AppURL));
                }
            }
            AfterInstall();
        }

        /// <summary> Actions to do befor installation</summary>
        protected abstract bool BeforeInstall();

        /// <summary>Actions to do after installation</summary>
        protected abstract void AfterInstall();

        /// <summary>Installer execution</summary>
        /// <param name="p">Path to the installer app</param>
        protected void Install(string p)
        {
            this.Exec(p, "");
        }

        /// <summary>Executes an app</summary>
        /// <param name="executable">Path to the app</param>
        /// <param name="arguments">Arguments for app</param>
        protected void Exec(string executable, string arguments)
        {
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = executable;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            while (!proc.HasExited) { Application.DoEvents(); }
            this.Result = proc;
        }

        /// <summary>Downloads a file from an URL</summary>
        /// <param name="p">URL string</param>
        /// <returns>downloaded file path</returns>
        protected string Download(string p)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.DownloadFile(p, "temp.exe");
                    return "temp.exe";
                }
                catch (Exception e) { throw e; }
            }
        }

        /// <summary>Checks if the app is installed</summary>
        /// <returns>Returns true if it is installed. False otherwise.</returns>
        protected virtual bool isInstalled()
        {
            object a = Microsoft.Win32.Registry.GetValue(this.RegistryLocalKey, "path", null);
            return a != null;
        }
        /// <summary>URL for app installer download</summary>
        public abstract string AppURL { get; }
    }
}
