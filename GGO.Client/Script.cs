using CitizenFX.Core;
using System.Threading.Tasks;

namespace GGO.Client
{
    public class ScriptClient : BaseScript
    {
        public ScriptClient()
        {
            Tick += OnTick;
        }

        private async Task OnTick()
        {
            await Delay(100);
        }
    }
}
