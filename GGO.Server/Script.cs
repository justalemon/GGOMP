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
            EventHandlers.Add("playerConnecting", new Action<string, CallbackDelegate, ExpandoObject>(OnPlayerConnecting));
            EventHandlers.Add("playerDropped", new Action<string, Player>(OnPlayerDropped));
            EventHandlers.Add("onPlayerDied", new Action<Player, string, Vector3>(OnPlayerDied));
            EventHandlers.Add("onPlayerKilled", new Action<Player, Player, string, Vector3>(OnPlayerKilled));
            EventHandlers.Add("playerSpawned", new Action<Player, Vector3>(OnPlayerSpawned));
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

        private void OnPlayerConnecting(string Name, CallbackDelegate SetReason, ExpandoObject TempPlayer)
        {

        }

        private void OnPlayerDropped(string Reason, Player Source)
        {

        }

        private void OnPlayerDied(Player DeadPlayer, string Reason, Vector3 Position)
        {

        }

        private void OnPlayerKilled(Player DeadPlayer, Player Killer, string Reason, Vector3 Position)
        {

        }

        private void OnPlayerSpawned(Player Spawned, Vector3 Where)
        {

        }
    }
}
