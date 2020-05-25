using CitizenFX.Core;
using CitizenFX.Core.Native;
using GGO.Shared;
using System;

namespace GGO.Client
{
    /// <summary>
    /// Script that handles the matches.
    /// </summary>
    public class Matches : BaseScript
    {
        #region Private Fields

        /// <summary>
        /// Generator of random values.
        /// </summary>
        private static readonly Random random = new Random();

        #endregion

        #region Public Properties


        /// <summary>
        /// If the Local Player is on a match.
        /// </summary>
        public static bool OnMatch { get; set; } = false;

        #endregion

        #region Constructor

        public Matches()
        {

        }

        #endregion

        #region Network Events

        /// <summary>
        /// Event triggered when the gamemode starts on the client.
        /// </summary>
        [EventHandler("onClientGameTypeStart")]
        public void OnClientGameTypeStart(string name)
        {
            // Place the screen black
            API.DoScreenFadeOut(0);
            // Start by disabling the clouds
            API.SetCloudHatOpacity(0);

            // If there are no player switches active, start it
            if (!API.IsPlayerSwitchInProgress())
            {
                API.SwitchOutPlayer(Game.Player.Character.Handle, 1, 1);
            }

            // This is no longer useable, but I'm gonna leave it just in case
            API.SetManualShutdownLoadingScreenNui(true);

            // Remove the black screen
            API.DoScreenFadeIn(0);
            // And unload the loading screen
            API.ShutdownLoadingScreen();
            API.ShutdownLoadingScreenNui();

            // Load North Yankton
            foreach (string IPL in Data.NorthYanktonIPLs)
            {
                API.RequestIpl(IPL);
            }

            // And spawn the player in one of the HUB locations
            Location SpawnLocation = Data.HubSpawns[random.Next(Data.HubSpawns.Length)];
            Exports["spawnmanager"].spawnPlayer(new { x = SpawnLocation.X, y = SpawnLocation.Y, z = SpawnLocation.Z, heading = SpawnLocation.R, model = "mp_m_freemode_01" });
            Exports["spawnmanager"].forceRespawn();
        }

        /// <summary>
        /// Event triggered when the player is spawned in the map.
        /// </summary>
        [EventHandler("playerSpawned")]
        private void OnPlayerSpawn(object spawnInfo)
        {
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

        #endregion
    }
}
