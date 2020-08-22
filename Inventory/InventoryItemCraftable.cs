using System;
using CWDM.Enums;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItemCraftable : InventoryItem
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

        public InventoryItemCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            CraftType = craftType;
            RequiredMaterials = requiredMaterials;
        }
    }
}
