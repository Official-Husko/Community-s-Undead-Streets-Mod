using GTA;
using GTA.Native;
using NativeUI;
using System.Collections.Generic;
using CWDM.Enums;
using CWDM.Inventory;

namespace CWDM
{
    public static class Crafting
    {
        public static void AddCraftingItemsToMenu(UIMenu menu, CraftType craftType)
        {
            List<InventoryItem> craftables = new List<InventoryItem>();
            for (int i = 0; i < PlayerInventory.PlayerInventoryItems.Count; i++)
            {
                if (PlayerInventory.PlayerInventoryItems[i].GetType() == typeof(InventoryItemCraftable))
                {
                    InventoryItemCraftable item = (InventoryItemCraftable)PlayerInventory.PlayerInventoryItems[i];
                    if (item.CraftType == craftType)
                    {
                        craftables.Add(PlayerInventory.PlayerInventoryItems[i]);
                    }
                }
                else if (PlayerInventory.PlayerInventoryItems[i].GetType() == typeof(InventoryItemRestoreCraftable))
                {
                    InventoryItemRestoreCraftable item = (InventoryItemRestoreCraftable)PlayerInventory.PlayerInventoryItems[i];
                    if (item.CraftType == craftType)
                    {
                        craftables.Add(PlayerInventory.PlayerInventoryItems[i]);
                    }
                }
            }
            for (int i = 0; i < PlayerInventory.PlayerInventoryMaterials.Count; i++)
            {
                if (PlayerInventory.PlayerInventoryMaterials[i].GetType() == typeof(InventoryMaterialCraftable))
                {
                    InventoryMaterialCraftable item = (InventoryMaterialCraftable)PlayerInventory.PlayerInventoryMaterials[i];
                    if (item.CraftType == craftType)
                    {
                        craftables.Add(PlayerInventory.PlayerInventoryMaterials[i]);
                    }
                }
            }
            for (int i = 0; i < craftables.Count; i++)
            {
                string description = "Requirements: ";
                if (craftables[i].GetType() == typeof(InventoryItemCraftable))
                {
                    InventoryItemCraftable item = (InventoryItemCraftable)craftables[i];
                    foreach (RequiredMaterial requiredMaterial in item.GetRequiredMaterials())
                    {
                        string name = requiredMaterial.Material.Name;
                        int amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }
                else if (craftables[i].GetType() == typeof(InventoryItemRestoreCraftable))
                {
                    InventoryItemRestoreCraftable item = (InventoryItemRestoreCraftable)craftables[i];
                    foreach (RequiredMaterial requiredMaterial in item.GetRequiredMaterials())
                    {
                        string name = requiredMaterial.Material.Name;
                        int amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }
                else if (craftables[i].GetType() == typeof(InventoryMaterialCraftable))
                {
                    InventoryMaterialCraftable item = (InventoryMaterialCraftable)craftables[i];
                    foreach (RequiredMaterial requiredMaterial in item.GetRequiredMaterials())
                    {
                        string name = requiredMaterial.Material.Name;
                        int amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }
                menu.AddItem(new UIMenuItem(craftables[i].Name, description));
            }
        }

        public static bool CanBeCrafted(InventoryItem item)
        {
            if (item.GetType() == typeof(InventoryItemCraftable))
            {
                InventoryItemCraftable check = (InventoryItemCraftable)item;
                foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryItems.Find(match: a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }
            else if (item.GetType() == typeof(InventoryItemRestoreCraftable))
            {
                InventoryItemRestoreCraftable check = (InventoryItemRestoreCraftable)item;
                foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryItems.Find(match: a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }
            else if (item.GetType() == typeof(InventoryMaterialCraftable))
            {
                InventoryMaterialCraftable check = (InventoryMaterialCraftable)item;
                foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryMaterials.Find(match: a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool HaveEnoughRoomInInventory(InventoryItem item)
        {
            int amount;
            int maxAmount;
            if (PlayerInventory.PlayerInventoryItems.Exists(match: a => a.Name == item.Name))
            {
                amount = PlayerInventory.PlayerInventoryItems.Find(match: a => a.Name == item.Name).Amount;
                maxAmount = PlayerInventory.PlayerInventoryItems.Find(match: a => a.Name == item.Name).MaxAmount;
            }
            else
            {
                amount = PlayerInventory.PlayerInventoryMaterials.Find(match: a => a.Name == item.Name).Amount;
                maxAmount = PlayerInventory.PlayerInventoryMaterials.Find(match: a => a.Name == item.Name).MaxAmount;
            }
            return maxAmount < amount;
        }

        public static void Craft(InventoryItem item)
        {
            if (CanBeCrafted(item))
            {
                if (HaveEnoughRoomInInventory(item))
                {
                    if (item.GetType() == typeof(InventoryItemCraftable))
                    {
                        InventoryItemCraftable check = (InventoryItemCraftable)item;
                        foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material, requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }
                    else if (item.GetType() == typeof(InventoryItemRestoreCraftable))
                    {
                        InventoryItemRestoreCraftable check = (InventoryItemRestoreCraftable)item;
                        foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material, requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }
                    else if (item.GetType() == typeof(InventoryMaterialCraftable))
                    {
                        InventoryMaterialCraftable check = (InventoryMaterialCraftable)item;
                        foreach (RequiredMaterial requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material, requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }
                    if (PlayerInventory.PlayerInventoryItems.Exists(match: a => a.Name == item.Name))
                    {
                        PlayerInventory.UpdateItemsInventory(item, 1, InventoryUpdate.Increase);
                    }
                    else
                    {
                        InventoryMaterial material = (InventoryMaterial)item;
                        PlayerInventory.UpdateMaterialsInventory(material, 1, InventoryUpdate.Increase);
                    }
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("Crafted ~b~" + item.Name + "~s~!");
                }
                else
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("Not enough space in inventory!", true);
                }
            }
            else
            {
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                UI.Notify("Crafting requirements not met!", true);
            }
        }
    }
}
