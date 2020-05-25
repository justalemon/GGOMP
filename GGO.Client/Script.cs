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
            EventHandlers.Add("onClientGameTypeStart", new Action(OnClientGameTypeStart));
            EventHandlers.Add("playerSpawned", new Action<ExpandoObject, Vector3>(OnPlayerSpawn));
            EventHandlers.Add("ggo:onPlayerNotification", new Action<string>(OnPlayerNotification));
            EventHandlers.Add("onPlayerMatchStart", new Action<Player, Vector3, float>(OnPlayerMatchStart));

            // Set up our menus
            Main.SetUp();
            Main.Pool.RefreshIndex();
        }

        private async Task OnTickMenu()
        {
            // Process our NativeUI menus
            Main.Pool.ProcessMenus();

            // Open our menu if the Z key is pressed
            if (Game.IsControlJustReleased(1, Control.MultiplayerInfo))
            {
                Main.Menu.Visible = !Main.Menu.Visible;
            }
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
            // If the player is not in a game, trigger a server event
            if (!OnMatch)
            {
                TriggerServerEvent("ggo:onPlayerSpawnHUB");
            }

            // If the player is the GTA Online Male model, set some of the player visuals
            if (LocalPlayer.Character.Model == new Model("mp_m_freemode_01"))
            {
                API.SetPedComponentVariation(Game.Player.Character.Handle, 0, 0, 0, 2);  // Face
                API.SetPedComponentVariation(Game.Player.Character.Handle, 2, 11, 4, 2); // Hair
                API.SetPedComponentVariation(Game.Player.Character.Handle, 4, 1, 5, 2);  // Pants
                API.SetPedComponentVariation(Game.Player.Character.Handle, 6, 1, 0, 2);  // Shoes
                API.SetPedComponentVariation(Game.Player.Character.Handle, 11, 7, 2, 2); // Jacket
            }

            // And return to the player control
            API.SwitchInPlayer(LocalPlayer.Character.GetHashCode());
        }

        private void OnPlayerNotification(string Reason)
        {
            Messages.Phone("CHAR_FRANK_TREV_CONF", Reason);
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
