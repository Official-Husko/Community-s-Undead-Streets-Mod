using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace CWDM
{
    public interface IWeapon
    {
        int Ammo
        {
            get;
            set;
        }

        WeaponHash Hash
        {
            get;
            set;
        }

        WeaponComponent[] GetComponents();
        void SetComponents(WeaponComponent[] value);
    }

    [Serializable]
    public class Weapon : IWeapon
    {
        public int Ammo
        {
            get;
            set;
        }

        public WeaponHash Hash
        {
            get;
            set;
        }

        private WeaponComponent[] components;

        public WeaponComponent[] GetComponents()
        {
            return components;
        }

        public void SetComponents(WeaponComponent[] value)
        {
            components = value;
        }

        public Weapon(int ammo, WeaponHash hash, WeaponComponent[] components)
        {
            Ammo = ammo;
            Hash = hash;
            SetComponents(components);
        }
    }

    [Serializable]
    public class PedData
    {
        public int Handle
        {
            get;
            set;
        }

        public int Hash
        {
            get;
            set;
        }

        public Vector3 Rotation
        {
            get;
            set;
        }

        public Vector3 Position
        {
            get;
            set;
        }

        public PedTasks Task
        {
            get;
            set;
        }

        public List<Weapon> Weapons
        {
            get;
            set;
        }

        public PedData(int handle, int hash, Vector3 rotation, Vector3 position, PedTasks task, List<Weapon> weapons)
        {
            Handle = handle;
            Hash = hash;
            Rotation = rotation;
            Position = position;
            Task = task;
            Weapons = weapons;
        }
    }

    [Serializable]
    public class PedCollection : IList<PedData>, ICollection<PedData>, IEnumerable<PedData>, IEnumerable
    {
        public delegate void ListChangedEvent(PedCollection sender);

        private readonly List<PedData> peds;

        public int Count => peds.Count;

        public bool IsReadOnly => ((ICollection<PedData>)peds).IsReadOnly;

        public PedData this[int index]
        {
            get
            {
                return peds[index];
            }
            set
            {
                peds[index] = value;
            }
        }

        [field: NonSerialized]
        public event ListChangedEvent ListChanged;

        public PedCollection()
        {
            peds = new List<PedData>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<PedData> GetEnumerator()
        {
            return peds.GetEnumerator();
        }

        public void Add(PedData item)
        {
            peds.Add(item);
            this.ListChanged?.Invoke(this);
        }

        public void Clear()
        {
            peds.Clear();
            ListChanged?.Invoke(this);
        }

        public bool Contains(PedData item)
        {
            return peds.Contains(item);
        }

        public void CopyTo(PedData[] array, int arrayIndex)
        {
            peds.CopyTo(array, arrayIndex);
        }

        public bool Remove(PedData item)
        {
            bool remove = peds.Remove(item);
            ListChanged?.Invoke(this);
            return remove;
        }

        public int IndexOf(PedData item)
        {
            return peds.IndexOf(item);
        }

        public void Insert(int index, PedData item)
        {
            peds.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            peds.RemoveAt(index);
        }
    }

    public class PlayerGroup : Script
    {
        public static PedCollection PlayerPedCollection = new PedCollection();
        public static List<Weapon> PlayerWeapons = new List<Weapon>();
        private readonly UIMenu mainMenu;
        private Ped currentGroupPed;
        private PedTasks taskApply;

        public PlayerGroup()
        {
            mainMenu = new UIMenu("Manage Group", "");
            Main.MasterMenuPool.Add(mainMenu);
            AddMenuTasks(mainMenu);
            AddMenuApplyToPed(mainMenu);
            AddMenuApplyToAll(mainMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.Purple)
            };
            mainMenu.SetBannerType(banner);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
        }

        public static void LoadPedFromPedData(PedData pedData)
        {
            Ped ped = World.CreatePed(pedData.Hash, pedData.Position);
            SurvivorPed survivorPed = new SurvivorPed(ped);
            Population.survivorList.Add(survivorPed);
            if (ped != null)
            {
                ped.Rotation = pedData.Rotation;
                pedData.Weapons.ForEach((Weapon w) => ped.Weapons.Give(w.Hash, w.Ammo, true, true));
                pedData.Handle = ped.Handle;
                ped.Recruit(Game.Player.Character);
                SetPedTasks(ped, pedData.Task);
            }
        }

        public static void SetPedTasks(Ped ped, PedTasks task)
        {
            ped.Task.ClearAll();
            if (task == PedTasks.Follow)
            {
            }
            else if (task == PedTasks.Guard)
            {
                ped.Task.GuardCurrentPosition();
            }
            else if (task == PedTasks.Wander)
            {
                ped.Task.WanderAround(ped.Position, 100f);
            }
            else if (task == PedTasks.None)
            {
                ped.Task.StandStill(-1);
            }
            else if (task == PedTasks.Leave)
            {
                ped.LeaveGroup();
                Blip currentBlip = ped.CurrentBlip;
                if (currentBlip.Handle != 0)
                {
                    currentBlip.Remove();
                }
                ped.RelationshipGroup = Relationships.FriendlyGroup;
                ped.Task.ClearAll();
                Blip blip = ped.AddBlip();
                blip.Color = BlipColor.Blue;
                blip.Scale = 0.65f;
                blip.Name = "Friendly";
                task = PedTasks.Wander;
                ped.Task.WanderAround(ped.Position, 100f);
            }
            Population.survivorList.Find(match: a => a.pedEntity == ped).tasks = task;
        }

        public static void SavePlayerWeapons()
        {
            IEnumerable<WeaponHash> hashes = from hash in (WeaponHash[])Enum.GetValues(typeof(WeaponHash))
                                             where Game.Player.Character.Weapons.HasWeapon(hash)
                                             select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent));
            List<Weapon> weapons = hashes.ToList().ConvertAll((WeaponHash hash) =>
            {
                GTA.Weapon weapon = Game.Player.Character.Weapons[hash];
                WeaponComponent[] components = (from h in componentHashes
                                                where weapon.IsComponentActive(h)
                                                select h).ToArray();
                return new Weapon(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            PlayerWeapons = weapons;
        }

        public static void LoadPlayerWeapons()
        {
            PlayerWeapons.ForEach((Weapon w) => Game.Player.Character.Weapons.Give(w.Hash, w.Ammo, true, true));
        }

        public static void AddPedData(Ped ped)
        {
            SurvivorPed survivorPed = Population.survivorList.Find(match: a => a.pedEntity == ped);
            PedTasks pedTasks = survivorPed.tasks;
            IEnumerable<WeaponHash> hashes = from hash in (WeaponHash[])Enum.GetValues(typeof(WeaponHash))
                                             where ped.Weapons.HasWeapon(hash)
                                             select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent));
            List<Weapon> weapons = hashes.ToList().ConvertAll((WeaponHash hash) =>
            {
                GTA.Weapon weapon = ped.Weapons[hash];
                WeaponComponent[] components = (from h in componentHashes
                                                where weapon.IsComponentActive(h)
                                                select h).ToArray();
                return new Weapon(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            PedData pedData = new PedData(ped.Handle, ped.Model.Hash, ped.Rotation, ped.Position, pedTasks, weapons);
            PlayerPedCollection.Add(pedData);
        }

        public void AddMenuTasks(UIMenu menu)
        {
            var tasks = new List<dynamic>
            {
                "None",
                "Wander",
                "Guard",
                "Follow",
                "Leave"
             };
            var newitem = new UIMenuListItem("Task", tasks, 0, "Select a task for your Group member(s)");
            menu.AddItem(newitem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    string taskString = item.Items[index].ToString();
                    if (taskString == "None")
                    {
                        taskApply = PedTasks.None;
                    }
                    else if (taskString == "Wander")
                    {
                        taskApply = PedTasks.Wander;
                    }
                    else if (taskString == "Guard")
                    {
                        taskApply = PedTasks.Guard;
                    }
                    else if (taskString == "Follow")
                    {
                        taskApply = PedTasks.Follow;
                    }
                    else if (taskString == "Leave")
                    {
                        taskApply = PedTasks.Leave;
                    }
                }
            };
        }

        public void AddMenuApplyToPed(UIMenu menu)
        {
            var newitem = new UIMenuItem("Set Task", "Set task for selected Group member");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    if (currentGroupPed == null)
                    {
                        UI.Notify("You do not have a ~p~Group ~w~member seleted");
                    }
                    else
                    {
                        SetPedTasks(currentGroupPed, taskApply);
                        UI.Notify("You have given seleted task to selected ~p~Group ~w~member");
                    }
                    mainMenu.Visible = !mainMenu.Visible;
                }
            };
        }

        public void AddMenuApplyToAll(UIMenu menu)
        {
            var newitem = new UIMenuItem("Set Task (All)", "Set task for all Group members");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    List<Ped> group = Game.Player.Character.CurrentPedGroup.ToList(false);
                    foreach (Ped ped in group)
                    {
                        PlayerGroup.SetPedTasks(ped, taskApply);
                    }
                    UI.Notify("You have given seleted task to all ~p~Group ~w~members");
                }
                mainMenu.Visible = !mainMenu.Visible;
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                Ped ped = World.GetClosest(Game.Player.Character.Position, World.GetNearbyPeds(Game.Player.Character, 1.5f));
                if (ped?.IsDead == false && ped.IsHuman && ped.RelationshipGroup == Relationships.FriendlyGroup)
                {
                    Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recruit");
                    Game.DisableControlThisFrame(2, GTA.Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                    {
                        try
                        {
                            ped.Recruit(Game.Player.Character);
                        }
                        catch (Exception x)
                        {
                            Debug.Log(x.ToString());
                        }
                    }
                }
                else if (ped?.IsDead == false && ped.IsHuman && ped.RelationshipGroup == Game.Player.Character.RelationshipGroup)
                {
                    Extensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to manage Group");
                    Game.DisableControlThisFrame(2, GTA.Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context) && !Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        currentGroupPed = ped;
                        mainMenu.Visible = !mainMenu.Visible;
                    }
                }
            }
        }
    }
}
