using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ria.FxonlineInstaller.RegisterEdit
{
    public class RegisterEditCollection : IList<RegisterInfo>
    {
        private List<RegisterInfo> list = new List<RegisterInfo>();
        public int IndexOf(RegisterInfo item)
        { return list.IndexOf(item); }

        public void Insert(int index, RegisterInfo item)
        { list.Insert(index, item); }

        public void RemoveAt(int index)
        { list.RemoveAt(index); }

        public RegisterInfo this[int index]
        {
            get { return this.list[index]; }
            set { list[index] = value; }
        }

        public void Add(RegisterInfo item)
        { list.Add(item); }

        public void Clear()
        { list.Clear(); }

        public bool Contains(RegisterInfo item)
        { return list.Contains(item); }

        public void CopyTo(RegisterInfo[] array, int arrayIndex)
        { list.CopyTo(array, arrayIndex); }

        public int Count
        { get { return list.Count; } }

        public bool IsReadOnly
        { get { return false; } }

        public bool Remove(RegisterInfo item)
        { return list.Remove(item); }

        public IEnumerator<RegisterInfo> GetEnumerator()
        { return list.GetEnumerator(); }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return (System.Collections.IEnumerator)this.GetEnumerator(); }

        public void RunRegEdit()
        {
            foreach (RegisterInfo ri in list)
            {
                Registry.SetValue(ri.KeyName, ri.ValueName, ri.value);                
            }
        }

        public void Add(string KeyName, string ValueName, object value, string RegistryValueKind)
        {
            RegisterInfo ri = new RegisterInfo();
            ri.KeyName = KeyName;
            ri.ValueName = ValueName;
            ri.value = value;
            ri.RegistryValueKind = RegistryValueKind;
            this.Add(ri);
        }

    }

    public class RegisterInfo
    {
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public object value { get; set; }
        public string RegistryValueKind { get; set; }
    }
}
