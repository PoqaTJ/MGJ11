using Dialogs;
using Services;
using UnityEngine;

namespace Game
{
    public class TransitionToGameplayTrigger: MonoBehaviour
    {
        [SerializeField] private ConversationDefinition _conversation;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_conversation)
            {
                ServiceLocator.Instance.DialogManager.StartConversation(_conversation, LoadGameplay);
            }
            else
            {
                LoadGameplay();
            }
        }

        private void LoadGameplay()
        {
            ServiceLocator.Instance.SaveManager.Level = 1;
            ServiceLocator.Instance.GameManager.SetState(State.Gameplay);
        }
    }
}