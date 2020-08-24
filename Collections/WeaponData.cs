using System;
using CWDM.Interfaces;
using GTA.Native;

namespace CWDM.Collections
{
    [Serializable]
    public class WeaponData : IWeapon
    {
        private WeaponComponent[] _components;

        public WeaponData(int ammo, WeaponHash hash, WeaponComponent[] components)
        {
            Ammo = ammo;
            Hash = hash;
            SetComponents(components);
        }

        public int Ammo { get; set; }

        public WeaponHash Hash { get; set; }

        public WeaponComponent[] GetComponents()
        {
            return _components;
        }

        public void SetComponents(WeaponComponent[] value)
        {
            _components = value;
        }
    }
}