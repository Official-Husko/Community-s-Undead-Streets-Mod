using GTA;
using GTA.Math;
using System;

namespace CWDM.Collections
{
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

        public float Fuel
        {
            get;
            set;
        }

        public VehicleData(int handle, int hash, Vector3 rotation, Vector3 position, VehicleColor primaryColor, VehicleColor secondaryColor, int health, float engineHealth, float heading, float fuel)
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
            Fuel = fuel;
        }
    }
}