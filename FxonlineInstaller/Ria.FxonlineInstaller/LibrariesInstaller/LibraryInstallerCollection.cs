using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ria.FxonlineInstaller.LibrariesInstaller
{
    public class LibraryInstallerCollection : IList<AbstractLibraryInstaller>
    {
        List<AbstractLibraryInstaller> ali = new List<AbstractLibraryInstaller>();
        public int IndexOf(AbstractLibraryInstaller item)
        {
            return ali.IndexOf(item);
        }

        public void Insert(int index, AbstractLibraryInstaller item)
        {
            ali.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ali.RemoveAt(index);
        }

        public AbstractLibraryInstaller this[int index]
        {
            get
            {
                return ali[index];
            }
            set
            {
                ali[index] = value;
            }
        }

        public void Add(AbstractLibraryInstaller item)
        {
            ali.Add(item);
        }

        public void Clear()
        {
            ali.Clear();
        }

        public bool Contains(AbstractLibraryInstaller item)
        {
            return ali.Contains(item);
        }

        public void CopyTo(AbstractLibraryInstaller[] array, int arrayIndex)
        {
            ali.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ali.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AbstractLibraryInstaller item)
        {
            return ali.Remove(item);
        }

        public IEnumerator<AbstractLibraryInstaller> GetEnumerator()
        {
            return ali.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)this.GetEnumerator();
        }
    }
}
