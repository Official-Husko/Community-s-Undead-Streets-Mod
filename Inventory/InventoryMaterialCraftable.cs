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

        public RequiredMaterial[] RequiredMaterials
        {
            get;
            set;
        }

        public InventoryMaterialCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            CraftType = craftType;
            RequiredMaterials = requiredMaterials;
        }
    }
}
