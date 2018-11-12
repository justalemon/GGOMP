using NativeUI;
using System.Drawing;

namespace GGO.Client.Menus
{
    /// <summary>
    /// Main Menu information and configuration.
    /// </summary>
    public static class Main
    {
        /// <summary>
        /// Pool of menus. Here is where they are stored.
        /// </summary>
        public static MenuPool Pool = new MenuPool();
        /// <summary>
        /// Main menu, where everything goes together.
        /// </summary>
        public static UIMenu Menu = new UIMenu("", "GGO Main Menu");

        /// <summary>
        /// Sets up the menus sets.
        /// </summary>
        public static void SetUp()
        {
            // Add the menus and submenus
            Pool.Add(Menu);

            // TODO: See why this ends up locking the client during FiveM loading screen

            // Store our Sprite for the menu images
            // Sprite UISprite = new Sprite("shopui_title_barber", "shopui_title_barber", PointF.Empty, SizeF.Empty);
            // And set this sprite on our windows
            // Menu.SetBannerType(UISprite);

            // Add our elements
            // Main Menu
            Menu.AddItem(Items.Close);

            // Add our events
            Menu.OnItemSelect += Events.OnItemSelect;
        }
    }
}
