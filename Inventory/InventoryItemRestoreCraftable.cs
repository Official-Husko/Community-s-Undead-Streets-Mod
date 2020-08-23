using CWDM.Enums;
using CWDM.Interfaces;
using System;

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

        private RequiredMaterial[] requiredMaterials;

        public RequiredMaterial[] GetRequiredMaterials()
        {
            return requiredMaterials;
        }

        public void SetRequiredMaterials(RequiredMaterial[] value)
        {
            requiredMaterials = value;
        }

        public InventoryItemRestoreCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes, RestoreType restoreType, float restore, CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes, restoreType, restore)
        {
            CraftType = craftType;
            SetRequiredMaterials(requiredMaterials);
        }
    }
}