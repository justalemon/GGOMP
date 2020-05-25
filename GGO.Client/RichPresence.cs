using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace GGO.Client
{
    /// <summary>
    /// Class that deals with the rich presence information.
    /// </summary>
    public class RichPresence : BaseScript
    {
        #region Constructor

        public RichPresence()
        {
            // Set the Discord ID to "GGO for FiveM", use the white icon and add a message that the player is waiting
            API.SetDiscordAppId("509408274357944341");
            API.SetDiscordRichPresenceAsset("ggo_white"); // There is also ggo_black, but Elope said that white looks better
            API.SetRichPresence("Waiting for a Match on the HUB...");
        }

        #endregion
    }
}
