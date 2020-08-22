using System;
using System.Drawing;
using System.Collections.Generic;
using GTA;
using GTA.Native;
using NativeUI;

namespace CWDM
{
    public enum FoodType
    {
        Food,
        Drink
    }

    public enum RestoreType
    {
        Health,
        Armor
    }

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

        public InventoryItem(string name, string description, int amount, int maxAmount)
        {
            Name = name;
            Description = description;
            Amount = amount;
            MaxAmount = maxAmount;
        }
    }

    public interface IFood
    {
        FoodType FoodType
        {
            get;
            set;
        }

        float Restore
        {
            get;
            set;
        }
    }

    public interface IRestore
    {
        RestoreType RestoreType
        {
            get;
            set;
        }

        float Restore
        {
            get;
            set;
        }
    }

    public interface ICraftable
    {
        MaterialCraftable[] GetRequiredMaterials();
        void SetRequiredMaterials(MaterialCraftable[] value);
    }

    [Serializable]
    public class InventoryCraftableMaterial : InventoryItem, ICraftable
    {
        private MaterialCraftable[] requiredMaterials;

        public MaterialCraftable[] GetRequiredMaterials()
        {
            return requiredMaterials;
        }

        public void SetRequiredMaterials(MaterialCraftable[] value)
        {
            requiredMaterials = value;
        }

        public InventoryCraftableMaterial(string name, string description, int amount, int maxAmount, MaterialCraftable[] requiredMaterials)
            : base(name, description, amount, maxAmount)
        {
            Name = name;
            Description = description;
            Amount = amount;
            MaxAmount = maxAmount;
            SetRequiredMaterials(requiredMaterials);
        }
    }

    [Serializable]
    public class InventoryMaterial : InventoryItem
    {
        public InventoryMaterial(string name, string description, int amount, int maxAmount)
            : base(name, description, amount, maxAmount)
        {
            Name = name;
            Description = description;
            Amount = amount;
            MaxAmount = maxAmount;
        }
    }

    [Serializable]
    public class InventoryCraftableFoodItem : InventoryFoodItem, IFood, ICraftable
    {
        private MaterialCraftable[] requiredMaterials;

        public MaterialCraftable[] GetRequiredMaterials()
        {
            return requiredMaterials;
        }

        public void SetRequiredMaterials(MaterialCraftable[] value)
        {
            requiredMaterials = value;
        }

        public InventoryCraftableFoodItem(string name, string description, int amount, int maxAmount, FoodType foodType, float restore, MaterialCraftable[] requiredMaterials)
            : base(name, description, amount, maxAmount, foodType, restore)
        {
            SetRequiredMaterials(requiredMaterials);
        }
    }

    [Serializable]
    public class InventoryFoodItem : InventoryItem, IFood
    {
        public FoodType FoodType
        {
            get;
            set;
        }

        public float Restore
        {
            get;
            set;
        }

        public InventoryFoodItem(string name, string description, int amount, int maxAmount, FoodType foodType, float restore)
            : base(name, description, amount, maxAmount)
        {
            FoodType = foodType;
            Restore = restore;
        }
    }

    [Serializable]
    public class MaterialCraftable
    {
        public InventoryMaterial Material
        {
            get;
            set;
        }

        public int RequiredAmount
        {
            get;
            set;
        }

        public MaterialCraftable(InventoryMaterial material, int requiredAmount)
        {
            Material = material;
            RequiredAmount = requiredAmount;
        }
    }

    [Serializable]
    public class Inventory : Script
    {
        private readonly List<Ped> lootedPeds = new List<Ped>();
        public static List<InventoryItem> playerItemInventory = new List<InventoryItem>();
        public static InventoryFoodItem bottleWater = new InventoryFoodItem("Water Bottle", "A bottle of water", 0, 10, FoodType.Drink, 0.2f);
        public static InventoryFoodItem bottleSoda = new InventoryFoodItem("Soda Bottle", "A bottle of soda", 0, 10, FoodType.Drink, 0.15f);
        public static InventoryFoodItem chocolateBar = new InventoryFoodItem("Chocolate Bar", "A bar of chocolate", 0, 20, FoodType.Food, 0.1f);
        public static InventoryFoodItem canFood = new InventoryFoodItem("Canned Food", "A can containing uncooked food", 0, 10, FoodType.Food, 0.15f);
        public static List<InventoryMaterial> playerMaterialInventory = new List<InventoryMaterial>();
        public static InventoryMaterial metal = new InventoryMaterial("Metal", "Scraps of metal", 0, 50);
        public static InventoryMaterial wood = new InventoryMaterial("Wood", "Scraps of wood", 0, 50);
        public static InventoryMaterial plastic = new InventoryMaterial("Plastic", "Scraps of plastic", 0, 50);
        public static InventoryMaterial rawMeat = new InventoryMaterial("Raw Meat", "Uncooked animal meat", 0, 20);
        public static InventoryCraftableFoodItem cookedMeat = new InventoryCraftableFoodItem("Cooked Meat", "Meat that has been cooked", 0, 10, FoodType.Food, 0.25f, new MaterialCraftable[]
            { new MaterialCraftable(rawMeat, 1) });

        [NonSerialized]
        public static UIMenu inventoryMenu;
        [NonSerialized]
        public static UIMenu craftCampfireMenu;
        [NonSerialized]
        public static UIMenu itemsSubMenu;
        [NonSerialized]
        public static UIMenu materialsSubMenu;

        public Inventory()
        {
            Tick += OnTick;
            playerItemInventory.Add(bottleWater);
            playerItemInventory.Add(bottleSoda);
            playerItemInventory.Add(chocolateBar);
            playerItemInventory.Add(canFood);
            playerItemInventory.Add(cookedMeat);
            playerMaterialInventory.Add(metal);
            playerMaterialInventory.Add(wood);
            playerMaterialInventory.Add(plastic);
            playerMaterialInventory.Add(rawMeat);
            inventoryMenu = new UIMenu("Inventory", "");
            craftCampfireMenu = new UIMenu("Campfire Crafting", "");
            AddItemsSubMenu(inventoryMenu);
            AddMaterialsSubMenu(inventoryMenu);
            AddCraftingCooking(craftCampfireMenu);
            UIResRectangle uIResRectangle = new UIResRectangle();
            var banner = uIResRectangle;
            banner.Color = Color.FromArgb(255, Color.Yellow);
            inventoryMenu.SetBannerType(banner);
            var banner2 = uIResRectangle;
            banner2.Color = Color.FromArgb(255, Color.OrangeRed);
            craftCampfireMenu.SetBannerType(banner2);
            Main.MasterMenuPool.Add(inventoryMenu);
            Main.MasterMenuPool.Add(craftCampfireMenu);
            Main.MasterMenuPool.RefreshIndex();
            KeyDown += (o, e) =>
            {
                if (e.KeyCode == Main.InventoryKey && Main.ModActive && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    inventoryMenu.Visible = !inventoryMenu.Visible;
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                inventoryMenu.Visible = false;
                craftCampfireMenu.Visible = false;
            }
            if (Main.ModActive)
            {
                Game.DisableControlThisFrame(2, GTA.Control.Phone);
                if (Game.IsDisabledControlJustPressed(2, GTA.Control.Phone) && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    inventoryMenu.Visible = !inventoryMenu.Visible;
                }
            }
            if (Main.ModActive && !Game.Player.Character.IsInVehicle())
            {
                try
                {
                    Prop prop = World.GetClosest(Game.Player.Character.Position, World.GetNearbyProps(Game.Player.Character.Position, 2.5f));
                    if (prop == Character.tent)
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to sleep in Tent");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            Population.Sleep(Game.Player.Character.Position);
                        }
                    }
                    else if (prop == Character.campFire)
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to craft using Campfire");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            craftCampfireMenu.Visible = !craftCampfireMenu.Visible;
                        }
                    }
                    Game.Player.CanControlCharacter = !craftCampfireMenu.Visible;
                    Ped ped = World.GetClosest(Game.Player.Character.Position, World.GetNearbyPeds(Game.Player.Character, 1.5f));
                    if (ped?.IsDead == true && ped.IsHuman && !lootedPeds.Contains(ped))
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to search corpse");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low");
                            int itemRnd = RandoMath.CachedRandom.Next(0, 10);
                            if (itemRnd < 3)
                            {
                                List<InventoryItem> itemsToFind = playerItemInventory;
                                for (int i = 0; i < itemsToFind.Count; i++)
                                {
                                    if (itemsToFind[i].GetType() == typeof(InventoryCraftableFoodItem))
                                    {
                                        itemsToFind.Remove(itemsToFind[i]);
                                    }
                                }
                                InventoryItem itemFound = RandoMath.GetRandomElementFromList(itemsToFind);
                                int invCheckAmount = playerItemInventory.Find(item => item.Name == itemFound.Name).Amount;
                                int invCheckMaxAmount = playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount;
                                if (invCheckAmount == invCheckMaxAmount)
                                {
                                    UI.Notify("Found ~b~" + itemFound.Name + "~w~ but inventory is full!", true);
                                }
                                else
                                {
                                    playerItemInventory.Find(item => item.Name == itemFound.Name).Amount++;
                                    UI.Notify("Found ~b~" + itemFound.Name + "~g~ (" + playerItemInventory.Find(item => item.Name == itemFound.Name).Amount + "/" + playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount + ")", true);
                                    lootedPeds.Add(ped);
                                    itemsSubMenu.MenuItems.Find(item => item.Text == itemFound.Name).SetRightLabel("(" + playerItemInventory.Find(item => item.Name == itemFound.Name).Amount + "/" + playerItemInventory.Find(item => item.Name == itemFound.Name).MaxAmount + ")");
                                }
                            }
                            else if (itemRnd > 5)
                            {
                                List<InventoryMaterial> materialsToFind = playerMaterialInventory;
                                for (int i = 0; i < materialsToFind.Count; i++)
                                {
                                    if (materialsToFind[i].GetType() == typeof(InventoryCraftableMaterial) || materialsToFind[i].Name == "Raw Meat")
                                    {
                                        materialsToFind.Remove(materialsToFind[i]);
                                    }
                                }
                                InventoryItem materialFound = RandoMath.GetRandomElementFromList(materialsToFind);
                                int invCheckAmount = playerMaterialInventory.Find(material => material.Name == materialFound.Name).Amount;
                                int invCheckMaxAmount = playerMaterialInventory.Find(material => material.Name == materialFound.Name).MaxAmount;
                                if (invCheckAmount == invCheckMaxAmount)
                                {
                                    UI.Notify("Found ~b~" + materialFound.Name + "~w~ but inventory is full!", true);
                                }
                                else
                                {
                                    playerMaterialInventory.Find(material => material.Name == materialFound.Name).Amount++;
                                    UI.Notify("Found ~b~" + materialFound.Name + "~g~ (" + playerMaterialInventory.Find(material => material.Name == materialFound.Name).Amount + "/" + playerMaterialInventory.Find(material => material.Name == materialFound.Name).MaxAmount + ")", true);
                                    lootedPeds.Add(ped);
                                    materialsSubMenu.MenuItems.Find(item => item.Text == materialFound.Name).SetRightLabel("(" + playerMaterialInventory.Find(item => item.Name == materialFound.Name).Amount + "/" + playerMaterialInventory.Find(item => item.Name == materialFound.Name).MaxAmount + ")");
                                    craftCampfireMenu.MenuItems.Find(item => item.Text == materialFound.Name).SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                                }
                            }
                            else
                            {
                                UI.Notify("Found nothing", true);
                                lootedPeds.Add(ped);
                            }
                        }
                    }
                    else if (ped?.IsDead == true && !ped.IsHuman && !lootedPeds.Contains(ped))
                    {
                        Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to harvest meat from animal corpse");
                        Game.DisableControlThisFrame(2, GTA.Control.Context);
                        if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                        {
                            if (Game.Player.Character.Weapons.HasWeapon(WeaponHash.Knife))
                            {
                                int invCheckAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                                int invCheckMaxAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount;
                                if (invCheckAmount == invCheckMaxAmount)
                                {
                                    UI.Notify("You cannot carry any more ~b~Raw Meat~w~!", true);
                                }
                                else
                                {
                                    Game.Player.Character.Weapons.Select(WeaponHash.Knife, true);
                                    Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low", 8f, 3000, AnimationFlags.None);
                                    playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount++;
                                    materialsSubMenu.MenuItems.Find(item => item.Text == "Raw Meat").SetRightLabel("(" + playerMaterialInventory.Find(item => item.Name == "Raw Meat").Amount + "/" + playerMaterialInventory.Find(item => item.Name == "Raw Meat").MaxAmount + ")");
                                    craftCampfireMenu.MenuItems[0].SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                                    UI.Notify("You have harvested ~b~Raw Meat ~g~(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + "/" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount + ")", true);
                                    lootedPeds.Add(ped);
                                }
                            }
                            else
                            {
                                UI.Notify("You need a knife to harvest ~b~Raw Meat~w~ from dead animals!", true);
                            }
                        }
                    }
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
        }

        public static void AddCraftingCooking(UIMenu menu)
        {
            UIMenuItem cookRawMeat = new UIMenuItem("Cook Raw Meat", "Cook Raw Meat so it can be eaten");
            cookRawMeat.SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
            menu.AddItem(cookRawMeat);
            craftCampfireMenu.OnItemSelect += (sender, item, index) =>
            {
                int invRawMeatAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                int invCookedMeatAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount;
                int invCookedMeatMaxAmount = playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount;
                if (invRawMeatAmount > 0)
                {
                    if (invCookedMeatAmount != invCookedMeatMaxAmount)
                    {
                        playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount--;
                        playerItemInventory.Find(items => items.Name == "Cooked Meat").Amount++;
                        cookRawMeat.SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + ")");
                        materialsSubMenu.MenuItems.Find(material => material.Text == "Raw Meat").SetRightLabel("(" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").Amount + "/" + playerMaterialInventory.Find(material => material.Name == "Raw Meat").MaxAmount + ")");
                        itemsSubMenu.MenuItems.Find(items => items.Text == "Cooked Meat").SetRightLabel("(" + playerItemInventory.Find(items => items.Name == "Cooked Meat").Amount + "/" + playerItemInventory.Find(items => items.Name == "Cooked Meat").MaxAmount + ")");
                        UI.Notify("You've made some ~b~Cooked Meat~w~!");
                    }
                    else
                    {
                        UI.Notify("Your inventory is full!");
                    }
                }
                else
                {
                    UI.Notify("You have no ~b~Raw Meat~w~ to cook");
                }
            };
        }

        public static void AddItemsSubMenu(UIMenu menu)
        {
            itemsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Items", "See what useful items you are carrying");
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkGreen)
            };
            itemsSubMenu.SetBannerType(banner);
            for (int i = 0; i < playerItemInventory.Count; i++)
            {
                itemsSubMenu.AddItem(new UIMenuItem(playerItemInventory[i].Name, playerItemInventory[i].Description));
                itemsSubMenu.MenuItems[itemsSubMenu.MenuItems.Count - 1].SetRightLabel("(" + playerItemInventory[i].Amount + "/" + playerItemInventory[i].MaxAmount + ")");
            }
            itemsSubMenu.OnItemSelect += (sender, item, index) =>
            {
                if (playerItemInventory[index].Amount > 0)
                {
                    if (playerItemInventory[index].GetType() == typeof(InventoryFoodItem))
                    {
                        InventoryFoodItem foodItem = (InventoryFoodItem)playerItemInventory[index];
                        if (foodItem.FoodType == FoodType.Food)
                        {
                            if (Character.currentHungerLevel < 1.0f)
                            {
                                Character.currentHungerLevel += foodItem.Restore;
                                if (Character.currentHungerLevel > 1.0f)
                                {
                                    Character.currentHungerLevel = 1.0f;
                                }
                                playerItemInventory[index].Amount--;
                                item.SetRightLabel("(" + playerItemInventory[index].Amount + "/" + playerItemInventory[index].MaxAmount + ")");
                                UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                            }
                            else
                            {
                                UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                            }
                        }
                        else if (foodItem.FoodType == FoodType.Drink)
                        {
                            if (Character.currentThirstLevel < 1.0f)
                            {
                                Character.currentThirstLevel += foodItem.Restore;
                                if (Character.currentThirstLevel > 1.0f)
                                {
                                    Character.currentThirstLevel = 1.0f;
                                }
                                playerItemInventory[index].Amount--;
                                item.SetRightLabel("(" + playerItemInventory[index].Amount + "/" + playerItemInventory[index].MaxAmount + ")");
                                UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                            }
                            else
                            {
                                UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                            }
                        }
                    }
                    else if (playerItemInventory[index].GetType() == typeof(InventoryCraftableFoodItem))
                    {
                        InventoryCraftableFoodItem foodItem = (InventoryCraftableFoodItem)playerItemInventory[index];
                        if (foodItem.FoodType == FoodType.Food)
                        {
                            if (Character.currentHungerLevel < 1.0f)
                            {
                                Character.currentHungerLevel += foodItem.Restore;
                                if (Character.currentHungerLevel > 1.0f)
                                {
                                    Character.currentHungerLevel = 1.0f;
                                }
                                playerItemInventory[index].Amount--;
                                item.SetRightLabel("(" + playerItemInventory[index].Amount + "/" + playerItemInventory[index].MaxAmount + ")");
                                UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Hunger levels.");
                            }
                            else
                            {
                                UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Hunger levels are full.");
                            }
                        }
                        else if (foodItem.FoodType == FoodType.Drink)
                        {
                            if (Character.currentThirstLevel < 1.0f)
                            {
                                Character.currentThirstLevel += foodItem.Restore;
                                if (Character.currentThirstLevel > 1.0f)
                                {
                                    Character.currentThirstLevel = 1.0f;
                                }
                                playerItemInventory[index].Amount--;
                                item.SetRightLabel("(" + playerItemInventory[index].Amount + "/" + playerItemInventory[index].MaxAmount + ")");
                                UI.Notify("Used ~b~" + item.Text + "~w~ to replenish Thirst levels.");
                            }
                            else
                            {
                                UI.Notify("Cannot use ~b~" + item.Text + "~w~ as your Thirst levels are full.");
                            }
                        }
                    }
                }
                else
                {
                    UI.Notify("Cannot use ~b~" + item.Text + "~w~ as you are not carrying any.");
                }
            };
        }

        public static void AddMaterialsSubMenu(UIMenu menu)
        {
            materialsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Materials", "See what materials that can be used for crafting you are carrying");
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkGreen)
            };
            materialsSubMenu.SetBannerType(banner);
            for (int i = 0; i < playerMaterialInventory.Count; i++)
            {
                materialsSubMenu.AddItem(new UIMenuItem(playerMaterialInventory[i].Name, playerMaterialInventory[i].Description));
                materialsSubMenu.MenuItems[materialsSubMenu.MenuItems.Count - 1].SetRightLabel("(" + playerMaterialInventory[i].Amount + "/" + playerMaterialInventory[i].MaxAmount + ")");
            }
        }
    }
}
