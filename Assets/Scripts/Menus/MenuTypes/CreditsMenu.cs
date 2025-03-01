using Services;

namespace Menus.MenuTypes
{
    public class CreditsMenu: MenuController
    {
        public override MenuType GetMenuType() => MenuType.CreditsMenu;

        public void OnBack()
        {
            ServiceLocator.Instance.MenuManager.HideTop();
        }
    }
}
