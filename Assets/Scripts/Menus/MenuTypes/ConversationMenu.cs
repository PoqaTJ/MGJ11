using System;
using Dialogs;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Menus.MenuTypes
{
    public class ConversationMenu: MenuController
    {
        public override MenuType GetMenuType() => MenuType.ConversationMenu;
        
        [SerializeField] private Image _leftImage;
        [SerializeField] private Image _rightImage;
        [SerializeField] private TMP_Text _leftName;
        [SerializeField] private TMP_Text _rightName;
        [SerializeField] private TMP_Text dialogText;
        
        // temp hopefully
        [SerializeField] private Sprite _tomoyaSprite;
        [SerializeField] private Sprite _akariSprite;
        [SerializeField] private Sprite _butterflySprite;
        [SerializeField] private Sprite _bigbadSprite;
        
        private ConversationMenuContext _context;
        
        private int _index = 0;
        
        protected override void OnSetup(DialogContext context)
        {
            _context = context as ConversationMenuContext;
            ShowDialog();
        }

        private void ShowDialog()
        {
            DialogDefinition dialog = _context.Conversation.Dialogs[_index];
            DialogCharacter character = dialog.Character;
            DialogSide side = dialog.Side;

            switch (side)
            {
                case DialogSide.Left:
                    _leftImage.enabled = true;
                    _leftName.enabled = true;
                    _leftImage.color = new Color(1f, 1f, 1f, 1f);
                    _rightImage.color = new Color(1f, 1f, 1f, 0.5f);
                    _leftImage.sprite = GetImage(character);
                    _leftName.text = GetName(character);
                    break;
                case DialogSide.Right:
                    _rightImage.enabled = true;
                    _rightName.enabled = true;
                    _rightImage.color = new Color(1f, 1f, 1f, 1f);
                    _leftImage.color = new Color(1f, 1f, 1f, 0.5f);
                    _rightImage.sprite = GetImage(character);
                    _rightName.text = GetName(character);
                    break;
            }

            dialogText.text = dialog.Text;
        }

        private Sprite GetImage(DialogCharacter character)
        {
            Sprite ret = null;
            switch (character)
            {
                case DialogCharacter.Akari:
                    ret = _akariSprite;
                    break;
                case DialogCharacter.Tomoya:
                    ret = _tomoyaSprite;
                    break;
                case DialogCharacter.Butterfly:
                    ret = _butterflySprite;
                    break;
                case DialogCharacter.BigBad:
                    ret = _bigbadSprite;
                    break;
            }

            return ret;
        }

        private string GetName(DialogCharacter character)
        {
            return LocalizationSettings.StringDatabase.GetLocalizedString("Launch", character.ToString());
        }

        public void OnProgress()
        {
            if (++_index < _context.Conversation.Dialogs.Count)
            {
                ShowDialog();
            }
            else
            {
                ServiceLocator.Instance.MenuManager.HideTop();
                _context.OnFinish?.Invoke();
            }
        }
    }

    public class ConversationMenuContext : MenuController.DialogContext
    {
        public Action OnFinish;
        public ConversationDefinition Conversation;
    }
}