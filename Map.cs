using CWDM.Extensions;
using GTA;
using GTA.Math;
using GTA.Native;
using System.Linq;

namespace CWDM
{
    public static class Map
    {
        public static bool MapPrepared = false;
        public static bool Electricity = false;

        public static Weather[] Weathers =
        {
            Weather.Clear,
            Weather.Clearing,
            Weather.Clouds,
            Weather.ExtraSunny,
            Weather.Foggy,
            Weather.Neutral,
            Weather.Overcast,
            Weather.Raining,
            Weather.Smog,
            Weather.ThunderStorm
        };

        public static Model[] LockedDoors =
        {
            new Model("v_ilev_gc_door03"),
            new Model("v_ilev_gc_door04")
        };

        public static Model[] ElectricDoors =
        {
            new Model("apa_prop_ss1_mpint_garage2"),
            new Model("hei_prop_bh1_08_mp_gar2"),
            new Model("hei_prop_bh1_09_mp_gar2"),
            new Model("hei_prop_com_mp_gar2"),
            new Model("hei_prop_dt1_20_mp_gar2"),
            new Model("hei_prop_sm_14_mp_gar2"),
            new Model("hei_prop_ss1_mpint_garage2"),
            new Model("hei_prop_station_gate"),
            new Model("imp_prop_impex_gate_01"),
            new Model("imp_prop_impex_gate_sm_13"),
            new Model("imp_prop_impex_gate_sm_15"),
            new Model("imp_prop_impexp_liftdoor_l"),
            new Model("imp_prop_impexp_liftdoor_r"),
            new Model("p_gar_door_01_s"),
            new Model("p_gar_door_02_s"),
            new Model("p_gar_door_03_s"),
            new Model("p_gate_prison_01_s"),
            new Model("p_gdoor1_s"),
            new Model("p_gdoortest_s"),
            new Model("p_sec_gate_01_s"),
            new Model("prop_abat_roller_static"),
            new Model("prop_abat_slide"),
            new Model("prop_bh1_08_mp_gar"),
            new Model("prop_bh1_09_mp_gar"),
            new Model("prop_biolab_g_door"),
            new Model("prop_ch_025c_g_door_01"),
            new Model("prop_ch2_05d_g_door"),
            new Model("prop_ch2_07b_20_g_door"),
            new Model("prop_ch2_09c_garage_door"),
            new Model("prop_com_gar_door_01"),
            new Model("prop_com_ls_door_01"),
            new Model("prop_conslift_door"),
            new Model("prop_facgate_01"),
            new Model("prop_facgate_01b"),
            new Model("prop_facgate_02_l"),
            new Model("prop_facgate_03_l"),
            new Model("prop_facgate_03_ld_l"),
            new Model("prop_facgate_03_ld_r"),
            new Model("prop_facgate_03_r"),
            new Model("prop_facgate_03b_l"),
            new Model("prop_facgate_03b_r"),
            new Model("prop_facgate_04_l"),
            new Model("prop_facgate_04_r"),
            new Model("prop_facgate_05_r"),
            new Model("prop_facgate_05_r_dam_l1"),
            new Model("prop_facgate_05_r_l1"),
            new Model("prop_facgate_06_l"),
            new Model("prop_facgate_06_r"),
            new Model("prop_facgate_07"),
            new Model("prop_facgate_07b"),
            new Model("prop_facgate_08"),
            new Model("prop_facgate_08_ld"),
            new Model("prop_gar_door_01"),
            new Model("prop_gar_door_02"),
            new Model("prop_gar_door_03"),
            new Model("prop_gar_door_03_ld"),
            new Model("prop_gar_door_04"),
            new Model("prop_gar_door_05"),
            new Model("prop_gar_door_05_l"),
            new Model("prop_gar_door_05_r"),
            new Model("prop_gar_door_a_01"),
            new Model("prop_gate_airport_01"),
            new Model("prop_gate_docks_ld"),
            new Model("prop_gate_military_01"),
            new Model("prop_gate_prison_01"),
            new Model("prop_gd_ch2_08"),
            new Model("prop_hw1_03_gardoor_01"),
            new Model("prop_id_21_gardoor_01"),
            new Model("prop_id_21_gardoor_02"),
            new Model("prop_id2_11_gdoor"),
            new Model("prop_ld_garaged_01"),
            new Model("prop_lrggate_02"),
            new Model("prop_lrggate_02_ld"),
            new Model("prop_lrggate_04a"),
            new Model("prop_lrggate_06a"),
            new Model("prop_sc1_06_gate_l"),
            new Model("prop_sc1_06_gate_r"),
            new Model("prop_sc1_21_g_door_01"),
            new Model("prop_sec_barier_01a"),
            new Model("prop_sec_barier_02a"),
            new Model("prop_sec_barier_02b"),
            new Model("prop_sec_barier_03a"),
            new Model("prop_sec_barier_03b"),
            new Model("prop_sec_barier_04a"),
            new Model("prop_sec_barier_04b"),
            new Model("prop_sec_barrier_ld_01a"),
            new Model("prop_sec_barrier_ld_02a"),
            new Model("prop_sec_gate_01b"),
            new Model("prop_sec_gate_01c"),
            new Model("prop_sec_gate_01d"),
            new Model("prop_section_garage_01"),
            new Model("prop_sluicegatel"),
            new Model("prop_sluicegater"),
            new Model("prop_sm_14_mp_gar"),
            new Model("prop_sm1_11_garaged"),
            new Model("prop_ss1_14_garage_door"),
            new Model("prop_ss1_mpint_garage"),
            new Model("prop_ss1_mpint_garage_cl"),
            new Model("v_ilev_bl_shutter1"),
            new Model("v_ilev_bl_shutter2"),
            new Model("v_ilev_carmod3door"),
            new Model("v_ilev_cor_doorlift01"),
            new Model("v_ilev_cor_doorlift02"),
            new Model("v_ilev_csr_garagedoor"),
            new Model("v_ilev_fh_slidingdoor"),
            new Model("v_ilev_garageliftdoor"),
            new Model("v_ilev_tow_doorlifta"),
            new Model("v_ilev_tow_doorliftb"),
            new Model("xm_prop_base_slide_door")
        };

