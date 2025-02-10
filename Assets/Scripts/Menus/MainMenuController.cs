using Services;
using Game;
using UnityEngine;

namespace Menus
{
    public class MainMenuController: MonoBehaviour
    {
        public void StartPressed()
        {
            ServiceLocator.Instance.GameManager.SetState(State.Gameplay);
        }
    }
}