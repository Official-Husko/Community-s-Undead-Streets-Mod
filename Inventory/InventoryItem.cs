using System;
using CWDM.Enums;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

        public int MaxAmount
        {
            get;
            set;
        }

        public LootType[] LootTypes
        {
            get;
            set;
        }

        public InventoryItem(string name, string description, int amount, int maxAmount, LootType[] lootTypes)
        {
            Name = name;
            Description = description;
            Amount = amount;
            MaxAmount = maxAmount;
            LootTypes = lootTypes;
        }
    }
}
