using NativeUI;

namespace GGO.Client.Menus
{
    public static class Events
    {
        public static void OnItemSelect(UIMenu Sender, UIMenuItem Item, int Index)
        {
            if (Item == Items.Close)
            {
                Main.Menu.Visible = false;
            }
        }
    }
}