        public static string[] KillScripts =
        {
            "abigail1",
            "abigail2",
            "achievement_controller",
            "activity_creator_prototype_launcher",
            "act_cinema",
            "af_intro_t_sandy",
            "agency_heist1",
            "agency_heist2",
            "agency_heist3a",
            "agency_heist3b",
            "agency_prep1",
            "agency_prep2amb",
            "aicover_test",
            "ainewengland_test",
            "altruist_cult",
            "ambientblimp",
            "ambient_diving",
            "ambient_mrsphilips",
            "ambient_solomon",
            "ambient_sonar",
            "ambient_tonya",
            "ambient_tonyacall",
            "ambient_tonyacall2",
            "ambient_tonyacall5",
            "ambient_ufos",
            "am_airstrike",
            "am_ammo_drop",
            "am_arena_shp",
            "am_armwrestling",
            "am_armwrestling_apartment",
            "am_armybase",
            "am_backup_heli",
            "am_boat_taxi",
            "am_bru_box",
            "am_car_mod_tut",
            "am_casino_limo",
            "am_casino_luxury_car",
            "am_casino_peds",
            "am_challenges",
            "am_contact_requests",
            "am_cp_collection",
            "am_crate_drop",
            "am_criminal_damage",
            "am_darts",
            "am_darts_apartment",
            "am_dead_drop",
            "am_destroy_veh",
            "am_distract_cops",
            "am_doors",
            "am_ferriswheel",
            "am_gang_call",
            "am_ga_pickups",
            "am_heist_int",
            "am_heli_taxi",
            "am_hold_up",
            "am_hot_property",
            "am_hot_target",
            "am_hunt_the_beast",
            "am_imp_exp",
            "am_joyrider",
            "am_kill_list",
            "am_king_of_the_castle",
            "am_launcher",
            "am_lester_cut",
            "am_lowrider_int",
            "am_mission_launch",
            "am_mp_arcade",
            "am_mp_arcade_claw_crane",
            "am_mp_arcade_fortune_teller",
            "am_mp_arcade_love_meter",
            "am_mp_arcade_peds",
            "am_mp_arc_cab_manager",
            "am_mp_arena_box",
            "am_mp_arena_garage",
            "am_mp_armory_aircraft",
            "am_mp_armory_truck",
            "am_mp_biker_warehouse",
            "am_mp_boardroom_seating",
            "am_mp_bunker",
            "am_mp_business_hub",
            "am_mp_carwash_launch",
            "am_mp_casino",
            "am_mp_casino_apartment",
            "am_mp_casino_valet_garage",
            "am_mp_creator_aircraft",
            "am_mp_creator_trailer",
            "am_mp_defunct_base",
            "am_mp_drone",
            "am_mp_garage_control",
            "am_mp_hacker_truck",
            "am_mp_hangar",
            "am_mp_ie_warehouse",
            "am_mp_nightclub",
            "am_mp_orbital_cannon",
            "am_mp_property_ext",
            "am_mp_property_int",
            "am_mp_rc_vehicle",
            "am_mp_shooting_range",
            "am_mp_smoking_activity",
            "am_mp_smpl_interior_ext",
            "am_mp_smpl_interior_int",
            "am_mp_vehicle_reward",
            "am_mp_vehicle_weapon",
            "am_mp_warehouse",
            "am_mp_yacht",
            "am_npc_invites",
            "am_pass_the_parcel",
            "am_penned_in",
            "am_penthouse_peds",
            "am_pi_menu",
            "am_plane_takedown",
            "am_prison",
            "am_prostitute",
            "am_rollercoaster",
            "am_rontrevor_cut",
            "am_taxi",
            "am_vehicle_spawn",
            "animal_controller",
            "apartment_minigame_launcher",
            "apparcadebusiness",
            "apparcadebusinesshub",
            "appbikerbusiness",
            "appbroadcast",
            "appbunkerbusiness",
            "appbusinesshub",
            "appcamera",
            "appchecklist",
            "appcontacts",
            "appcovertops",
            "appemail",
            "appextraction",
            "apphackertruck",
            "apphs_sleep",
            "appimportexport",
            "appinternet",
            "appjipmp",
            "appmedia",
            "appmpbossagency",
            "appmpemail",
            "appmpjoblistnew",
            "apporganiser",
            "apprepeatplay",
            "appsecurohack",
            "appsecuroserv",
            "appsettings",
            "appsidetask",
            "appsmuggler",
            "apptextmessage",
            "apptrackify",
            "appvlsi",
            "appzit",
            "arcade_seating",
            "arena_box_bench_seats",
            "arena_carmod",
            "arena_workshop_seats",
            "armenian1",
            "armenian2",
            "armenian3",
            "armory_aircraft_carmod",
            "assassin_bus",
            "assassin_construction",
            "assassin_hooker",
            "assassin_multi",
            "assassin_rankup",
            "assassin_valet",
            "atm_trigger",
            "audiotest",
            "autosave_controller",
            "bailbond1",
            "bailbond2",
            "bailbond3",
            "bailbond4",
            "bailbond_launcher",
            "barry1",
            "barry2",
            "barry3",
            "barry3a",
            "barry3c",
            "barry4",
            "base_carmod",
            "base_corridor_seats",
            "base_entrance_seats",
            "base_heist_seats",
            "base_lounge_seats",
            "base_quaters_seats",
            "base_reception_seats",
            "benchmark",
            "bigwheel",
            "bj",
            "blackjack",
            "blimptest",
            "blip_controller",
            "bootycallhandler",
            "bootycall_debug_controller",
            "buddydeathresponse",
            "bugstar_mission_export",
            "buildingsiteambience",
            "building_controller",
            "business_battles",
            "business_battles_defend",
            "business_battles_sell",
            "business_hub_carmod",
            "business_hub_garage_seats",
            "cablecar",
            "camera_test",
            "cam_coord_sender",
            "candidate_controller",
            "carmod_shop",
            "carsteal1",
            "carsteal2",
            "carsteal3",
            "carsteal4",
            "carwash1",
            "carwash2",
            "car_roof_test",
            "casinoroulette",
            "casino_bar_seating",
            "casino_exterior_seating",
            "casino_interior_seating",
            "casino_lucky_wheel",
            "casino_main_lounge_seating",
            "casino_penthouse_seating",
            "casino_slots",
            "celebrations",
            "celebration_editor",
            "cellphone_controller",
            "cellphone_flashhand",
            "charactergoals",
            "charanimtest",
            "cheat_controller",
            "chinese1",
            "chinese2",
            "chop",
            "clothes_shop_mp",
            "clothes_shop_sp",
            "code_controller",
            "combat_test",
            "comms_controller",
            "completionpercentage_controller",
            "component_checker",
            "context_controller",
            "controller_ambientarea",
            "controller_races",
            "controller_taxi",
            "controller_towing",
            "controller_trafficking",
            "coordinate_recorder",
            "country_race",
            "country_race_controller",
            "creation_startup",
            "creator",
            "custom_config",
            "cutscenemetrics",
            "cutscenesamples",
            "cutscene_test",
            "darts",
            "debug",
            "debug_app_select_screen",
            "debug_clone_outfit_testing",
            "debug_launcher",
            "debug_ped_data",
            "degenatron_games",
            "density_test",
            "dialogue_handler",
            "director_mode",
            "docks2asubhandler",
            "docks_heista",
            "docks_heistb",
            "docks_prep1",
            "docks_prep2b",
            "docks_setup",
            "dont_cross_the_line",
            "dreyfuss1",
            "drf1",
            "drf2",
            "drf3",
            "drf4",
            "drf5",
            "drunk",
            "drunk_controller",
            "dynamixtest",
            "email_controller",
            "emergencycall",
            "emergencycalllauncher",
            "epscars",
            "epsdesert",
            "epsilon1",
            "epsilon2",
            "epsilon3",
            "epsilon4",
            "epsilon5",
            "epsilon6",
            "epsilon7",
            "epsilon8",
            "epsilontract",
            "epsrobes",
            "error_listener",
            "error_thrower",
            "event_controller",
            "exile1",
            "exile2",
            "exile3",
            "exile_city_denial",
            "extreme1",
            "extreme2",
            "extreme3",
            "extreme4",
            "fairgroundhub",
            "fake_interiors",
            "fameorshame_eps",
            "fameorshame_eps_1",
            "fame_or_shame_set",
            "family1",
            "family1taxi",
            "family2",
            "family3",
            "family4",
            "family5",
            "family6",
            "family_scene_f0",
            "family_scene_f1",
            "family_scene_m",
            "family_scene_t0",
            "family_scene_t1",
            "fanatic1",
            "fanatic2",
            "fanatic3",
            "fbi1",
            "fbi2",
            "fbi3",
            "fbi4",
            "fbi4_intro",
            "fbi4_prep1",
            "fbi4_prep2",
            "fbi4_prep3",
            "fbi4_prep3amb",
            "fbi4_prep4",
            "fbi4_prep5",
            "fbi5a",
            "finalea",
            "finaleb",
            "finalec1",
            "finalec2",
            "finale_choice",
            "finale_credits",
            "finale_endgame",
            "finale_heist1",
            "finale_heist2a",
            "finale_heist2b",
            "finale_heist2_intro",
            "finale_heist_prepa",
            "finale_heist_prepb",
            "finale_heist_prepc",
            "finale_heist_prepd",
            "finale_heist_prepeamb",
            "finale_intro",
            "floating_help_controller",
            "flowintrotitle",
            "flowstartaccept",
            "flow_autoplay",
            "flow_controller",
            "flow_help",
            "flyunderbridges",
            "fmmc_launcher",
            "fmmc_playlist_controller",
            "fm_bj_race_controler",
            "fm_capture_creator",
            "fm_deathmatch_controler",
            "fm_deathmatch_creator",
            "fm_hideout_controler",
            "fm_hold_up_tut",
            "fm_horde_controler",
            "fm_impromptu_dm_controler",
            "fm_intro",
            "fm_intro_cut_dev",
            "fm_lts_creator",
            "fm_maintain_cloud_header_data",
            "fm_maintain_transition_players",
            "fm_main_menu",
            "fm_mission_controller",
            "fm_mission_creator",
            "fm_race_controler",
            "fm_race_creator",
            "fm_survival_controller",
            "fm_survival_creator",
            "forsalesigns",
            "fps_test",
            "fps_test_mag",
            "franklin0",
            "franklin1",
            "franklin2",
            "freemode",
            "freemode_clearglobals",
            "freemode_init",
            "friendactivity",
            "friends_controller",
            "friends_debug_controller",
            "fullmap_test",
            "fullmap_test_flow",
            "game_server_test",
            "gb_airfreight",
            "gb_amphibious_assault",
            "gb_assault",
            "gb_bank_job",
            "gb_bellybeast",
            "gb_biker_bad_deal",
            "gb_biker_burn_assets",
            "gb_biker_contraband_defend",
            "gb_biker_contraband_sell",
            "gb_biker_contract_killing",
            "gb_biker_criminal_mischief",
            "gb_biker_destroy_vans",
            "gb_biker_driveby_assassin",
            "gb_biker_free_prisoner",
            "gb_biker_joust",
            "gb_biker_last_respects",
            "gb_biker_race_p2p",
            "gb_biker_rescue_contact",
            "gb_biker_rippin_it_up",
            "gb_biker_safecracker",
            "gb_biker_search_and_destroy",
            "gb_biker_shuttle",
            "gb_biker_stand_your_ground",
            "gb_biker_steal_bikes",
            "gb_biker_target_rival",
            "gb_biker_unload_weapons",
            "gb_biker_wheelie_rider",
            "gb_carjacking",
            "gb_cashing_out",
            "gb_casino",
            "gb_casino_heist",
            "gb_casino_heist_planning",
            "gb_collect_money",
            "gb_contraband_buy",
            "gb_contraband_defend",
            "gb_contraband_sell",
            "gb_data_hack",
            "gb_deathmatch",
            "gb_delivery",
            "gb_finderskeepers",
            "gb_fivestar",
            "gb_fortified",
            "gb_fragile_goods",
            "gb_fully_loaded",
            "gb_gangops",
            "gb_gang_ops_planning",
            "gb_gunrunning",
            "gb_gunrunning_defend",
            "gb_gunrunning_delivery",
            "gb_headhunter",
            "gb_hunt_the_boss",
            "gb_ie_delivery_cutscene",
            "gb_illicit_goods_resupply",
            "gb_infiltration",
            "gb_jewel_store_grab",
            "gb_ploughed",
            "gb_point_to_point",
            "gb_ramped_up",
            "gb_rob_shop",
            "gb_salvage",
            "gb_security_van",
            "gb_sightseer",
            "gb_smuggler",
            "gb_stockpiling",
            "gb_target_pursuit",
            "gb_terminate",
            "gb_transporter",
            "gb_vehicle_export",
            "gb_velocity",
            "gb_yacht_rob",
            "general_test",
            "ggsm_arcade",
            "globals_fmmc_struct_registration",
            "golf",
            "golf_ai_foursome",
            "golf_ai_foursome_putting",
            "golf_mp",
            "gpb_andymoon",
            "gpb_baygor",
            "gpb_billbinder",
            "gpb_clinton",
            "gpb_griff",
            "gpb_jane",
            "gpb_jerome",
            "gpb_jesse",
            "gpb_mani",
            "gpb_mime",
            "gpb_pameladrake",
            "gpb_superhero",
            "gpb_tonya",
            "gpb_zombie",
            "grid_arcade_cabinet",
            "gtest_airplane",
            "gtest_avoidance",
            "gtest_boat",
            "gtest_divingfromcar",
            "gtest_divingfromcarwhilefleeing",
            "gtest_helicopter",
            "gtest_nearlymissedbycar",
            "gunclub_shop",
            "gunfighttest",
            "gunslinger_arcade",
            "hacker_truck_carmod",
            "hairdo_shop_mp",
            "hairdo_shop_sp",
            "hangar_carmod",
            "hao1",
            "headertest",
            "heatmap_test",
            "heatmap_test_flow",
            "heist_ctrl_agency",
            "heist_ctrl_docks",
            "heist_ctrl_finale",
            "heist_ctrl_jewel",
            "heist_ctrl_rural",
            "heli_gun",
            "heli_streaming",
            "hud_creator",
            "hunting1",
            "hunting2",
            "hunting_ambient",
            "idlewarper",
            "ingamehud",
            "initial",
            "jewelry_heist",
            "jewelry_prep1a",
            "jewelry_prep1b",
            "jewelry_prep2a",
            "jewelry_setup1",
            "josh1",
            "josh2",
            "josh3",
            "josh4",
            "lamar1",
            "laptop_trigger",
            "launcher_abigail",
            "launcher_barry",
            "launcher_basejumpheli",
            "launcher_basejumppack",
            "launcher_carwash",
            "launcher_darts",
            "launcher_dreyfuss",
            "launcher_epsilon",
            "launcher_extreme",
            "launcher_fanatic",
            "launcher_golf",
            "launcher_hao",
            "launcher_hunting",
            "launcher_hunting_ambient",
            "launcher_josh",
            "launcher_maude",
            "launcher_minute",
            "launcher_mrsphilips",
            "launcher_nigel",
            "launcher_offroadracing",
            "launcher_omega",
            "launcher_paparazzo",
            "launcher_pilotschool",
            "launcher_racing",
            "launcher_rampage",
            "launcher_range",
            "launcher_stunts",
            "launcher_tennis",
            "launcher_thelastone",
            "launcher_tonya",
            "launcher_triathlon",
            "launcher_yoga",
            "lester1",
            "lesterhandler",
            "letterscraps",
            "line_activation_test",
            "liverecorder",
            "locates_tester",
            "luxe_veh_activity",
            "magdemo",
            "magdemo2",
            "main",
            "maintransition",
            "main_install",
            "main_persistent",
            "martin1",
            "maude1",
            "maude_postbailbond",
            "me_amanda1",
            "me_jimmy1",
            "me_tracey1",
            "mg_race_to_point",
            "michael1",
            "michael2",
            "michael3",
            "michael4",
            "michael4leadout",
            "minigame_ending_stinger",
            "minigame_stats_tracker",
            "minute1",
            "minute2",
            "minute3",
            "missioniaaturret",
            "mission_race",
            "mission_repeat_controller",
            "mission_stat_alerter",
            "mission_stat_watcher",
            "mission_triggerer_a",
            "mission_triggerer_b",
            "mission_triggerer_c",
            "mission_triggerer_d",
            "mpstatsinit",
            "mptestbed",
            "mp_awards",
            "mp_bed_high",
            "mp_fm_registration",
            "mp_menuped",
            "mp_prop_global_block",
            "mp_prop_special_global_block",
            "mp_registration",
            "mp_save_game_global_block",
            "mp_unlocks",
            "mp_weapons",
            "mrsphilips1",
            "mrsphilips2",
            "murdermystery",
            "navmeshtest",
            "net_activity_creator_ui",
            "net_apartment_activity",
            "net_apartment_activity_light",
            "net_bot_brain",
            "net_bot_simplebrain",
            "net_cloud_mission_loader",
            "net_combat_soaktest",
            "net_jacking_soaktest",
            "net_rank_tunable_loader",
            "net_session_soaktest",
            "net_tunable_check",
            "nigel1",
            "nigel1a",
            "nigel1b",
            "nigel1c",
            "nigel1d",
            "nigel2",
            "nigel3",
            "nightclubpeds",
            "nightclub_ground_floor_seats",
            "nightclub_office_seats",
            "nightclub_vip_seats",
            "nodeviewer",
            "ob_abatdoor",
            "ob_abattoircut",
            "ob_airdancer",
            "ob_bong",
            "ob_cashregister",
            "ob_drinking_shots",
            "ob_foundry_cauldron",
            "ob_franklin_beer",
            "ob_franklin_tv",
            "ob_franklin_wine",
            "ob_huffing_gas",
            "ob_jukebox",
            "ob_mp_bed_high",
            "ob_mp_bed_low",
            "ob_mp_bed_med",
            "ob_mp_shower_med",
            "ob_mp_stripper",
            "ob_mr_raspberry_jam",
            "ob_poledancer",
            "ob_sofa_franklin",
            "ob_sofa_michael",
            "ob_telescope",
            "ob_tv",
            "ob_vend1",
            "ob_vend2",
            "ob_wheatgrass",
            "offroad_races",
            "omega1",
            "omega2",
            "paparazzo1",
            "paparazzo2",
            "paparazzo3",
            "paparazzo3a",
            "paparazzo3b",
            "paparazzo4",
            "paradise",
            "paradise2",
            "pausemenu",
            "pausemenu_example",
            "pausemenu_map",
            "pausemenu_multiplayer",
            "pausemenu_sp_repeat",
            "pb_busker",
            "pb_homeless",
            "pb_preacher",
            "pb_prostitute",
            "personal_carmod_shop",
            "photographymonkey",
            "photographywildlife",
            "physics_perf_test",
            "physics_perf_test_launcher",
            "pickuptest",
            "pickupvehicles",
            "pickup_controller",
            "pilot_school",
            "pilot_school_mp",
            "pi_menu",
            "placeholdermission",
            "placementtest",
            "planewarptest",
            "player_controller",
            "player_controller_b",
            "player_scene_ft_franklin1",
            "player_scene_f_lamgraff",
            "player_scene_f_lamtaunt",
            "player_scene_f_taxi",
            "player_scene_mf_traffic",
            "player_scene_m_cinema",
            "player_scene_m_fbi2",
            "player_scene_m_kids",
            "player_scene_m_shopping",
            "player_scene_t_bbfight",
            "player_scene_t_chasecar",
            "player_scene_t_insult",
            "player_scene_t_park",
            "player_scene_t_tie",
            "player_timetable_scene",
            "playthrough_builder",
            "pm_defend",
            "pm_delivery",
            "pm_gang_attack",
            "pm_plane_promotion",
            "pm_recover_stolen",
            "postkilled_bailbond2",
            "postrc_barry1and2",
            "postrc_barry4",
            "postrc_epsilon4",
            "postrc_nigel3",
            "profiler_registration",
            "prologue1",
            "prop_drop",
            "racetest",
            "rampage1",
            "rampage2",
            "rampage3",
            "rampage4",
            "rampage5",
            "rampage_controller",
            "randomchar_controller",
            "range_modern",
            "range_modern_mp",
            "replay_controller",
            "rerecord_recording",
            "respawn_controller",
            "restrictedareas",
            "re_abandonedcar",
            "re_accident",
            "re_armybase",
            "re_arrests",
            "re_atmrobbery",
            "re_bikethief",
            "re_border",
            "re_burials",
            "re_bus_tours",
            "re_cartheft",
            "re_chasethieves",
            "re_crashrescue",
            "re_cultshootout",
            "re_dealgonewrong",
            "re_domestic",
            "re_drunkdriver",
            "re_duel",
            "re_gangfight",
            "re_gang_intimidation",
            "re_getaway_driver",
            "re_hitch_lift",
            "re_homeland_security",
            "re_lossantosintl",
            "re_lured",
            "re_monkey",
            "re_mountdance",
            "re_muggings",
            "re_paparazzi",
            "re_prison",
            "re_prisonerlift",
            "re_prisonvanbreak",
            "re_rescuehostage",
            "re_seaplane",
            "re_securityvan",
            "re_shoprobbery",
            "re_snatched",
            "re_stag_do",
            "re_yetarian",
            "rng_output",
            "road_arcade",
            "rollercoaster",
            "rural_bank_heist",
            "rural_bank_prep1",
            "rural_bank_setup",
            "savegame_bed",
            "save_anywhere",
            "scaleformgraphictest",
            "scaleformminigametest",
            "scaleformprofiling",
            "scaleformtest",
            "scene_builder",
            "sclub_front_bouncer",
            "scripted_cam_editor",
            "scriptplayground",
            "scripttest1",
            "scripttest2",
            "scripttest3",
            "scripttest4",
            "script_metrics",
            "scroll_arcade_cabinet",
            "sctv",
            "sc_lb_global_block",
            "selector",
            "selector_example",
            "selling_short_1",
            "selling_short_2",
            "shooting_camera",
            "shoprobberies",
            "shop_controller",
            "shot_bikejump",
            "shrinkletter",
            "sh_intro_f_hills",
            "sh_intro_m_home",
            "smoketest",
            "social_controller",
            "solomon1",
            "solomon2",
            "solomon3",
            "spaceshipparts",
            "spawn_activities",
            "speech_reverb_tracker",
            "spmc_instancer",
            "spmc_preloader",
            "sp_dlc_registration",
            "sp_editor_mission_instance",
            "sp_menuped",
            "sp_pilotschool_reg",
            "standard_global_init",
            "standard_global_reg",
            "startup",
            "startup_install",
            "startup_locationtest",
            "startup_positioning",
            "startup_smoketest",
            "stats_controller",
            "stock_controller",
            "streaming",
            "stripclub",
            "stripclub_drinking",
            "stripclub_mp",
            "stripperhome",
            "stunt_plane_races",
            "tasklist_1",
            "tattoo_shop",
            "taxilauncher",
            "taxiservice",
            "taxitutorial",
            "taxi_clowncar",
            "taxi_cutyouin",
            "taxi_deadline",
            "taxi_followcar",
            "taxi_gotyounow",
            "taxi_gotyourback",
            "taxi_needexcitement",
            "taxi_procedural",
            "taxi_takeiteasy",
            "taxi_taketobest",
            "tempalpha",
            "temptest",
            "tennis",
            "tennis_ambient",
            "tennis_family",
            "tennis_network_mp",
            "test_startup",
            "thelastone",
            "three_card_poker",
            "timershud",
            "title_update_registration",
            "title_update_registration_2",
            "tonya1",
            "tonya2",
            "tonya3",
            "tonya4",
            "tonya5",
            "towing",
            "traffickingsettings",
            "traffickingteleport",
            "traffick_air",
            "traffick_ground",
            "train_create_widget",
            "train_tester",
            "trevor1",
            "trevor2",
            "trevor3",
            "trevor4",
            "triathlonsp",
            "tunables_registration",
            "tuneables_processing",
            "turret_cam_script",
            "ufo",
            "ugc_global_registration",
            "underwaterpickups",
            "utvc",
            "valentinerpreward2",
            "vehiclespawning",
            "vehicle_ai_test",
            "vehicle_force_widget",
            "vehicle_gen_controller",
            "vehicle_plate",
            "vehicle_stealth_mode",
            "veh_play_widget",
            "walking_ped",
            "wardrobe_mp",
            "wardrobe_sp",
            "weapon_audio_widget",
            "wizard_arcade",
            "wp_partyboombox",
            "xml_menus",
            "yoga"
        };

