using GTA;
using GTA.Math;

namespace CWDM.Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsOnScreen(this Vector3 vector3)
        {
            Vector3 camPos = GameplayCamera.Position;
            Vector3 camDir = GameplayCamera.Direction;
            float fov = GameplayCamera.FieldOfView;
            Vector3 dir = vector3 - camPos;
            float angle = Vector3.Angle(dir, camDir);
            return angle < fov;
        }
    }
}