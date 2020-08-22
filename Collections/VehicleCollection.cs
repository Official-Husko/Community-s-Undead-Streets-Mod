using System;
using System.Collections;
using System.Collections.Generic;

namespace CWDM.Collections
{
    [Serializable]
    public class VehicleCollection : IList<VehicleData>, ICollection<VehicleData>, IEnumerable<VehicleData>, IEnumerable
    {
        public delegate void ListChangedEvent(VehicleCollection sender);

        private readonly List<VehicleData> vehicles;

        public int Count => vehicles.Count;

        public bool IsReadOnly => ((ICollection<VehicleData>)vehicles).IsReadOnly;

        public VehicleData this[int index]
        {
            get => vehicles[index];
            set => vehicles[index] = value;
        }

        [field: NonSerialized]
        public event ListChangedEvent ListChanged;

        public VehicleCollection()
        {
            vehicles = new List<VehicleData>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<VehicleData> GetEnumerator()
        {
            return vehicles.GetEnumerator();
        }

        public void Add(VehicleData item)
        {
            vehicles.Add(item);
            ListChanged?.Invoke(this);
        }

        public void Clear()
        {
            vehicles.Clear();
            ListChanged?.Invoke(this);
        }

        public bool Contains(VehicleData item)
        {
            return vehicles.Contains(item);
        }

        public void CopyTo(VehicleData[] array, int arrayIndex)
        {
            vehicles.CopyTo(array, arrayIndex);
        }

        public bool Remove(VehicleData item)
        {
            bool remove = vehicles.Remove(item);
            ListChanged?.Invoke(this);
            return remove;
        }

        public int IndexOf(VehicleData item)
        {
            return vehicles.IndexOf(item);
        }

        public void Insert(int index, VehicleData item)
        {
            vehicles.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            vehicles.RemoveAt(index);
        }
    }
}
