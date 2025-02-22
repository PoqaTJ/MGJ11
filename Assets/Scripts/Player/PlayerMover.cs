using System;
using UnityEngine;

namespace Player
{
    public class PlayerMover: MonoBehaviour
    {
        [SerializeField] private Transform _hTarget = null;
        [SerializeField] private PlayerController _playerController;

        private Action _onArrive;
        private float _lastFrameDiff;

        private void Start()
        {
            if (_hTarget != null)
            {
                MoveTo(_hTarget, null);
            }
        }

        public void MoveTo(Transform hLocation, Action onArrive)
        {
            _lastFrameDiff = hLocation.position.x - transform.position.x;
            _hTarget = hLocation;
            _onArrive = onArrive;
        }

        public void Face(Direction direction)
        {
            _playerController.Face(direction == Direction.RIGHT);
        }
        
        private void FixedUpdate()
        {
            if (_hTarget != null)
            {
                float diff = _hTarget.position.x - transform.position.x;
                if (diff == 0 || Mathf.Abs(diff) > Mathf.Abs(_lastFrameDiff))
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

                _playerController.OnFixedUpdate(hMove);
                _lastFrameDiff = diff;
            }
            else
            {
                _playerController.OnFixedUpdate(0);
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