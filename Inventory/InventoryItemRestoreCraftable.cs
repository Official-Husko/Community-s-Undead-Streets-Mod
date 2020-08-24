using System;
using CWDM.Enums;
using CWDM.Interfaces;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItemRestoreCraftable : InventoryItemRestore, IRestore, ICraftable
    {
        private RequiredMaterial[] _requiredMaterials;

        public InventoryItemRestoreCraftable(string name, string description, int amount, int maxAmount,
            LootType[] lootTypes, RestoreType restoreType, float restore, CraftType craftType,
            RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes, restoreType, restore)
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