using System;
using Services;
using UnityEngine;

namespace Dialogs
{
    public class ShowDialogOnTrigger: MonoBehaviour
    {
        [SerializeField] private ConversationDefinition _conversation;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                ShowConversation();
            }
        }

        private void ShowConversation()
        {
            ServiceLocator.Instance.DialogManager.StartConversation(_conversation, null);
            Destroy(gameObject);
        }
    }
}