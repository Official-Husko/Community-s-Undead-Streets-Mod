using GTA;
using GTA.Math;

namespace CWDM.Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsOnScreen(this Vector3 vector3)
        {
            var camPos = GameplayCamera.Position;
            var camDir = GameplayCamera.Direction;
            var fov = GameplayCamera.FieldOfView;
            var dir = vector3 - camPos;
            var angle = Vector3.Angle(dir, camDir);
            return angle < fov;
        }
    }
}