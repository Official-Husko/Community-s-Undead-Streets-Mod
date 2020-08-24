using System;
using System.Collections.Generic;
using CWDM.Extensions;
using GTA;
using GTA.Math;
using GTA.Native;

namespace CWDM
{
    public class FixVehicles : Script
    {
        public static List<Vehicle> FixedVehicles = new List<Vehicle>();
        private Vehicle _selectedVehicle;

        public FixVehicles()
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                var vehicle = World.GetClosestVehicle(Game.Player.Character.Position, 20f);
                if (_selectedVehicle != null)
                {
                    Game.DisableControlThisFrame(2, Control.Attack);
                    "Press ~INPUT_ATTACK~ to cancel.".DisplayHelpTextThisFrame();
                    if (Game.IsDisabledControlJustPressed(2, Control.Attack))
                    {
                        Game.Player.Character.Task.ClearAllImmediately();
                        _selectedVehicle.CloseDoor(VehicleDoor.Hood, false);
                        _selectedVehicle = null;
                    }
                    else if (Game.Player.Character.TaskSequenceProgress == -1)
                    {
                        var random = new Random();
                        var chance = random.Next(0, 6);
                        if (chance == 2)
                        {
                            _selectedVehicle.EngineHealth = 1000f;
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Vehicle was fixed!", true);
                        }
                        else
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Vehicle could not be fixed!", true);
                        }

                        _selectedVehicle.CloseDoor(VehicleDoor.Hood, false);
                        FixedVehicles.Add(_selectedVehicle);
                        _selectedVehicle = null;
                    }
                }
                else if (vehicle != null)
                {
                    var model = vehicle.Model;
                    if (model.IsCar && vehicle.EngineHealth < 1000f && !Main.MasterMenuPool.IsAnyMenuOpen() &&
                        !vehicle.IsUpsideDown && vehicle.HasBone("engine"))
                    {
                        var pos = vehicle.GetBoneCoord(vehicle.GetBoneIndex("engine"));
                        if (pos != Vector3.Zero && Game.Player.Character.IsInRangeOf(pos, 1.5f))
                        {
                            if (!Game.Player.Character.Weapons.HasWeapon(WeaponHash.Wrench))
                            {
                                "You need a Wrench to try and fix this vehicle".DisplayHelpTextThisFrame();
                            }
                            else
                            {
                                Game.DisableControlThisFrame(2, Control.Context);
                                "Press ~INPUT_CONTEXT~ to try and repair vehicle".DisplayHelpTextThisFrame();
                                if (Game.IsDisabledControlJustPressed(2, Control.Context))
                                {
                                    vehicle.OpenDoor(VehicleDoor.Hood, false, false);
                                    Game.Player.Character.Weapons.Select(WeaponHash.Wrench, true);
                                    var position = pos + vehicle.ForwardVector;
                                    var val = vehicle.Position - Game.Player.Character.Position;
                                    var heading = val.ToHeading();
                                    var sequence = new TaskSequence();
                                    sequence.AddTask.ClearAllImmediately();
                                    sequence.AddTask.GoTo(position, false, 1500);
                                    sequence.AddTask.AchieveHeading(heading, 2000);
                                    sequence.AddTask.PlayAnimation("mp_intro_seq@", "mp_mech_fix", 8f, -8f, 7500,
                                        AnimationFlags.Loop, 1f);
                                    sequence.AddTask.ClearAll();
                                    sequence.Close();
                                    Game.Player.Character.Task.PerformSequence(sequence);
                                    sequence.Dispose();
                                    _selectedVehicle = vehicle;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}