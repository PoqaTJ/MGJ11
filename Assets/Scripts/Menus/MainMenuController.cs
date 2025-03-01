using Services;
using Game;
using Menus.MenuTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Menus
{
    public class MainMenuController: MonoBehaviour
    {
        [SerializeField] private GameObject _startButton;
        [SerializeField] private GameObject _continueButton;
        [SerializeField] private Button _deleteSaveButton;
        
        private void Awake()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool saveExists = ServiceLocator.Instance.SaveManager.WatchedIntro;
            if (saveExists)
            {
                _startButton.SetActive(false);                
                _continueButton.SetActive(true);
                _deleteSaveButton.interactable = true;
            }
            else
            {                
                _startButton.SetActive(true);                
                _continueButton.SetActive(false); 
                _deleteSaveButton.interactable = false;
            }
        }

        public void StartPressed()
        {
            State state = ServiceLocator.Instance.SaveManager.WatchedIntro ? State.Gameplay : State.Intro;
            ServiceLocator.Instance.GameManager.SetState(state);
        }

        public void CreditsPressed()
        {
            ServiceLocator.Instance.GameManager.SetState(State.Credits);
        }

        public void ResetGameStatePressed()
        {
            PopupMenuTwoButton.PopupMenuTwoButtonContext context = new PopupMenuTwoButton.PopupMenuTwoButtonContext();
            context.titleLocString = "delete-save-title";
            context.bodyLocString = "delete-save-body";
            context.buttonLeftLocString = "delete-save-cancel";
            context.buttonRightLocString = "delete-save-confirm";
            context.OnButtonRightAction = () =>
            {
                ServiceLocator.Instance.SaveManager.Destroy();
                UpdateButtons();
            };
            ServiceLocator.Instance.MenuManager.Show(MenuType.PopupTwoButtons, context);
        }
    }
}