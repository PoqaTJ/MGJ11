using UnityEngine;

namespace Menus
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] public readonly MenuType MenuType;
        
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