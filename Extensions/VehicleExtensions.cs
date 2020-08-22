using GTA;
using GTA.Native;
using System;
using System.Linq;

namespace CWDM.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleDoor GetRandomDoor(this Vehicle vehicle)
        {
            VehicleDoor[] vehicleDoors = vehicle.GetDoors();
            VehicleDoor door = MathExtensions.GetRandomElementFromArray(vehicleDoors);
            return door;
        }

        public static VehicleWindow GetRandomWindow(this Vehicle vehicle)
        {
            VehicleWindow[] vehicleWindows = (VehicleWindow[])Enum.GetValues(typeof(VehicleWindow));
            vehicleWindows = (from v in vehicleWindows
                              where Function.Call<bool>(Hash.IS_VEHICLE_WINDOW_INTACT, vehicle.Handle, (int)v)
                              select v).ToArray();
            VehicleWindow window = MathExtensions.GetRandomElementFromArray(vehicleWindows);
            return window;
        }

        public static BlipSprite GetVehicleTypeSprite(this Vehicle vehicle)
        {
            return ((int)vehicle.ClassType == 8) ? BlipSprite.PersonalVehicleBike : (((int)vehicle.ClassType == 14) ? BlipSprite.Boat : (((int)vehicle.ClassType == 15) ? BlipSprite.Helicopter : (((int)vehicle.ClassType == 16) ? BlipSprite.Plane : BlipSprite.PersonalVehicleCar)));
        }
    }
}
