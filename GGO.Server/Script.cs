using CitizenFX.Core;
using CitizenFX.Core.Native;
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
            // Store our current players
            PlayerList Players = new PlayerList();

            // Check that the number of players is correct
            if (API.GetNumPlayerIndices() <= 3)
            {
                // Write a note into the server console
                Debug.WriteLine("Not enough players to start a match. Player Count is " + API.GetNumPlayerIndices().ToString());

                // And notify all of the players
                foreach (Player NotifyTo in new PlayerList())
                {
                    TriggerClientEvent(NotifyTo, "onMatchStart", false, "There are not enough players to start the match.~n~Trying again in one more minute.");
                }
            }

            // Try again in one minute
            await Delay(60000);
        }
    }
}
