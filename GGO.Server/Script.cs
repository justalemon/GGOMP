using CitizenFX.Core;
using CitizenFX.Core.Native;
using GGO.Shared.Properties;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace GGO.Server
{
    public class ScriptServer : BaseScript
    {
        public ScriptServer()
        {
            // Register our events, all of them
            Tick += OnTickCheckStart;
        }

        private async Task OnTickCheckStart()
        {
            // Store our wait time
            int WaitTime = API.GetConvarInt("ggo_gamestart", 1);

            // Create a list of players and store the player count
            PlayerList Players = new PlayerList();
            int PlayerCount = API.GetNumPlayerIndices();

            // Check that the number of players is correct
            if (PlayerCount <= 3)
            {
                // Store the line that says what is going on
                string CheckLine = string.Format(Resources.MatchNotEnoughPlayers, 3 - PlayerCount, API.GetConvarInt("ggo_gamestart", 1));

                // Write a note into the server console
                Debug.WriteLine(CheckLine.Replace("~n~", " "));

                // And notify all of the players
                foreach (Player NotifyTo in new PlayerList())
                {
                    TriggerClientEvent(NotifyTo, "onMatchStart", false, CheckLine);
                }
            }

            // Try again in the specified number of minutes (default: 1)
            await Delay(API.GetConvarInt("ggo_gamestart", 1) * 60 * 1000);
        }
    }
}
