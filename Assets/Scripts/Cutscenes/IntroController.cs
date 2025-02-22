using System;
using System.Collections;
using Dialogs;
using Menus;
using Menus.MenuTypes;
using Player;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cutscenes
{
    public class IntroController: MonoBehaviour
    {
        [SerializeField] private PlayerMover _akariNormal;
        [SerializeField] private PlayerMover _akariTransformed;
        [SerializeField] private PlayerMover _tomoyaNormal;

        [SerializeField] private Transform _akariPosition1;
        [SerializeField] private Transform _tomoyaPosition1;
        [SerializeField] private Transform _tomoyaPosition2;


        [SerializeField] private ConversationDefinition _conversation1;
        [SerializeField] private ConversationDefinition _boredConversation;
        [SerializeField] private ConversationDefinition _portalClosingConversation;

        [SerializeField] private Image _blackScreen;
        private void Start()
        {
            StartCoroutine(PlayIntro());
        }

        private IEnumerator PlayIntro()
        {
            yield return new WaitForSeconds(0.5f);

            bool akariReached1 = false;
            bool tomoyaReached1 = false;
            _akariNormal.MoveTo(_akariPosition1, () =>
            {
                _akariNormal.Face(PlayerMover.Direction.LEFT);
                akariReached1 = true;
            });
            _tomoyaNormal.MoveTo(_tomoyaPosition1, () =>
            {
                _tomoyaNormal.Face(PlayerMover.Direction.RIGHT);
                tomoyaReached1 = true;
            });

            yield return new WaitUntil(()=> akariReached1 && tomoyaReached1);
            yield return new WaitForSeconds(0.1f);

            bool convOver = false;
            ServiceLocator.Instance.DialogManager.StartConversation(_conversation1, () => convOver = true);
            
            yield return new WaitUntil(()=> convOver);

            yield return new WaitForSeconds(0.2f);
            
            _akariNormal.Face(PlayerMover.Direction.RIGHT);
            
            yield return new WaitForSeconds(0.2f);

            yield return ShowDebugDialog("Akari transformation");
            yield return new WaitForSeconds(0.2f);

            yield return ShowDebugDialog("Mascot opens portal");
            yield return new WaitForSeconds(0.2f);

            yield return ShowDebugDialog("Akari leaves");
            _akariNormal.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);

            yield return FadeToBlack();
            yield return new WaitForSeconds(2f);
            yield return FadeFromBlack();

            yield return MoveCharacterTo(_tomoyaNormal, _tomoyaPosition2);
            yield return new WaitForSeconds(0.1f);

            yield return MoveCharacterTo(_tomoyaNormal, _tomoyaPosition1);
            yield return new WaitForSeconds(0.1f);
            
            yield return MoveCharacterTo(_tomoyaNormal, _tomoyaPosition2);
            yield return new WaitForSeconds(0.2f);
            
            _tomoyaNormal.Face(PlayerMover.Direction.RIGHT);
            yield return new WaitForSeconds(0.2f);
            
            ServiceLocator.Instance.DialogManager.StartConversation(_boredConversation, () => convOver = true);
            
            yield return new WaitForSeconds(0.2f);
            ShowDebugDialog("The portal begins to close");
            yield return new WaitForSeconds(0.2f);
            ShowDebugDialog("Tomoya runs in");
            SceneManager.LoadSceneAsync("Gameplay");
        }

        private IEnumerator MoveCharacterTo(PlayerMover mover, Transform loc)
        {
            bool reached = false;
            mover.MoveTo(loc, () =>
            {
                reached = true;
            });

            yield return new WaitUntil(() => reached);
        }
        
        private IEnumerator ShowDebugDialog(string description)
        {
            bool dismissed = false;
            PopupMenuOneButton.PopupMenuOneButtonContext context = new PopupMenuOneButton.PopupMenuOneButtonContext();
            context.titleLocString = "Placeholder";
            context.bodyLocString = description;
            context.buttonLocString = "Next";
            context.OnCloseAction = () => dismissed = true;
            ServiceLocator.Instance.MenuManager.Show(MenuType.PopupOneButton, context);
            
            yield return new WaitUntil(()=>dismissed);
        }

        private IEnumerator FadeToBlack()
        {
            while (_blackScreen.color.a < 1)
            {
                Color color = _blackScreen.color;
                color.a += .01f;
                _blackScreen.color = color;
                yield return null;
            }
        }

        private IEnumerator FadeFromBlack()
        {
            while (_blackScreen.color.a > 0)
            {
                Color color = _blackScreen.color;
                color.a -= .01f;
                _blackScreen.color = color;
                yield return null;
            }
        }
    }
}