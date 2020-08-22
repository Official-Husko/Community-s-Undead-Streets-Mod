using GTA;
using CWDM.Extensions;

namespace CWDM.Wrappers
{
    public class VehicleEntity
    {
        public Vehicle vehicle;

        public VehicleEntity(Vehicle vehicle)
        {
            AttachData(vehicle);
        }

        public void AttachData(Vehicle vehicle)
        {
            this.vehicle = vehicle;
        }

        public void Update()
        {
            Blip currentBlip = vehicle.CurrentBlip;
            if (vehicle.PassengerCount > 0)
            {
                if (vehicle.Passengers[0].RelationshipGroup == Relationships.FriendlyGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        Blip newBlip = vehicle.AddBlip();
                        newBlip.Sprite = vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Blue;
                        newBlip.Name = "Friendly Vehicle";
                    }
                }
                else if (vehicle.Passengers[0].RelationshipGroup == Relationships.NeutralGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        Blip newBlip = vehicle.AddBlip();
                        newBlip.Sprite = vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Yellow;
                        newBlip.Name = "Neutral Vehicle";
                    }
                }
                else if (vehicle.Passengers[0].RelationshipGroup == Relationships.HostileGroup)
                {
                    if (currentBlip.Handle == 0)
                    {
                        Blip newBlip = vehicle.AddBlip();
                        newBlip.Sprite = vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Red;
                        newBlip.Name = "Hostile Vehicle";
                    }
                }
                else if (vehicle.Passengers[0].RelationshipGroup == Relationships.PlayerGroup && !vehicle.Passengers[0].IsPlayer)
                {
                    if (currentBlip.Handle == 0)
                    {
                        Blip newBlip = vehicle.AddBlip();
                        newBlip.Sprite = vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Green;
                        newBlip.Name = "Group Vehicle";
                    }
                }
                else if (vehicle.Passengers[0].IsPlayer)
                {
                    if (currentBlip.Handle != 0)
                    {
                        currentBlip.Remove();
                    }
                }
            }
            else
            {
                if (Character.PlayerVehicles.Exists(match: a => a == vehicle))
                {
                    if (currentBlip.Handle == 0)
                    {
                        Blip newBlip = vehicle.AddBlip();
                        newBlip.Sprite = vehicle.GetVehicleTypeSprite();
                        newBlip.Color = BlipColor.Green;
                        newBlip.Name = "Personal Vehicle";
                    }
                }
                else
                {
                    if (currentBlip.Handle != 0)
                    {
                        currentBlip.Remove();
                    }
                }
            }
            if (vehicle.Health == 0)
            {
                if (currentBlip.Handle != 0)
                {
                    currentBlip.Remove();
                }
                if (Character.PlayerVehicles.Exists(match: a => a == vehicle))
                {
                    Character.PlayerVehicles.Remove(vehicle);
                }
            }
        }
    }
}
