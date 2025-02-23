using Services;
using Game;
using Menus.MenuTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Menus
{
    public class MainMenuController: MonoBehaviour
    {
        [SerializeField] private TMP_Text _startButtonText;
        private string _startLocTag = "mm-button-start2";
        private string _continueLocTag = "mm-button-continue2";

        private void Awake()
        {
            UpdateLoc();
        }

        private void UpdateLoc()
        {
            string tag = ServiceLocator.Instance.SaveManager.WatchedIntro ? _continueLocTag : _startLocTag;
            _startButtonText.text = LocalizationSettings.StringDatabase.GetLocalizedString("launch", tag);
        }

        public void StartPressed()
        {
            State state = ServiceLocator.Instance.SaveManager.WatchedIntro ? State.Gameplay : State.Intro;
            ServiceLocator.Instance.GameManager.SetState(state);
        }

        public void DebugPressed()
        {
            ServiceLocator.Instance.GameManager.SetState(State.Debug);
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
                UpdateLoc();
            };
            ServiceLocator.Instance.MenuManager.Show(MenuType.PopupTwoButtons, context);
        }
    }
}