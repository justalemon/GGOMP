using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;

namespace GGO.Server
{
    public class ScriptServer : BaseScript
    {
        public ScriptServer()
        {
            Tick += OnTick;
            EventHandlers.Add("playerConnecting", new Action<string, CallbackDelegate>(OnPlayerConnecting));
        }

        private async Task OnTick()
        {
            await Delay(100);
        }

        private void OnPlayerConnecting(string Name, CallbackDelegate SetReason)
        {

        }
    }
}
