using System;
using CWDM.Enums;
using CWDM.Interfaces;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItemRestoreCraftable : InventoryItemRestore, IRestore, ICraftable
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

        public InventoryItemRestoreCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes, RestoreType restoreType, float restore, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes, restoreType, restore)
        {
            CraftType = craftType;
            RequiredMaterials = requiredMaterials;
        }
    }
}
