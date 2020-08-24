using System;
using System.Linq;
using GTA;
using GTA.Native;

namespace CWDM.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleDoor GetRandomDoor(this Vehicle vehicle)
        {
            var vehicleDoors = vehicle.GetDoors();
            return vehicleDoors.GetRandomElementFromArray();
        }

        public static VehicleWindow GetRandomWindow(this Vehicle vehicle)
        {
            var vehicleWindows = (VehicleWindow[]) Enum.GetValues(typeof(VehicleWindow));
            vehicleWindows = (from v in vehicleWindows
                where Function.Call<bool>(Hash.IS_VEHICLE_WINDOW_INTACT, vehicle.Handle, (int) v)
                select v).ToArray();
            var window = vehicleWindows.GetRandomElementFromArray();
            return window;
        }

        public static BlipSprite GetVehicleTypeSprite(this Vehicle vehicle)
        {
            return (int) vehicle.ClassType == 8 ? BlipSprite.PersonalVehicleBike :
                (int) vehicle.ClassType == 14 ? BlipSprite.Boat :
                (int) vehicle.ClassType == 15 ? BlipSprite.Helicopter :
                (int) vehicle.ClassType == 16 ? BlipSprite.Plane : BlipSprite.PersonalVehicleCar;
        }
    }
}