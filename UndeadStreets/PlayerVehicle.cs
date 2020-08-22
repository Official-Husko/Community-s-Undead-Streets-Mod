using System;
using System.Collections;
using System.Collections.Generic;
using GTA;
using GTA.Math;

namespace CWDM
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
            get
            {
                return vehicles[index];
            }
            set
            {
                vehicles[index] = value;
            }
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
            this.ListChanged?.Invoke(this);
        }

        public void Clear()
        {
            vehicles.Clear();
            this.ListChanged?.Invoke(this);
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
            this.ListChanged?.Invoke(this);
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

    [Serializable]
    public class VehicleData
    {
        public int Handle
        {
            get;
            set;
        }

        public int Hash
        {
            get;
            set;
        }

        public Vector3 Rotation
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public int Health
        {
            get;
            set;
        }

        public float EngineHealth
        {
            get;
            set;
        }

        public VehicleColor PrimaryColor
        {
            get;
            set;
        }

        public VehicleColor SecondaryColor
        {
            get;
            set;
        }

        public float Heading
        {
            get;
            set;
        }

        public VehicleData(int handle, int hash, Vector3 rotation, Vector3 position, VehicleColor primaryColor, VehicleColor secondaryColor, int health, float engineHealth, float heading)
        {
            Handle = handle;
            Hash = hash;
            Rotation = rotation;
            Position = position;
            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            Health = health;
            EngineHealth = engineHealth;
            Heading = heading;
        }
    }

    public class PlayerVehicle
    {
        public static VehicleCollection PlayerVehicleCollection = new VehicleCollection();

        public static void AddVehicleData(Vehicle vehicle)
        {
            VehicleData vehicleData = new VehicleData(vehicle.Handle, vehicle.Model.Hash, vehicle.Rotation, vehicle.Position, vehicle.PrimaryColor, vehicle.SecondaryColor, vehicle.Health, vehicle.EngineHealth, vehicle.Heading);
            PlayerVehicleCollection.Add(vehicleData);
        }

        public static void LoadVehicleFromVehicleData(VehicleData vehicleData)
        {
            Model model = new Model(vehicleData.Hash);
            Vehicle vehicle = Extensions.SpawnVehicle(model, vehicleData.Position, vehicleData.Heading);
            if (vehicle != null)
            {
                vehicle.Rotation = vehicleData.Rotation;
                vehicle.PrimaryColor = vehicleData.PrimaryColor;
                vehicle.SecondaryColor = vehicleData.SecondaryColor;
                vehicle.Health = vehicleData.Health;
                vehicle.EngineHealth = vehicleData.EngineHealth;
            }
            Character.playerVehicle = vehicle;
        }
    }
}