        public static void Prepare()
        {
            KillAllGameScripts();
            Game.MissionFlag = false;
            if (Function.Call<bool>(Hash.IS_CUTSCENE_ACTIVE))
            {
                Function.Call(Hash.STOP_CUTSCENE_IMMEDIATELY);
            }
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
            if (!Electricity)
            {
                World.SetBlackout(true);
            }
            else
            {
                World.SetBlackout(false);
            }
            Weather rndWeather = Weathers.GetRandomElementFromArray();
            World.TransitionToWeather(rndWeather, 0f);
            Function.Call(Hash.SET_CLOCK_TIME, 07, 00, 00);
            Function.Call(Hash.SET_CLOCK_DATE, 01, 01, 2020);
            foreach (Vector3 pos in Looting.StoreLocations)
            {
                Blip blip = World.CreateBlip(pos);
                blip.Sprite = BlipSprite.Store;
                blip.Name = "Store";
                blip.IsShortRange = true;
            }
            MapPrepared = true;
        }

        public static void Update()
        {
            Game.DisableControlThisFrame(0, GTA.Control.VehicleRadioWheel);
            Game.DisableControlThisFrame(0, GTA.Control.VehicleNextRadio);
            Game.DisableControlThisFrame(0, GTA.Control.VehiclePrevRadio);
            Game.DisableControlThisFrame(0, GTA.Control.RadioWheelLeftRight);
            Game.DisableControlThisFrame(0, GTA.Control.RadioWheelUpDown);
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
            UnlockAllDoors();
            KillAllGameScripts();
            ClearUpEntities();
        }

