using System.Collections;
using Dialogs;
using Player;
using Services;
using UnityEngine;

namespace Cutscenes
{
    public class LevelOneController: Director
    {
        [SerializeField] private PlayerInputController _tomoyaNormalInput;
        [SerializeField] private PlayerInputController _tomoyaMagicalInput;
        [SerializeField] private Animator _tomoyaNormalAnimator;
        [SerializeField] private Animator _tomoyaMagicalAnimator;

        [SerializeField] private ConversationDefinition _convinceConversation;
        [SerializeField] private Transform _transformationLocation;
        
        [SerializeField] private ButterflyController _butterflyController;
        [SerializeField] private Transform _butterflyLocation1;
        [SerializeField] private Transform _butterflyLocation2;

        private static readonly int StartTransform = Animator.StringToHash("TransformStart");
        private static readonly int StopTransform = Animator.StringToHash("TransformEnd");

        private void Start()
        {
            ServiceLocator.Instance.GameManager.OnPlayerSpawn += (controller) =>
            {
                if (ServiceLocator.Instance.SaveManager.Transformed)
                {
                    _tomoyaMagicalAnimator = controller.GetComponent<Animator>();                    
                }
                else
                {
                    _tomoyaNormalAnimator = controller.GetComponent<Animator>();
                }
            };
        }

        public void StartTransformationSequence()
        {
            StartCoroutine(TransformationRoutine());
        }
        
        private IEnumerator TransformationRoutine()
        {
            // stop tomoya input
            _tomoyaNormalInput.enabled = false;
            PlayerMover tomoyaMover = _tomoyaNormalInput.gameObject.GetComponent<PlayerMover>();
            tomoyaMover.enabled = true;

            yield return MoveCharacterTo(tomoyaMover, _transformationLocation);
            
            // fly to Tomoya
            bool reached = false;
            _butterflyController.MoveTo(_butterflyLocation1, () => reached = true);

            yield return new WaitUntil(() => reached);
            
            ServiceLocator.Instance.DialogManager.StartConversation(_convinceConversation, null);
            
            // fly above her
            reached = false;
            _butterflyController.MoveTo(_butterflyLocation2, () => reached = true);
            yield return new WaitUntil(() => reached);
            
            // transformation sequence
            _butterflyController.Disappear();

            yield return new WaitForSeconds(0.2f);
            
            _tomoyaNormalAnimator.SetTrigger(StartTransform);
            _tomoyaMagicalAnimator.SetTrigger(StartTransform);

            yield return new WaitForSeconds(1.5f);
            //_akariParticleEmitter.transform.SetParent(null);
            //_akariParticleEmitter.Play();
            _tomoyaMagicalInput.transform.position = _tomoyaNormalInput.transform.position;
            _tomoyaNormalInput.gameObject.SetActive(false);

            //yield return FlashBlue();
            yield return new WaitForSeconds(1.5f);
            
            _tomoyaMagicalAnimator.SetTrigger(StopTransform);

            ServiceLocator.Instance.SaveManager.Transformed = true;
            
            yield return new WaitForSeconds(1f);
            _tomoyaMagicalInput.enabled = true;
        }
    }
}