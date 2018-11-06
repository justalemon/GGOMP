using CitizenFX.Core;
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
            Tick += OnTick;
            EventHandlers.Add("playerConnecting", new Action<string, CallbackDelegate, ExpandoObject>(OnPlayerConnecting));
            EventHandlers.Add("playerDropped", new Action<string, Player>(OnPlayerDropped));
            EventHandlers.Add("onPlayerDied", new Action<Player, string, Vector3>(OnPlayerDied));
            EventHandlers.Add("onPlayerKilled", new Action<Player, Player, string, Vector3>(OnPlayerKilled));
            EventHandlers.Add("playerSpawned", new Action<Player, Vector3>(OnPlayerSpawned));
        }

        private async Task OnTick()
        {
            // Wait 1ms just in case
            await Delay(1);
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