        public static void UnlockAllDoors()
        {
            Prop[] GetProps = World.GetNearbyProps(Game.Player.Character.Position, 50f);
            if (GetProps.Length > 0)
            {
                for (int i = 0; i < GetProps.Length; i++)
                {
                    if (GetProps[i] == null)
                    {
                        continue;
                    }
                    if (!Electricity)
                    {
                        if (ElectricDoors.Contains(GetProps[i].Model))
                        {
                            Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, GetProps[i].Model.Hash, GetProps[i].Position.X, GetProps[i].Position.Y, GetProps[i].Position.Z, true, 0, 0);
                        }
                        else
                        {
                            Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, GetProps[i].Model.Hash, GetProps[i].Position.X, GetProps[i].Position.Y, GetProps[i].Position.Z, false, 0, 0);
                        }
                    }
                    else
                    {
                        Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, GetProps[i].Model.Hash, GetProps[i].Position.X, GetProps[i].Position.Y, GetProps[i].Position.Z, false, 0, 0);
                    }
                    if (LockedDoors.Contains(GetProps[i].Model))
                    {
                        Function.Call(Hash.SET_STATE_OF_CLOSEST_DOOR_OF_TYPE, GetProps[i].Model.Hash, GetProps[i].Position.X, GetProps[i].Position.Y, GetProps[i].Position.Z, true, 0, 0);
                    }
                }
            }
        }

        public static void KillAllGameScripts()
        {
            foreach (string script in KillScripts)
            {
                script.TerminateScript();
            }
        }

        public static void ClearUpEntities()
        {
            if (Population.ZombiePeds.Count > 0)
            {
                for (int i = 0; i < Population.ZombiePeds.Count; i++)
                {
                    if (Population.ZombiePeds[i].pedEntity?.IsDead != false)
                    {
                        Population.ZombiePeds.RemoveAt(i);
                    }
                }
            }
            if (Population.AnimalPeds.Count > 0)
            {
                for (int i = 0; i < Population.AnimalPeds.Count; i++)
                {
                    if (Population.AnimalPeds[i].pedEntity?.IsDead != false)
                    {
                        Population.AnimalPeds.RemoveAt(i);
                    }
                }
            }
            if (Population.SurvivorPeds.Count > 0)
            {
                for (int i = 0; i < Population.SurvivorPeds.Count; i++)
                {
                    if (Population.SurvivorPeds[i].pedEntity?.IsDead != false)
                    {
                        Population.SurvivorPeds.RemoveAt(i);
                    }
                }
            }
            if (Population.Vehicles.Count > 0)
            {
                for (int i = 0; i < Population.Vehicles.Count; i++)
                {
                    if (Population.Vehicles[i].vehicle == null || Population.Vehicles[i].vehicle.Health == 0)
                    {
                        Population.Vehicles.RemoveAt(i);
                    }
                }
            }
            Ped[] all_ped = World.GetAllPeds();
            if (all_ped.Length > 0)
            {
                foreach (Ped ped in all_ped)
                {
                    if (!ped.IsAlive)
                    {
                        if (ped.CurrentBlip.Handle != 0)
                        {
                            ped.CurrentBlip.Remove();
                        }
                    }
                    if (ped.DistanceBetween(Game.Player.Character) > Population.MaxSpawnDistance && (ped.RelationshipGroup == Relationships.ZombieGroup || ped.RelationshipGroup == Relationships.AnimalGroup))
                    {
                        if (ped.CurrentBlip.Handle != 0)
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
                    if (!Character.PlayerVehicles.Exists(match: a => a == vehicle) || vehicle.PassengerCount > 0)
                    {
                        if (vehicle.DistanceBetween(Game.Player.Character) > Population.MaxSpawnDistance)
                        {
                            if (vehicle.CurrentBlip.Handle != 0)
                            {
                                vehicle.CurrentBlip.Remove();
                            }
                            vehicle.Delete();
                        }
                    }
                }
            }
        }
    }
}