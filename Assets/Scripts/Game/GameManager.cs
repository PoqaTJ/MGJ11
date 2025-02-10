using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
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
            }
        }
    }

    public enum State
    {
        MainMenu,
        Gameplay
    }
}