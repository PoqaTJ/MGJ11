using System;
using Menus;
using Menus.Quips;
using Services;
using UnityEngine;

namespace Dialogs
{
    public class DialogManager: MonoBehaviour
    {
        [SerializeField] private QuipController _quipController;
        
        public void StartConversation(ConversationDefinition definition, Action onFinish)
        {
            var context = new Menus.MenuTypes.ConversationMenuContext();
            context.Conversation = definition;
            context.OnFinish = onFinish;

            ServiceLocator.Instance.MenuManager.Show(MenuType.ConversationMenu, context);
        }

        public void ShowQuip(QuipDefinition definition)
        {
            _quipController.Show(definition);
        }
    }
}