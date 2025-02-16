using Menus;
using Menus.MenuTypes;
using Services;

namespace Game
{
    public class PowerupWallJump: Collectable
    {
        protected override void OnContact()
        {
            var context = new PopupMenuOneButton.PopupMenuOneButtonContext();
            context.titleLocString = "dialog-powerup-wall-jump-title";
            context.bodyLocString = "dialog-powerup-wall-jump-body";
            context.buttonLocString = "dialog-powerup-wall-jump-button";
            
            ServiceLocator.Instance.GameManager.UnlockWallJump();
            ServiceLocator.Instance.MenuManager.Show(MenuType.PopupOneButton, context);
        }
    }
}