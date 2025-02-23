using System;
using UnityEngine;

namespace Cutscenes
{
    public class ButterflyController: MonoBehaviour
    {
        private Transform _hTarget = null;

        private Action _onArrive;
        private float _minDistance = 0.05f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private Animator _animator;
        private static readonly int AppearTrigger = Animator.StringToHash("Appear");
        private static readonly int DisappearTrigge = Animator.StringToHash("Disappear");

        public void MoveTo(Transform hLocation, Action onArrive)
        {
            _hTarget = hLocation;
            _onArrive = onArrive;
        }

        public void Face(Direction direction)
        {
            if (direction == Direction.RIGHT)
            {
                Vector3 t = transform.localScale;
                t.z = 1;
                transform.localScale = t;
            }
            else
            {
                Vector3 t = transform.localScale;
                t.z = -1;
                transform.localScale = t;
            }
        }

        public void Disappear()
        {
            _animator.SetTrigger(DisappearTrigge);
        }
        
        public void Appear()
        {
            _animator.SetTrigger(AppearTrigger);
        }
        
        private void FixedUpdate()
        {
            if (_hTarget != null)
            {
                float diff = _hTarget.position.x - transform.position.x;
                if (diff == 0 || Mathf.Abs(diff) <= _minDistance)
                {
                    OnReachTarget();
                    return;
                }

                float hMove;
                if (diff < 0)
                {
                    hMove = -1;
                }
                else
                {
                    hMove = 1;
                }

                Vector3 currentLocalPos = transform.position;
                currentLocalPos.x += _moveSpeed * Time.fixedDeltaTime * hMove;
                transform.position = currentLocalPos;
            }
        }

        private void OnReachTarget()
        {
            transform.position = new Vector3(_hTarget.position.x, transform.position.y, transform.position.z);
            _onArrive?.Invoke();
            _hTarget = null;
        }

        public enum Direction
        {
            RIGHT,
            LEFT
        }
    }
}