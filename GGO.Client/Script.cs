using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;

namespace GGO.Client
{
    public class ScriptClient : BaseScript
    {
        public ScriptClient()
        {
            Tick += OnTick;
            EventHandlers.Add("onClientMapStart", new Action(OnClientMapStart));
            EventHandlers.Add("playerSpawned", new Action<Player, Vector3>(OnPlayerSpawned));
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

        private void OnClientMapStart()
        {
            Exports["spawnmanager"].setAutoSpawn(true);
            Exports["spawnmanager"].forceRespawn();
        }
    }
}
