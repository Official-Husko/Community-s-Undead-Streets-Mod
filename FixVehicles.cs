using CWDM.Extensions;
using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace CWDM
{
    public class FixVehicles : Script
    {
        private Vehicle SelectedVehicle;
        public static List<Vehicle> FixedVehicles = new List<Vehicle>();

        public FixVehicles()
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                Vehicle vehicle = World.GetClosestVehicle(Game.Player.Character.Position, 20f);
                if (SelectedVehicle != null)
                {
                    Game.DisableControlThisFrame(2, GTA.Control.Attack);
                    UIExtensions.DisplayHelpTextThisFrame("Press ~INPUT_ATTACK~ to cancel.");
                    if (Game.IsDisabledControlJustPressed(2, GTA.Control.Attack))
                    {
                        Game.Player.Character.Task.ClearAllImmediately();
                        SelectedVehicle.CloseDoor(VehicleDoor.Hood, false);
                        SelectedVehicle = null;
                    }
                    else if (Game.Player.Character.TaskSequenceProgress == -1)
                    {
                        Random random = new Random();
                        int chance = random.Next(0, 6);
                        if (chance == 2)
                        {
                            SelectedVehicle.EngineHealth = 1000f;
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Vehicle was fixed!", true);
                        }
                        else
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Vehicle could not be fixed!", true);
                        }
                        SelectedVehicle.CloseDoor(VehicleDoor.Hood, false);
                        FixedVehicles.Add(SelectedVehicle);
                        SelectedVehicle = null;
                    }
                }
                else if (vehicle != null)
                {
                    Model model = vehicle.Model;
                    if (model.IsCar && vehicle.EngineHealth < 1000f && !Main.MasterMenuPool.IsAnyMenuOpen() && !vehicle.IsUpsideDown && vehicle.HasBone("engine"))
                    {
                        Vector3 pos = vehicle.GetBoneCoord(vehicle.GetBoneIndex("engine"));
                        if (pos != Vector3.Zero && Game.Player.Character.IsInRangeOf(pos, 1.5f))
                        {
                            if (!Game.Player.Character.Weapons.HasWeapon(WeaponHash.Wrench))
                            {
                                UIExtensions.DisplayHelpTextThisFrame("You need a Wrench to try and fix this vehicle");
                            }
                            else
                            {
                                Game.DisableControlThisFrame(2, GTA.Control.Context);
                                UIExtensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to try and repair vehicle");
                                if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                                {
                                    vehicle.OpenDoor(VehicleDoor.Hood, false, false);
                                    Game.Player.Character.Weapons.Select(WeaponHash.Wrench, true);
                                    Vector3 position = pos + vehicle.ForwardVector;
                                    Vector3 val = vehicle.Position - Game.Player.Character.Position;
                                    float heading = val.ToHeading();
                                    TaskSequence sequence = new TaskSequence();
                                    sequence.AddTask.ClearAllImmediately();
                                    sequence.AddTask.GoTo(position, false, 1500);
                                    sequence.AddTask.AchieveHeading(heading, 2000);
                                    sequence.AddTask.PlayAnimation("mp_intro_seq@", "mp_mech_fix", 8f, -8f, 7500, AnimationFlags.Loop, 1f);
                                    sequence.AddTask.ClearAll();
                                    sequence.Close();
                                    Game.Player.Character.Task.PerformSequence(sequence);
                                    sequence.Dispose();
                                    SelectedVehicle = vehicle;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}