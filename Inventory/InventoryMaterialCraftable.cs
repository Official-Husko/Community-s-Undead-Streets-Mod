using System;
using CWDM.Enums;
using CWDM.Interfaces;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryMaterialCraftable : InventoryMaterial, ICraftable
    {
        private RequiredMaterial[] _requiredMaterials;

        public InventoryMaterialCraftable(string name, string description, int amount, int maxAmount,
            LootType[] lootTypes, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            CraftType = craftType;
            SetRequiredMaterials(requiredMaterials);
        }

        public CraftType CraftType { get; set; }

        public RequiredMaterial[] GetRequiredMaterials()
        {
            return _requiredMaterials;
        }

        public void SetRequiredMaterials(RequiredMaterial[] value)
        {
            _requiredMaterials = value;
        }
    }
}