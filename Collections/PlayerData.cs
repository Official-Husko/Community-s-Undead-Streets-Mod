using GTA;
using GTA.Math;
using System;

namespace CWDM.Collections
{
    [Serializable]
    public class PlayerData
    {
        public Gender Gender
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public Vector3 Rotation
        {
            get;
            set;
        }

        public float Heading
        {
            get;
            set;
        }

        public int Health
        {
            get;
            set;
        }

        public int Armor
        {
            get;
            set;
        }

        public float Hunger
        {
            get;
            set;
        }

        public float Thirst
        {
            get;
            set;
        }

        public float Energy
        {
            get;
            set;
        }

        public float Infection
        {
            get;
            set;
        }

        public float Battery
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }

        public TimeSpan Time
        {
            get;
            set;
        }

        public Weather Weather
        {
            get;
            set;
        }

        public PlayerData(Gender gender, Vector3 position, Vector3 rotation, float heading, int health, int armor, float hunger, float thirst, float energy, float infection, float battery, DateTime date, TimeSpan time, Weather weather)
        {
            Gender = gender;
            Position = position;
            Rotation = rotation;
            Heading = heading;
            Health = health;
            Armor = armor;
            Hunger = hunger;
            Thirst = thirst;
            Energy = energy;
            Infection = infection;
            Battery = battery;
            Date = date;
            Time = time;
            Weather = weather;
        }
    }
}
