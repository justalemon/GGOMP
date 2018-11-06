namespace GGO.Shared
{
    public static class Data
    {
        /// <summary>
        /// The list of team spawns.
        /// </summary>
        public static TeamSpawn[] DefinedSpawns = new TeamSpawn[]
        {
            new TeamSpawn("Los Santos International Airport", new Location[] {
                new Location(-1030f, -2740f, 14f, 330f),
                new Location(-1035f, -2740f, 14f, 330f),
                new Location(-1040f, -2740f, 14f, 330f),
                new Location(-1045f, -2740f, 14f, 330f),
                new Location(-1050f, -2740f, 14f, 330f) })
        };

        /// <summary>
        /// The defined places to spawn the players on the hub.
        /// </summary>
        public static Location[] HubSpawns = new Location[]
        {
            new Location(5309.5f, -5213, 83.5f, 0), new Location(5314.5f, -5211.7f, 83.5f, 90),
            new Location(5314.5f, -5206.6f, 83.5f, 81.5f), new Location(5308.5f, -5197.4f, 83.5f, 135.7f),
            new Location(5295, -5198.7f, 83.5f, 0), new Location(5292, -5189.6f, 83.5f, 269),
            new Location(5294.6f, -5181, 83.5f, 264.5f), new Location(5301.5f, -5176.1f, 83.5f, 260),
            new Location(5309, -5177, 83.5f, 180), new Location(5318.3f, -5175, 83.5f, 93.4f),
            new Location(5319.6f, -5181, 83.5f, 90), new Location(5318.3f, -5184, 83.5f, 0),
            new Location(5313.3f, -5182.6f, 83.5f, 0), new Location(5303.3f, -5181.6f, 83.52f, 354.77f)
        };
    }
}
