using System;
using System.Collections;
using System.Collections.Generic;

namespace CWDM.Collections
{
    [Serializable]
    public class PedCollection : IList<PedData>, ICollection<PedData>, IEnumerable<PedData>, IEnumerable
    {
        public delegate void ListChangedEvent(PedCollection sender);

        private readonly List<PedData> peds;

        public int Count => peds.Count;

        public bool IsReadOnly => ((ICollection<PedData>)peds).IsReadOnly;

        public PedData this[int index]
        {
            get => peds[index];
            set => peds[index] = value;
        }

        [field: NonSerialized]
        public event ListChangedEvent ListChanged;

        public PedCollection()
        {
            peds = new List<PedData>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PedData> GetEnumerator()
        {
            return peds.GetEnumerator();
        }

        public void Add(PedData item)
        {
            peds.Add(item);
            ListChanged?.Invoke(this);
        }

        public void Clear()
        {
            peds.Clear();
            ListChanged?.Invoke(this);
        }

        public bool Contains(PedData item)
        {
            return peds.Contains(item);
        }

        public void CopyTo(PedData[] array, int arrayIndex)
        {
            peds.CopyTo(array, arrayIndex);
        }

        public bool Remove(PedData item)
        {
            bool remove = peds.Remove(item);
            ListChanged?.Invoke(this);
            return remove;
        }

        public int IndexOf(PedData item)
        {
            return peds.IndexOf(item);
        }

        public void Insert(int index, PedData item)
        {
            peds.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            peds.RemoveAt(index);
        }
    }
}