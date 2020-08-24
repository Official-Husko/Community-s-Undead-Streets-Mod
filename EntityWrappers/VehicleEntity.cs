using CWDM.Extensions;
using GTA;

namespace CWDM.EntityWrappers
{
    public class VehicleEntity
    {
        public Vehicle Vehicle;

        public VehicleEntity(Vehicle vehicle)
        {
            AttachData(vehicle);
        }

        public void AttachData(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public void Update()
        {
            var currentBlip = Vehicle.CurrentBlip;
            if (Vehicle.PassengerCount > 0)
            {
                if (Vehicle.Passengers[0].RelationshipGroup == Relationships.FriendlyGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        var newBlip = Vehicle.AddBlip();
                        newBlip.Sprite = Vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Blue;
                        newBlip.Name = "Friendly Vehicle";
                    }
                }
                else if (Vehicle.Passengers[0].RelationshipGroup == Relationships.NeutralGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        var newBlip = Vehicle.AddBlip();
                        newBlip.Sprite = Vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Yellow;
                        newBlip.Name = "Neutral Vehicle";
                    }
                }
                else if (Vehicle.Passengers[0].RelationshipGroup == Relationships.HostileGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        var newBlip = Vehicle.AddBlip();
                        newBlip.Sprite = Vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Red;
                        newBlip.Name = "Hostile Vehicle";
                    }
                }
                else if (Vehicle.Passengers[0].RelationshipGroup == Relationships.PlayerGroup &&
                         !Vehicle.Passengers[0].IsPlayer)
                {
                    if (currentBlip.Handle == 0)
                    {
                        var newBlip = Vehicle.AddBlip();
                        newBlip.Sprite = Vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Green;
                        newBlip.Name = "Group Vehicle";
                    }
                }
                else if (Vehicle.Passengers[0].IsPlayer)
                {
                    if (currentBlip.Handle != 0) currentBlip.Remove();
                }
            }
            else
            {
                if (Character.PlayerVehicles.Exists(a => a == Vehicle))
                {
                    if (currentBlip.Handle == 0)
                    {
                        var newBlip = Vehicle.AddBlip();
                        newBlip.Sprite = Vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Green;
                        newBlip.Name = "Personal Vehicle";
                    }
                }
                else
                {
                    if (currentBlip.Handle != 0) currentBlip.Remove();
                }
            }

            if (Vehicle.Health == 0)
            {
                if (currentBlip.Handle != 0) currentBlip.Remove();
                if (Character.PlayerVehicles.Exists(a => a == Vehicle)) Character.PlayerVehicles.Remove(Vehicle);
            }
        }
    }
}