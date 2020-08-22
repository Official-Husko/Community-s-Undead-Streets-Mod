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

        private WeaponComponent[] components;

        public WeaponComponent[] GetComponents()
        {
            return components;
        }

        public void SetComponents(WeaponComponent[] value)
        {
            components = value;
        }

        public WeaponData(int ammo, WeaponHash hash, WeaponComponent[] components)
        {
            Ammo = ammo;
            Hash = hash;
            SetComponents(components);
        }
    }
}
