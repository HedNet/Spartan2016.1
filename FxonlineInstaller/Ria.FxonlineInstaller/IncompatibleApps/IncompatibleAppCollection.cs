using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ria.FxonlineInstaller.IncompatibleApps
{
    public class IncompatibleAppCollection : IList<AppInfo>
    {
        private string IncompatibleMessage;
        private string IncompatibleMessageReboot;
        private string IncompatibleCaption;
        private List<AppInfo> AppsList = new List<AppInfo>();

        public int ProcessStatus { get; private set; }

        internal void IncompatibleApp(string process_name, string Caption, bool needs_reboot)
        {
            Process[] pname = Process.GetProcessesByName(process_name);
            if (pname.Length != 0)
            {
                MessageBox.Show(String.Format(
                    this.IncompatibleMessage, Caption, needs_reboot ? this.IncompatibleMessageReboot : ""),
                    String.Format(this.IncompatibleCaption, Caption),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        public void RunIncompatibleApps()
        {
            for (int i = 0; i < this.Count; i++)
            {
                ProcessStatus = i;
                AppInfo app = AppsList[i];
                IncompatibleApp(app.ProcessName, app.ProcessCaption, app.RebootNeeded);
            }
        }

        public void RunAsyncIncompatibleApps()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += delegate(object sender, DoWorkEventArgs e)
            { this.RunIncompatibleApps(); };

        }

        public int IndexOf(AppInfo item)
        { return AppsList.IndexOf(item); }

        public void Insert(int index, AppInfo item)
        { AppsList.Insert(index, item); }

        public void RemoveAt(int index)
        { AppsList.RemoveAt(index); }

        public AppInfo this[int index]
        {
            get { return AppsList[index]; }
            set { AppsList[index] = value; }
        }

        public void Add(AppInfo item)
        { AppsList.Add(item); }

        public void Clear()
        { AppsList.Clear(); }

        public bool Contains(AppInfo item)
        { return AppsList.Contains(item); }

        public void CopyTo(AppInfo[] array, int arrayIndex)
        { AppsList.CopyTo(array, arrayIndex); }

        public int Count
        { get { return AppsList.Count; } }

        public bool IsReadOnly
        { get { return false; } }

        public bool Remove(AppInfo item)
        { return AppsList.Remove(item); }

        public IEnumerator<AppInfo> GetEnumerator()
        { return AppsList.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.GetEnumerator(); }

        public void Add(string p1, string p2, bool p3)
        {
            AppInfo ai = new AppInfo(p1, p2, p3);
            this.Add(ai);
        }
    }
}

public class AppInfo
{
    public string ProcessName;
    public string ProcessCaption;
    public bool RebootNeeded;

    public AppInfo(string p1, string p2, bool p3)
    {
        this.ProcessName = p1;
        this.ProcessCaption = p2;
        this.RebootNeeded = p3;
    }
}