using GTA;

namespace CWDM
{
    public class Relationships
    {
        public static int PlayerGroup;
        public static int FriendlyGroup;
        public static int NeutralGroup;
        public static int HostileGroup;
        public static int ZombieGroup;
        public static int AnimalGroup;

        public static void SetRelationship(int group1, int group2, Relationship relationship)
        {
            World.SetRelationshipBetweenGroups(relationship, group1, group2);
            World.SetRelationshipBetweenGroups(relationship, group2, group1);
        }

        public static void Update()
        {
            PlayerGroup = World.AddRelationshipGroup("PLAYER");
            FriendlyGroup = World.AddRelationshipGroup("FRIENDLY");
            NeutralGroup = World.AddRelationshipGroup("NEUTRAL");
            HostileGroup = World.AddRelationshipGroup("HOSTILE");
            ZombieGroup = World.AddRelationshipGroup("ZOMBIE");
            AnimalGroup = World.AddRelationshipGroup("ANIMAL");
            SetRelationship(PlayerGroup, FriendlyGroup, Relationship.Like);
            SetRelationship(PlayerGroup, NeutralGroup, Relationship.Neutral);
            SetRelationship(PlayerGroup, HostileGroup, Relationship.Hate);
            SetRelationship(PlayerGroup, ZombieGroup, Relationship.Hate);
            SetRelationship(FriendlyGroup, NeutralGroup, Relationship.Neutral);
            SetRelationship(HostileGroup, NeutralGroup, Relationship.Neutral);
            SetRelationship(FriendlyGroup, HostileGroup, Relationship.Hate);
            SetRelationship(FriendlyGroup, ZombieGroup, Relationship.Hate);
            SetRelationship(HostileGroup, ZombieGroup, Relationship.Hate);
            SetRelationship(NeutralGroup, ZombieGroup, Relationship.Hate);
            SetRelationship(PlayerGroup, AnimalGroup, Relationship.Hate);
            SetRelationship(FriendlyGroup, AnimalGroup, Relationship.Hate);
            SetRelationship(NeutralGroup, AnimalGroup, Relationship.Hate);
            SetRelationship(HostileGroup, AnimalGroup, Relationship.Hate);
            SetRelationship(ZombieGroup, AnimalGroup, Relationship.Neutral);
            Game.Player.Character.RelationshipGroup = PlayerGroup;
        }
    }
}