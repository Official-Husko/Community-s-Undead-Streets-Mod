using GTA;
using GTA.Native;

namespace CWDM
{
	public class Map
	{
		public static Weather[] weathers = { Weather.Clear, Weather.Clearing, Weather.Clouds, Weather.ExtraSunny,
											Weather.Foggy, Weather.Neutral, Weather.Overcast, Weather.Raining,
											Weather.Smog, Weather.ThunderStorm };

		public void Update()
		{
			Game.DisableControlThisFrame(85, GTA.Control.VehicleRadioWheel);
			Game.DisableControlThisFrame(81, GTA.Control.VehicleNextRadio);
			Game.DisableControlThisFrame(82, GTA.Control.VehiclePrevRadio);
			Game.DisableControlThisFrame(333, GTA.Control.RadioWheelLeftRight);
			Game.DisableControlThisFrame(332, GTA.Control.RadioWheelUpDown);
			Function.Call(Hash.DISABLE_CONTROL_ACTION, 2, 19, true);
			Function.Call(Hash.DESTROY_MOBILE_PHONE);
			Function.Call(Hash.SET_VEHICLE_POPULATION_BUDGET, 0);
			Function.Call(Hash.SET_PED_POPULATION_BUDGET, 0);
			Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
			Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
			Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
			Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
			Function.Call(Hash.STOP_ALARM, "PRISON_ALARMS", true);
			Function.Call(Hash.SET_AUDIO_FLAG, "PoliceScannerDisabled", true);
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prison");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_prison");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "gb_biker_free_prisoner");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prisonvanbreak");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_vehicle_spawn");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_taxi");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "audiotest");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "freemode");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_prisonerlift");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_prison");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_lossantosintl");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_armybase");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "restrictedareas");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "stripclub");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_gangfight");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_gang_intimidation");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "spawn_activities");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "am_vehiclespawn");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "traffick_air");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "traffick_ground");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "emergencycall");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "emergencycalllauncher");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "clothes_shop_sp");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "gb_rob_shop");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "gunclub_shop");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "hairdo_shop_sp");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_shoprobbery");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "shop_controller");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_crashrescue");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_rescuehostage");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "fm_mission_controller");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "player_scene_m_shopping");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "shoprobberies");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_atmrobbery");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "ob_vend1");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "ob_vend2");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "cellphone_controller");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "blip_controller");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "ambientblimp");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "blimptest");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "re_abandonedcar");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "director_mode");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "replay_controller");
			Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "rerecord_recording");
			Function.Call(Hash.START_AUDIO_SCENE, "FBI_HEIST_H5_MUTE_AMBIENCE_SCENE");
			Function.Call(Hash.START_AUDIO_SCENE, "MIC1_RADIO_DISABLE");
			Function.Call((Hash)0x808519373FD336A3, true);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_AFB_ALARM_SPEECH", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_CHILEAD_CABLE_CAR_LINE", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_01", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_02", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_DISTANT_CARS_ZONE_03", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_ALARM", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_GENERAL", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRYSIDE_PRISON_01_ANNOUNCER_WARNING", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_COUNTRY_SAWMILL", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DISTANT_SASQUATCH", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DISTANT_VEHICLES_CITY_CENTRE", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DLC_HEISTS_BIOLAB", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DLC_HEIST_BIOLAB_GARAGE", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DLC_HEIST_POLICE_STATION_BOOST", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_DMOD_TRAILER_01", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_EPSILONISM_01_HILLS", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_FBI_HEIST_SPRINKLER_FIRES_A_01", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_FIB_HEIST_JANITOR_WALKIE_TALKIE", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_PAPARAZZO_02_AMBIENCE", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_PORT_OF_LS_UNDERWATER_CREAKS", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_SAWMILL_CONVEYOR_01", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_SOL_1_FACTORY_AREA_CONSTRUCTIONS", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_SPECIAL_UFO_01", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_SPECIAL_UFO_02", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_SPECIAL_UFO_03", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_UNDERWATER_EXILE_01_PLANE_WRECK", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_YANKTON_CEMETARY", 0, 0);
			Function.Call(Hash.CLEAR_AMBIENT_ZONE_LIST_STATE, "AZ_strp3stge_SP", 0, 0);
			Function.Call(Hash.SUPPRESS_SHOCKING_EVENTS_NEXT_FRAME);
			Function.Call(Hash.SUPPRESS_AGITATION_EVENTS_NEXT_FRAME);
			Prop[] GetProps = World.GetNearbyProps(Game.Player.Character.Position, 10000f);
			for (int i = 0; i < GetProps.Length; i++)
			{
				Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, GetProps[i].Model.Hash, GetProps[i].Position.X, GetProps[i].Position.Y, GetProps[i].Position.Z, false, 0, 0);
			}
			Ped[] all_ped = World.GetAllPeds();
			if (all_ped.Length > 0)
			{
				foreach (Ped ped in all_ped)
				{
					if (ped.IsAlive != true)
					{
						if (ped.CurrentBlip.Exists())
						{
							ped.CurrentBlip.Remove();
						}
					}
					if (Extensions.DistanceBetween(ped, Game.Player.Character) > (Population.maxSpawnDistance + 30) && ped.RelationshipGroup == Relationships.ZombieGroup)
					{
						if (ped.CurrentBlip.Exists())
						{
							ped.CurrentBlip.Remove();
						}
						ped.Delete();
					}
					if (Extensions.DistanceBetween(ped, Game.Player.Character) > 1000 && ped.RelationshipGroup != Relationships.PlayerGroup)
					{
						if (ped.CurrentBlip.Exists())
						{
							ped.CurrentBlip.Remove();
						}
						ped.Delete();
					}
				}
			}
			Vehicle[] all_vecs = World.GetAllVehicles();
			if (all_vecs.Length > 0)
			{
				foreach (Vehicle vehicle in all_vecs)
				{
					if (vehicle != Character.playerVehicle)
					{
						if (Extensions.DistanceBetween(vehicle, Game.Player.Character) > (Population.maxSpawnDistance + 30))
						{
							if (vehicle.CurrentBlip.Exists())
							{
								vehicle.CurrentBlip.Remove();
							}
							vehicle.Delete();
						}
					}
				}
			}
		}

		public void Setup()
		{
			Function.Call(Hash.ADD_SCENARIO_BLOCKING_AREA, -10000.0f, -10000.0f, -1000.0f, 10000.0f, 10000.0f, 1000.0f, 0, 1, 1, 1);
			Function.Call(Hash.CLEAR_AREA, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 1000F, false, false, false, false);
			Function.Call(Hash.SET_CREATE_RANDOM_COPS, false);
			Function.Call(Hash.SET_RANDOM_BOATS, false);
			Function.Call(Hash.SET_RANDOM_TRAINS, false);
			Function.Call(Hash.SET_GARBAGE_TRUCKS, false);
			Function.Call(Hash.DELETE_ALL_TRAINS);
			Function.Call(Hash.SET_PED_POPULATION_BUDGET, 0);
			Function.Call(Hash.SET_VEHICLE_POPULATION_BUDGET, 0);
			Function.Call(Hash.SET_ALL_LOW_PRIORITY_VEHICLE_GENERATORS_ACTIVE, false);
			Function.Call(Hash.SET_NUMBER_OF_PARKED_VEHICLES, 0);
			Function.Call((Hash)0xF796359A959DF65D, false);
			Function.Call(Hash.DISABLE_VEHICLE_DISTANTLIGHTS, true);
			Ped[] all_ped = World.GetAllPeds();
			if (all_ped.Length > 0)
			{
				foreach (Ped ped in all_ped)
				{
					ped.Delete();
				}
			}
			Vehicle[] all_vecs = World.GetAllVehicles();
			if (all_vecs.Length > 0)
			{
				foreach (Vehicle vehicle in all_vecs)
				{
					vehicle.Delete();
				}
			}
			World.SetBlackout(true);
			Weather rndWeather = RandoMath.GetRandomElementFromArray(weathers);
			World.TransitionToWeather(rndWeather, 0f);
			Function.Call(Hash.SET_CLOCK_TIME, 07, 00, 00);
			Function.Call(Hash.SET_CLOCK_DATE, 01, 01, 20);
		}
	}
}