using GTA;
using GTA.Math;
using NativeUI;
using System;
using System.Drawing;
using System.Linq;
using CWDM.Enums;
using CWDM.Extensions;
using CWDM.Inventory;

namespace CWDM.Interactives
{
    public class Campfire : Script
    {
        public static UIMenu CampfireMenu;

        public Campfire()
        {
            Tick += OnTick;
            CampfireMenu = new UIMenu("Campfire", "Cook Raw Meat here so it can be consumed");
            UIResRectangle banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.OrangeRed)
            };
            CampfireMenu.SetBannerType(banner);
            AddMenuCook(CampfireMenu);
            Main.MasterMenuPool.Add(CampfireMenu);
            Main.MasterMenuPool.RefreshIndex();
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                if (!CampfireMenu.Visible && !Main.MasterMenuPool.IsAnyMenuOpen() && Game.Player.Character.IsAlive)
                {
                    Game.Player.Character.FreezePosition = false;
                }
                Prop prop = World.GetClosest(Game.Player.Character.Position, World.GetNearbyProps(Game.Player.Character.Position, 2.5f));
                if (prop != null && prop == Character.CampFire)
                {
                    UIExtensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to cook on campfire");
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context) && !CampfireMenu.Visible && !Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        Ped[] peds = World.GetNearbyPeds(Game.Player.Character.Position, 50f).Where(IsEnemy).ToArray();
                        if (!peds.Any())
                        {
                            Vector3 val = prop.Position - Game.Player.Character.Position;
                            Game.Player.Character.Heading = val.ToHeading();
                            Game.Player.Character.FreezePosition = true;
                            CampfireMenu.Visible = !CampfireMenu.Visible;
                        }
                        else
                        {
                            UI.Notify("There are ~r~hostiles ~s~nearby");
                        }
                    }
                }
            }
        }

        public void AddMenuCook(UIMenu menu)
        {
            Crafting.AddCraftingItemsToMenu(menu, CraftType.Campfire);
            menu.OnItemSelect += (sender, item, index) =>
            {
                InventoryItem craftingItem;
                if (PlayerInventory.PlayerInventoryItems.Exists(match: a => a.Name == item.Text))
                {
                    craftingItem = PlayerInventory.PlayerInventoryItems.Find(match: a => a.Name == item.Text);
                }
                else
                {
                    craftingItem = PlayerInventory.PlayerInventoryMaterials.Find(match: a => a.Name == item.Text);
                }
                Crafting.Craft(craftingItem);
            };
        }

        private static bool IsEnemy(Ped ped)
        {
            return (ped.IsHuman && ped.IsAlive && ped.GetRelationshipWithPed(Game.Player.Character) == Relationship.Hate) || ped.IsInCombatAgainst(Game.Player.Character);
        }
    }
}
