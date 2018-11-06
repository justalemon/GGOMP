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
        /// The defined places to spawn the players on the hub (aka the North Yankton bank).
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

        /// <summary>
        /// List of IPLs required to load and unload North Yankton.
        /// </summary>
        public static string[] NorthYanktonIPLs = new string[]
        {
            "prologue01", "prologue01c", "prologue01d", "prologue01e", "prologue01f",
            "prologue01g", "prologue01h", "prologue01i", "prologue01j", "prologue01k",
            "prologue01z", "prologue02", "prologue03", "prologue03b", "prologue04",
            "prologue04b", "prologue05", "prologue05b", "prologue06", "prologue06b",
            "prologue06_int", "prologuerd", "prologuerdb", "prologue_DistantLights",
            "prologue_LODLights", "prologue_m2_door"
        };
    }
}
