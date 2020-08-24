using System;
using GTA;
using GTA.Math;
using GTA.Native;

namespace CWDM.Extensions
{
    public static class EntityExtensions
    {
        public static double DistanceTo(this Entity p1, Vector3 p2)
        {
            var v1 = p1.Position;
            var v2 = p2;
            double x = v2.X - v1.X;
            double y = v2.Y - v1.Y;
            double z = v2.Z - v1.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static double DistanceBetween(this Entity p1, Entity p2)
        {
            var v1 = p1.Position;
            var v2 = p2.Position;
            double x = v2.X - v1.X;
            double y = v2.Y - v1.Y;
            double z = v2.Z - v1.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }

        public static bool HasClearLineOfSight(this Entity entity, Entity target, float visionDistance)
        {
            return Function.Call<bool>(Hash.HAS_ENTITY_CLEAR_LOS_TO_ENTITY, entity.Handle, target.Handle) &&
                   entity.Position.DistanceTo(target.Position) < visionDistance;
        }
    }
}