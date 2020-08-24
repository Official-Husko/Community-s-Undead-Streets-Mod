using System;
using System.Collections;
using System.Collections.Generic;

namespace CWDM.Collections
{
    [Serializable]
    public class PedCollection : IList<PedData>
    {
        public delegate void ListChangedEvent(PedCollection sender);

        private readonly List<PedData> _peds;

        public PedCollection()
        {
            _peds = new List<PedData>();
        }

        public int Count => _peds.Count;

        public bool IsReadOnly => ((ICollection<PedData>) _peds).IsReadOnly;

        public PedData this[int index]
        {
            get => _peds[index];
            set => _peds[index] = value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PedData> GetEnumerator()
        {
            return _peds.GetEnumerator();
        }

        public void Add(PedData item)
        {
            _peds.Add(item);
            ListChanged?.Invoke(this);
        }

        public void Clear()
        {
            _peds.Clear();
            ListChanged?.Invoke(this);
        }

        public bool Contains(PedData item)
        {
            return _peds.Contains(item);
        }

        public void CopyTo(PedData[] array, int arrayIndex)
        {
            _peds.CopyTo(array, arrayIndex);
        }

        public bool Remove(PedData item)
        {
            var remove = _peds.Remove(item);
            ListChanged?.Invoke(this);
            return remove;
        }

        public int IndexOf(PedData item)
        {
            return _peds.IndexOf(item);
        }

        public void Insert(int index, PedData item)
        {
            _peds.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _peds.RemoveAt(index);
        }

        [field: NonSerialized] public event ListChangedEvent ListChanged;
    }
}