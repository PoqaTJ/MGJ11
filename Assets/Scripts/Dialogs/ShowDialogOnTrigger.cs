using System;
using Services;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogs
{
    public class ShowDialogOnTrigger: MonoBehaviour
    {
        [SerializeField] private ConversationDefinition _conversation;
        [SerializeField] private UnityEvent _onDoneEvent;

        void Start()
        {
            if (ServiceLocator.Instance.SaveManager.WatchedIntro)
            {
                Debug.Log("Temp way to spawn");
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                ShowConversation();
            }
        }

        private void ShowConversation()
        {
            ServiceLocator.Instance.DialogManager.StartConversation(_conversation, () =>
            {
                _onDoneEvent?.Invoke();                
            });
            Destroy(gameObject);
        }
    }
}