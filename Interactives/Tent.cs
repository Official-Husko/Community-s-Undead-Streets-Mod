using System;
using System.Linq;
using CWDM.Extensions;
using GTA;

namespace CWDM.Interactives
{
    public class Tent : Script
    {
        public Tent()
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                var prop = World.GetClosest(Game.Player.Character.Position,
                    World.GetNearbyProps(Game.Player.Character.Position, 2.5f));
                if (prop != null && prop == Character.Tent)
                {
                    "Press ~INPUT_CONTEXT~ to sleep in Tent".DisplayHelpTextThisFrame();
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context))
                    {
                        var peds = World.GetNearbyPeds(Game.Player.Character.Position, 50f).Where(IsEnemy).ToArray();
                        if (peds.Length == 0)
                        {
                            var val = prop.Position - Game.Player.Character.Position;
                            Game.Player.Character.Heading = val.ToHeading();
                            Game.Player.Character.FreezePosition = true;
                            Game.FadeScreenOut(2000);
                            Wait(3000);
                            Character.CurrentEnergyLevel += 0.4f;
                            if (Character.CurrentEnergyLevel > Character.MaxEnergyLevel)
                                Character.CurrentEnergyLevel = Character.MaxEnergyLevel;
                            Character.CurrentHungerLevel -= 0.25f;
                            Character.CurrentThirstLevel -= 0.3f;
                            var unused = World.CurrentDayTime.Add(new TimeSpan(6, 0, 0));
                            World.Weather = Map.Weathers.GetRandomElementFromArray();
                            if (Population.AnimalPeds.Count > 0)
                                for (var i = 0; i < Population.AnimalPeds.Count; i++)
                                {
                                    Population.AnimalPeds[i].PedEntity?.Delete();
                                    Population.AnimalPeds.RemoveAt(i);
                                }

                            if (Population.ZombiePeds.Count > 0)
                                for (var i = 0; i < Population.ZombiePeds.Count; i++)
                                {
                                    Population.ZombiePeds[i].PedEntity?.Delete();
                                    Population.ZombiePeds.RemoveAt(i);
                                }

                            if (Population.SurvivorPeds.Count > 0)
                                for (var i = 0; i < Population.SurvivorPeds.Count; i++)
                                {
                                    if (Game.Player.Character.CurrentPedGroup.Contains(Population.SurvivorPeds[i]
                                        .PedEntity)) continue;
                                    Population.SurvivorPeds[i].PedEntity?.Delete();
                                    Population.SurvivorPeds.RemoveAt(i);
                                }

                            Stats.StatsLastUpdateTime = DateTime.UtcNow;
                            Population.SurvivorLastSpawnTime = DateTime.UtcNow;
                            Config.SavePlayerAll();
                            Wait(8000);
                            Game.FadeScreenIn(2000);
                            Game.Player.Character.FreezePosition = false;
                        }
                        else
                        {
                            UI.Notify("There are ~r~hostiles ~s~nearby");
                        }
                    }
                }
            }
        }

        private static bool IsEnemy(Ped ped)
        {
            return ped.IsHuman && ped.IsAlive &&
                   ped.GetRelationshipWithPed(Game.Player.Character) == Relationship.Hate ||
                   ped.IsInCombatAgainst(Game.Player.Character);
        }
    }
}