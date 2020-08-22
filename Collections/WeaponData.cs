using GTA.Native;
using System;
using CWDM.Interfaces;

namespace CWDM.Collections
{
    [Serializable]
    public class WeaponData : IWeapon
    {
        public int Ammo
        {
            get;
            set;
        }

        public WeaponHash Hash
        {
            get;
            set;
        }

        public WeaponComponent[] Components
        {
            get;
            set;
        }

        public WeaponData(int ammo, WeaponHash hash, WeaponComponent[] components)
        {
            Ammo = ammo;
            Hash = hash;
            Components = components;
        }
    }
}
