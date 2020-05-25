using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;

namespace GGO.Client
{
    /// <summary>
    /// Class that deals with the gameplay changes during a match.
    /// </summary>
    public class Gameplay : BaseScript
    {
        #region Constructor

        public Gameplay()
        {

        }

        #endregion

        #region Features

        /// <summary>
        /// Changes the size of the radar if the user needs it.
        /// </summary>
        [Tick]
        public async Task HandleRadar()
        {
            // If the user pressed Z/DOWN without having the game paused
            if (Game.IsEnabledControlJustPressed(0, Control.MultiplayerInfo) && !Game.IsPaused)
            {
                // Alternate the big map
                API.SetRadarBigmapEnabled(!API.IsBigmapActive(), false);
            }
        }

        /// <summary>
        /// Removes the population (NPCs) in the game world.
        /// </summary>.
        [Tick]
        public async Task RemovePopulation()
        {
            Vector3 position = Game.Player.Character.Position;
            API.SetVehicleDensityMultiplierThisFrame(0);
            API.SetPedDensityMultiplierThisFrame(0);
            API.SetRandomVehicleDensityMultiplierThisFrame(0);
            API.SetParkedVehicleDensityMultiplierThisFrame(0);
            API.SetScenarioPedDensityMultiplierThisFrame(0, 0);
            API.RemoveVehiclesFromGeneratorsInArea(position.X - 500, position.Y - 500, position.Z - 500, position.X + 500, position.Y + 500, position.Z + 500, 0);
            API.SetGarbageTrucks(false);
            API.SetRandomBoats(false);
        }

        #endregion
    }
}
