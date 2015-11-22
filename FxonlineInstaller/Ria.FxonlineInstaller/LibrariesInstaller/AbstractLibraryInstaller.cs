using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
        protected ProgressBar progressBar;

        public AbstractLibraryInstaller(ProgressBar progressBar)
        {
            // TODO: Complete member initialization
            this.progressBar = progressBar;
        }
        
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
            if (BeforeDownload())
            {
                if (!isInstalled())
                {
                    if (this.ForceInstall || MessageBox.Show(String.Format(MsgAppNotInstalled, AppName), AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Install((string)Download(this.AppURL));
                }
            }
            AfterInstall();
        }

        /// <summary> Actions to do befor installation</summary>
        protected abstract bool BeforeDownload();

        /// <summary> Actions to do befor executing installer</summary>
        protected virtual string BeforeExecuting(string path){return path;}

        /// <summary>Actions to do after installation</summary>
        protected abstract void AfterInstall();

        /// <summary>Installer execution</summary>
        /// <param name="p">Path to the installer app</param>
        protected void Install(string p)
        {
            p = BeforeExecuting(p);
            this.Exec(p, this.Arguments);
        }

        /// <summary>Executes an app</summary>
        /// <param name="executable">Path to the app</param>
        /// <param name="arguments">Arguments for app</param>
        protected void Exec(string executable, string arguments)
        {
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Visible = true;
            
            Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = executable;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.RedirectStandardOutput = false;
            proc.Start();
            while (!proc.HasExited) { Application.DoEvents(); }
            progressBar.Style = ProgressBarStyle.Blocks;
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
                    string fileName = "";
                    byte[] file = new byte[0];
                    bool isBusy = true;

                    this.progressBar.Visible = true;
                    this.progressBar.Value = 0;
                    this.progressBar.Minimum = 0;
                    this.progressBar.Maximum = 100;
                    int progressValue = 0;

                    wc.DownloadProgressChanged += (s, e) =>
                    {
                        progressValue = e.ProgressPercentage;
                    };
                    wc.DownloadDataCompleted += (s, e) =>
                    {                        
                        file = e.Result;

                        if (!String.IsNullOrEmpty(wc.ResponseHeaders["Content-Disposition"]))
                        {
                            fileName = wc.ResponseHeaders["Content-Disposition"].Substring(wc.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "");
                        }
                        else
                        {
                            fileName = "temp." + this.Extension;
                        }

                        File.WriteAllBytes(fileName, file);
                        isBusy = false;
                    };
                    wc.DownloadDataAsync(new Uri(p));
                    while (isBusy) { this.progressBar.Value = progressValue; Application.DoEvents(); }
                    this.progressBar.Visible = false;
                    return fileName;
                }
                catch (Exception e) { throw e; }
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        void DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>Checks if the app is installed</summary>
        /// <returns>Returns true if it is installed. False otherwise.</returns>
        protected virtual bool isInstalled()
        {
            object a = Microsoft.Win32.Registry.GetValue(this.RegistryLocalKey, "path", null);
            return this.ForceInstall ? a != null : false;
        }
        /// <summary>URL for app installer download</summary>
        public abstract string AppURL { get; }

        public  string Arguments { get; set; }

        public virtual bool ForceInstall { get { return false; } }

        public virtual string Extension { get { return "exe"; } }
    }
}
