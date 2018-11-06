namespace GGO.Shared
{
    public class TeamSpawn
    {
        /// <summary>
        /// The name of the spawn.
        /// </summary>
        public string ZoneName;
        /// <summary>
        /// The points where the players should spawn.
        /// </summary>
        public Location[] SpawnPoints;

        public TeamSpawn(string Name, Location[] Spawns)
        {
            ZoneName = Name;
            SpawnPoints = Spawns;
        }
    }
}
