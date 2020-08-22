using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;


namespace CWDM
{
    public class BlipController : Script
    {
        public BlipController()
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                RemoveAllUnrequiredBlips();
                PersonalVehicleBlip();
            }
        }

        public void PersonalVehicleBlip()
        {
            if (Character.playerVehicle != null)
            {
                if (Game.Player.Character.IsInVehicle(Character.playerVehicle))
                {
                    if (Character.playerVehicle.CurrentBlip.Exists())
                    {
                        Character.playerVehicle.CurrentBlip.Alpha = 0;
                    }
                }
                else
                {
                    if (Character.playerVehicle.CurrentBlip.Exists())
                    {
                        Character.playerVehicle.CurrentBlip.Alpha = 255;
                    }
                }
            }
            if (Character.playerVehicle != null && Character.playerVehicle.Health == 0)
            {
                Blip blip = Character.playerVehicle.CurrentBlip;
                blip.Remove();
                Character.playerVehicle = null;
            }
        }

        public void RemoveAllUnrequiredBlips()
        {
            Function.Call(Hash.SET_THIS_SCRIPT_CAN_REMOVE_BLIPS_CREATED_BY_ANY_SCRIPT, true);
            foreach (var blip in GetAllBlips())
            {
                if (blip.Sprite == BlipSprite.Blimp)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.PersonalVehicleCar)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.PersonalVehicleBike)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Sub)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Plane)
                {
                    blip.Alpha = 0;
                }
                if (blip.Sprite == BlipSprite.Helicopter)
                {
                    blip.Alpha = 0;
                }
            }
        }

        public static IEnumerable<Blip> GetAllBlips()
        {
            foreach (BlipSprite sprite in Enum.GetValues(typeof(BlipSprite)))
            {
                int Handle = Function.Call<int>(Hash.GET_FIRST_BLIP_INFO_ID, (int)sprite);
                while (Function.Call<bool>(Hash.DOES_BLIP_EXIST, Handle))
                {
                    yield return new Blip(Handle);
                    Handle = Function.Call<int>(Hash.GET_NEXT_BLIP_INFO_ID, (int)sprite);
                }
            }
        }
    }
}
