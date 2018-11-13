using CitizenFX.Core;
using CitizenFX.Core.Native;
using GGO.Client.Menus;
using GGO.Shared;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace GGO.Client
{
    public class ScriptClient : BaseScript
    {
        /// <summary>
        /// Generator of random values.
        /// </summary>
        public static Random Generator = new Random();
        /// <summary>
        /// If the HUD should be disabled during the next frame.
        /// </summary>
        public static bool DisableHud = false;
        /// <summary>
        /// If the Local Player is on a match.
        /// </summary>
        public static bool OnMatch = false;

        public ScriptClient()
        {
            // Add our tick function8 and our events for when the client-side starts and when the player has spawned
            Tick += OnTickMenu;
            Tick += OnTick;
            EventHandlers.Add("onClientGameTypeStart", new Action(OnClientGameTypeStart));
            EventHandlers.Add("playerSpawned", new Action<ExpandoObject, Vector3>(OnPlayerSpawn));
            EventHandlers.Add("onMatchStart", new Action<bool, string>(OnMatchStart));
            EventHandlers.Add("onPlayerMatchStart", new Action<Player, Vector3, float>(OnPlayerMatchStart));

            // Set up our menus
            Main.SetUp();
            Main.Pool.RefreshIndex();

            // Set the Discord ID to "GGO for FiveM", use the white icon and add a message that the player is waiting
            API.SetDiscordAppId("509408274357944341");
            API.SetDiscordRichPresenceAsset("ggo_white"); // There is also ggo_black, but Elope said that white looks better
            API.SetRichPresence("Waiting for a Match on the HUB...");
        }

        private async Task OnTickMenu()
        {
            // Process our NativeUI menus
            Main.Pool.ProcessMenus();

            // Open our menu if the Z key is pressed
            if (Game.IsControlJustReleased(1, Control.CharacterWheel))
            {
                Main.Menu.Visible = !Main.Menu.Visible;
            }
        }

        private async Task OnTick()
        {
            // Disable traffic and peds to make somewhat of a ghost town
            Vector3 PlayerPosition = LocalPlayer.Character.Position;
            API.SetVehicleDensityMultiplierThisFrame(0);
            API.SetPedDensityMultiplierThisFrame(0);
            API.SetRandomVehicleDensityMultiplierThisFrame(0);
            API.SetParkedVehicleDensityMultiplierThisFrame(0);
            API.SetScenarioPedDensityMultiplierThisFrame(0, 0);
            API.RemoveVehiclesFromGeneratorsInArea(PlayerPosition.X - 500, PlayerPosition.Y - 500, PlayerPosition.Z - 500, PlayerPosition.X + 500, PlayerPosition.Y + 500, PlayerPosition.Z + 500, 0);
            API.SetGarbageTrucks(false);
            API.SetRandomBoats(false);

            // Disable the HUD if is required
            if (DisableHud)
            {
                API.HideHudAndRadarThisFrame();
            }

            // Wait 1ms just in case
            await Delay(0);
        }

        private void OnClientGameTypeStart()
        {
            // Enable the manual control of the FiveM loading screen
            API.SetManualShutdownLoadingScreenNui(true);

            // Load all of the IPLs from North Yankton to also see the map on the switch scene
            foreach (string IPL in Data.NorthYanktonIPLs)
            {
                API.RequestIpl(IPL);
            }

            // Disable the clouds on the switch screen
            API.SetCloudHatOpacity(0);

            // Do a Fade Out to avoid showing something ugly during the loading and spawn
            API.DoScreenFadeOut(0);

            // If there is not a player switch active
            if (!API.IsPlayerSwitchInProgress())
            {
                // Switch out of the player location
                API.SwitchOutPlayer(LocalPlayer.Character.GetHashCode(), 0, 2);
            }

            // Close the loading screen for the game data (aka the FiveM loading screen)
            API.ShutdownLoadingScreen();
            API.ShutdownLoadingScreenNui();

            // And return to the game window
            API.DoScreenFadeIn(500);

            // Spawn the player at one of the random hub spawns
            Location SpawnLocation = Data.HubSpawns[Generator.Next(Data.HubSpawns.Length)];
            Exports["spawnmanager"].spawnPlayer(new { x = SpawnLocation.X, y = SpawnLocation.Y, z = SpawnLocation.Z, heading = SpawnLocation.R, model = "mp_m_freemode_01" });
            Exports["spawnmanager"].forceRespawn();
        }

        private void OnPlayerSpawn(ExpandoObject Spawned, Vector3 Where)
        {
            // If the player is the GTA Online Male model, set some of the player visuals
            if (LocalPlayer.Character.Model == new Model("mp_m_freemode_01"))
            {
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 0, 0, 0, 2);  // Face
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 2, 11, 4, 2); // Hair
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 4, 1, 5, 2);  // Pants
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 6, 1, 0, 2);  // Shoes
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 11, 7, 2, 2); // Jacket
            }

            // And return to the player control
            API.SwitchInPlayer(LocalPlayer.Character.GetHashCode());
        }

        private void OnMatchStart(bool Started, string Reason)
        {
            // If the match has not started, show a noficiation
            if (!Started)
            {
                Messages.Phone("CHAR_FRANK_TREV_CONF", Reason);
            }
        }

        private void OnPlayerMatchStart(Player Spawned, Vector3 Location, float Heading)
        {
            // Return if the player does not match
            if (Spawned != LocalPlayer)
            {
                return;
            }

            // Switch out of the player location/the hub
            API.SwitchOutPlayer(LocalPlayer.Character.GetHashCode(), 0, 2);

            // Respawn the player on the new location
            Exports["spawnmanager"].spawnPlayer(new { x = Location.X, y = Location.Y, z = Location.Z, heading = Heading, model = "mp_m_freemode_01" });
            Exports["spawnmanager"].forceRespawn();

            // Enable the check to show that the player is on a match
            OnMatch = true;

            // Finally, unload North Yankton
            foreach (string IPL in Data.NorthYanktonIPLs)
            {
                API.RemoveIpl(IPL);
            }
        }
    }
}
