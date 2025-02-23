using System;
using UnityEngine;

namespace Cutscenes
{
    public class ButterflyController: MonoBehaviour
    {
        private Transform _target = null;

        private Action _onArrive;
        private float _minDistance = 0.05f;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private Animator _animator;
        private static readonly int AppearTrigger = Animator.StringToHash("Appear");
        private static readonly int DisappearTrigge = Animator.StringToHash("Disappear");

        public void MoveTo(Transform hLocation, Action onArrive)
        {
            _target = hLocation;
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
            if (_target != null)
            {
                float diff = Vector2.Distance(transform.position, _target.position);
                if (diff == 0 || diff <= _minDistance)
                {
                    OnReachTarget();
                    return;
                }
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnReachTarget()
        {
            transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            _onArrive?.Invoke();
            _target = null;
        }

        public enum Direction
        {
            RIGHT,
            LEFT
        }
    }
}