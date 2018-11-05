using CitizenFX.Core;
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
        }

        private async Task OnTick()
        {
            await Delay(100);
        }

        private void OnClientMapStart()
        {
            Exports["spawnmanager"].setAutoSpawn(true);
            Exports["spawnmanager"].forceRespawn();
        }
    }
}
