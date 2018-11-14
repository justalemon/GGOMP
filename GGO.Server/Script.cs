using CitizenFX.Core;
using CitizenFX.Core.Native;
using GGO.Shared;
using GGO.Shared.Properties;
using System.Threading.Tasks;

namespace GGO.Server
{
    public class ScriptServer : BaseScript
    {
        /// <summary>
        /// The current game status.
        /// </summary>
        public static GameStatus Status = GameStatus.WaitingForPlayers;

        public ScriptServer()
        {
            // Register our events, all of them
            Tick += OnTickCheckStart;
        }

        private async Task OnTickCheckStart()
        {
            // Store our wait time
            int WaitTime = API.GetConvarInt("ggo_gamestart", 1);

            // Create a list of players and store the player count and minimum for the game
            PlayerList Players = new PlayerList();
            int PlayerCount = API.GetNumPlayerIndices();
            int MinimumPlayers = API.GetConvarInt("ggo_minplayers", 1);
            
            // If the player count is higher or equal to the minimum and the current status is not "enough players"
            if (PlayerCount >= MinimumPlayers && Status != GameStatus.EnoughPlayers)
            {
                // Store the log message
                string Message = string.Format(Resources.MatchEnoughPlayers, API.GetConvarInt("ggo_gamestart", 1));
                // Write a note into the server console
                Debug.WriteLine(Message.Replace("~n~", " "));
                // Store the status on a variable
                Status = GameStatus.EnoughPlayers;
                // And notify all of the players
                NotifyPlayers(Message, false);
            }
            // If the player count is lower than the required one and the current status is "enough players"
            else if (PlayerCount < MinimumPlayers)
            {
                // Store the log message
                string Message = string.Format(Resources.MatchNotEnoughPlayers, MinimumPlayers - PlayerCount, API.GetConvarInt("ggo_gamestart", 1));
                // Write a note into the server console
                Debug.WriteLine(Message.Replace("~n~", " "));
                // Store the status on a variable
                Status = GameStatus.WaitingForPlayers;
                // And notify all of the players
                NotifyPlayers(Message, false);
            }
            // If none of the later match
            else if (Status == GameStatus.EnoughPlayers)
            {
                // Write a note into the server console
                Debug.WriteLine(Resources.MatchStartingUp.Replace("~n~", " "));
                // Store the status on a variable
                Status = GameStatus.WaitingForPlayers;
                // And notify all of the players
                NotifyPlayers(Resources.MatchStartingUp);
            }

            // Try again in the specified number of minutes (default: 1)
            await Delay(API.GetConvarInt("ggo_gamestart", 1) * 60 * 1000);
        }

        public void NotifyPlayers(string Message, bool Started = false)
        {
            // And notify all of the players
            foreach (Player NotifyTo in new PlayerList())
            {
                TriggerClientEvent(NotifyTo, "onMatchStart", false, Message);
            }
        }
    }
}
