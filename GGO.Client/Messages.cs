using CitizenFX.Core.Native;

namespace GGO.Client
{
    public static class Messages
    {
        public static void Phone(string Contact, string Message, string Author = "GGO")
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(Message);
            API.SetNotificationMessage("CHAR_LESTER", "CHAR_LESTER", false, 1, "Server", "");
            API.DrawNotification(false, true);
        }
    }
}
