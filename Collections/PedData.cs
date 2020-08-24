using System;
using System.Collections.Generic;
using CWDM.Enums;
using GTA.Math;

namespace CWDM.Collections
{
    [Serializable]
    public class PedData
    {
        public PedData(int handle, int hash, Vector3 rotation, Vector3 position, PedTask task, List<WeaponData> weapons)
        {
            Handle = handle;
            Hash = hash;
            Rotation = rotation;
            Position = position;
            Task = task;
            Weapons = weapons;
        }

        public int Handle { get; set; }

        public int Hash { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Position { get; set; }

        public PedTask Task { get; set; }

        public List<WeaponData> Weapons { get; set; }
    }
}