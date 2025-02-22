using System;
using System.Collections;
using Dialogs;
using Player;
using Services;
using UnityEngine;

namespace Cutscenes
{
    public class IntroController: MonoBehaviour
    {
        [SerializeField] private PlayerMover _akariNormal;
        [SerializeField] private PlayerMover _akariTransformed;
        [SerializeField] private PlayerMover _tomoyaNormal;

        [SerializeField] private Transform _akariPosition1;
        [SerializeField] private Transform _tomoyaPosition1;

        [SerializeField] private ConversationDefinition _conversation1;
        private void Start()
        {
            StartCoroutine(PlayIntro());
        }

        private IEnumerator PlayIntro()
        {
            yield return new WaitForSeconds(0.5f);
            
            _akariNormal.MoveTo(_akariPosition1, () => _akariNormal.Face(PlayerMover.Direction.LEFT));
            _tomoyaNormal.MoveTo(_tomoyaPosition1, () => _tomoyaNormal.Face(PlayerMover.Direction.RIGHT));

            yield return new WaitForSeconds(4f);

            bool convOver = false;
            ServiceLocator.Instance.DialogManager.StartConversation(_conversation1, () => convOver = true);
            
            yield return new WaitUntil(()=> convOver == true);
        }
    }
}