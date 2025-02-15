using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region events

        public Action OnDoublejumpUnlocked;
        public Action OnWalljumpUnlocked;

        #endregion
        
        public State CurrentState { get; private set; }
        
        public void SetState(State state)
        {
            if (CurrentState == state)
            {
                Debug.LogError($"Tried to set state to {state}, but that was already the current state.");
                return;
            }

            Debug.Log($"Setting state to {state}.");
            switch (state)
            {
                case State.MainMenu:
                    SceneManager.LoadSceneAsync("Main");
                    break;
                case State.Gameplay:
                    SceneManager.LoadSceneAsync("Gameplay");
                    break;
                case State.Debug:
                    SceneManager.LoadSceneAsync("Debug");
                    break;
            }
        }

        public void UnlockDoubleJump()
        {
            OnDoublejumpUnlocked?.Invoke();
        }

        public void UnlockWallJump()
        {
            OnWalljumpUnlocked?.Invoke();
        }
    }

    public enum State
    {
        MainMenu,
        Gameplay,
        Debug
    }
}