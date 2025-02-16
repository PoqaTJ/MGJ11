using UnityEngine;

namespace Menus
{
    public class MenuController : MonoBehaviour
    {
        public virtual MenuType GetMenuType()
        {
            return MenuType.PopupOneButton;
        }
        
        public void Setup(DialogContext context)
        {
            OnSetup(context);
        }

        protected virtual void OnSetup(DialogContext context)
        {
            
        }

        public virtual void Show()
        {
            OnShow();
        }

        protected virtual void OnShow()
        {
            
        }
        
        public virtual void Hide()
        {
            OnHide();
        }

        protected virtual void OnHide()
        {
            
        }

        public abstract class DialogContext
        {
        
        }
    }
}