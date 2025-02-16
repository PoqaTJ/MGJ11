using System;
using Menus;
using Services;
using UnityEngine;

namespace Dialogs
{
    public class DialogManager: MonoBehaviour
    {
        public void StartConversation(ConversationDefinition definition, Action onFinish)
        {
            var context = new Menus.MenuTypes.ConversationMenuContext();
            context.Conversation = definition;

            ServiceLocator.Instance.MenuManager.Show(MenuType.ConversationMenu, context);
        }

        public void ShowQuip(QuipDefinition definition, Action onFinish)
        {
            
        }
    }
}