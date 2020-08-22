using System;
using GTA;
using GTA.Native;
using GTA.Math;

namespace CWDM
{
    public static class Extensions
    {
        public static Vehicle SpawnVehicle(Model model, Vector3 position, float heading = 0.0f)
        {
            if (!model.IsVehicle || !model.Request(1000))
            {
                return null;
            }

            Vehicle veh = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading, false, false);
            Function.Call(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, veh);
            Function.Call(Hash.SET_ENTITY_AS_MISSION_ENTITY, veh, true, false);
            return veh;
        }

        public static void Recruit(this Ped ped, Ped leader)
        {
            if (!(leader == null))
            {
                PedGroup group = leader.CurrentPedGroup;
                ped.LeaveGroup();
                Function.Call(Hash.SET_PED_RAGDOLL_ON_COLLISION, ped.Handle, false);
                ped.Task.ClearAll();
                group.SeparationRange = 2.14748365E+09f;
                if (!group.Contains(leader))
                {
                    group.Add(leader, true);
                }
                if (!group.Contains(ped))
                {
                    group.Add(ped, false);
                }
                ped.IsPersistent = true;
                ped.RelationshipGroup = leader.RelationshipGroup;
                ped.NeverLeavesGroup = true;
                Blip currentBlip = ped.CurrentBlip;
                if (currentBlip.Type != 0)
                {
                    currentBlip.Remove();
                }
                Blip blip = ped.AddBlip();
                blip.Color = BlipColor.Green;
                blip.Scale = 0.65f;
                blip.Name = "Group";
                PlayerGroup.SetPedTasks(ped, PedTasks.Follow);
            }
        }

        public static double DistanceBetween(Entity p1, Entity p2)
        {
            Vector3 v1 = p1.Position;
            Vector3 v2 = p2.Position;
            double distance;
            double x = v2.X - v1.X;
            double y = v2.Y - v1.Y;
            double z = v2.Z - v1.Z;
            distance = Math.Sqrt((x * x) + (y * y) + (z * z));
            return distance;
        }

        public static double DistanceBetweenV3(Vector3 p1, Vector3 p2)
        {
            Vector3 v1 = p1;
            Vector3 v2 = p2;
            double distance;
            double x = v2.X - v1.X;
            double y = v2.Y - v1.Y;
            double z = v2.Z - v1.Z;
            distance = Math.Sqrt((x * x) + (y * y) + (z * z));
            return distance;
        }

        public static double DistanceTo(Entity p1, Vector3 p2)
        {
            Vector3 v1 = p1.Position;
            Vector3 v2 = p2;
            double distance;
            double x = v2.X - v1.X;
            double y = v2.Y - v1.Y;
            double z = v2.Z - v1.Z;
            distance = Math.Sqrt((x * x) + (y * y) + (z * z));
            return distance;
        }

        public static bool IsOnScreen(this Vector3 vector3)
        {
            Vector3 position = GameplayCamera.Position;
            Vector3 direction = GameplayCamera.Direction;
            float fieldOfView = GameplayCamera.FieldOfView;
            return (double)Vector3.Angle(Vector3.Subtract(vector3, position), direction) < (double)fieldOfView;
        }

        public static bool HasClearLineOfSight(this Entity entity, Entity target, float visionDistance)
        {
            return Function.Call<bool>(Hash.HAS_ENTITY_CLEAR_LOS_TO_ENTITY, entity.Handle, target.Handle) && entity.Position.DistanceTo(target.Position) < visionDistance;
        }

        public static bool IsCurrentWeaponSileced(this Ped ped)
        {
            return Function.Call<bool>(Hash.IS_PED_CURRENT_WEAPON_SILENCED, ped.Handle);
        }

        public static bool IsAnyHelpTextOnScreen()
        {
            return Function.Call<bool>(Hash.IS_HELP_MESSAGE_ON_SCREEN);
        }

        public static void ClearAllHelpText()
        {
            Function.Call(Hash.CLEAR_ALL_HELP_MESSAGES);
        }

        public static void DisplayHelpTextThisFrame(string helpText)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON");
            for (int i = 0; i < helpText.Length; i += 99)
            {
                Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, helpText.Substring(i, Math.Min(99, helpText.Length - i)));
            }
            Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, !Function.Call<bool>(Hash.IS_HELP_MESSAGE_BEING_DISPLAYED) ? 1 : 0, -1);
        }
    }
}
