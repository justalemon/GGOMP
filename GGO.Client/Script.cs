using CitizenFX.Core;
using CitizenFX.Core.Native;
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

        public ScriptClient()
        {
            Tick += OnTick;
            EventHandlers.Add("onClientGameTypeStart", new Action(OnClientGameTypeStart));
            EventHandlers.Add("playerSpawned", new Action<ExpandoObject, Vector3>(OnPlayerSpawn));
        }

        private async Task OnTick()
        {
            if (!Convert.ToBoolean(API.GetConvar("ggo_npcs", "false")))
            {
                Vector3 PlayerPosition = LocalPlayer.Character.Position;

                API.SetVehicleDensityMultiplierThisFrame(0);
                API.SetPedDensityMultiplierThisFrame(0);
                API.SetRandomVehicleDensityMultiplierThisFrame(0);
                API.SetParkedVehicleDensityMultiplierThisFrame(0);
                API.SetScenarioPedDensityMultiplierThisFrame(0, 0);
                API.RemoveVehiclesFromGeneratorsInArea(PlayerPosition.X - 500, PlayerPosition.Y - 500, PlayerPosition.Z - 500, PlayerPosition.X + 500, PlayerPosition.Y + 500, PlayerPosition.Z + 500, 0);
                API.SetGarbageTrucks(false);
                API.SetRandomBoats(false);
            }

            await Delay(1);
        }

        private void OnClientGameTypeStart()
        {
            Exports["spawnmanager"].setAutoSpawnCallback(new Action(SpawnPlayerCallback));
            Exports["spawnmanager"].forceRespawn();

            API.RequestIpl("prologue06_int");

            foreach (string IPL in Data.NorthYankton)
            {
                // API.RequestIpl(IPL);
            }
        }

        private void OnPlayerSpawn(ExpandoObject Spawned, Vector3 Where)
        {
            if (LocalPlayer.Character.Model == new Model("mp_m_freemode_01"))
            {
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 0, 0, 0, 2);  // Face
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 2, 11, 4, 2); // Hair
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 4, 1, 5, 2);  // Pants
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 6, 1, 0, 2);  // Shoes
                API.SetPedComponentVariation(LocalPlayer.Character.GetHashCode(), 11, 7, 2, 2); // Jacket
            }
            
            if (API.IsPlayerSwitchInProgress())
            {
                API.ShowLoadingPrompt(1);
            }

            API.SwitchInPlayer(LocalPlayer.Character.GetHashCode());
        }

        private void SpawnPlayerCallback()
        {
            if (!API.IsPlayerSwitchInProgress())
            {
                API.SwitchOutPlayer(LocalPlayer.Character.GetHashCode(), 32, 1);
            }

            Location SpawnLocation = Data.HubSpawns[Generator.Next(Data.HubSpawns.Length)];
            Exports["spawnmanager"].spawnPlayer(new { x = SpawnLocation.X, y = SpawnLocation.Y, z = SpawnLocation.Z, heading = SpawnLocation.R, model = "mp_m_freemode_01" });
        }
    }
}
