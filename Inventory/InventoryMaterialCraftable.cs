using System;
using CWDM.Enums;
using CWDM.Interfaces;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryMaterialCraftable : InventoryMaterial, ICraftable
    {
        public CraftType CraftType
        {
            get;
            set;
        }

        private RequiredMaterial[] requiredMaterials;

        public RequiredMaterial[] GetRequiredMaterials()
        {
            return requiredMaterials;
        }

        public void SetRequiredMaterials(RequiredMaterial[] value)
        {
            requiredMaterials = value;
        }

        public InventoryMaterialCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            CraftType = craftType;
            SetRequiredMaterials(requiredMaterials);
        }
    }
}
