using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menus.MenuTypes
{
    public class PopupMenuTwoButton : MenuController
    {
        [SerializeField] private TMP_Text titleString;
        [SerializeField] private TMP_Text bodyString;
        [SerializeField] private TMP_Text buttonLeftString;
        [SerializeField] private TMP_Text buttonRightString;

        public override MenuType GetMenuType() => MenuType.PopupTwoButtons;
        
        private PopupMenuTwoButtonContext context;

        protected override void OnSetup(DialogContext context)
        {
            this.context = context as PopupMenuTwoButtonContext;
            
            titleString.text = LocalizationSettings.StringDatabase.GetLocalizedString("Launch", this.context.titleLocString);
            bodyString.text = LocalizationSettings.StringDatabase.GetLocalizedString("Launch", this.context.bodyLocString);
            buttonLeftString.text = LocalizationSettings.StringDatabase.GetLocalizedString("Launch", this.context.buttonLeftLocString);
            buttonRightString.text = LocalizationSettings.StringDatabase.GetLocalizedString("Launch", this.context.buttonRightLocString);
        }

        protected override void OnShow()
        {
            base.OnShow();
        }

        public void OnClickLeft()
        {
            this.context.OnButtonLeftAction?.Invoke();
            ServiceLocator.Instance.MenuManager.HideTop();
        }
        
        public void OnClickRight()
        {
            this.context.OnButtonRightAction?.Invoke();
            ServiceLocator.Instance.MenuManager.HideTop();
        }
        
        protected override void OnHide()
        {
            base.OnHide();
        }

        public class PopupMenuTwoButtonContext : Menus.MenuController.DialogContext
        {
            public Action OnButtonLeftAction;
            public Action OnButtonRightAction;
            public string titleLocString;
            public string bodyLocString;
            public string buttonLeftLocString;
            public string buttonRightLocString;
        }
    }
}