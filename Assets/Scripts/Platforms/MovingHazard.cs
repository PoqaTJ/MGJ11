using System.Collections.Generic;
using UnityEngine;

namespace Platforms
{
    public class MovingHazard: MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private bool _reverseMode;
        
        private bool _reversing = false;

        private int _index = 0;
        private Transform _currentWaypoint => _waypoints[_index];

        private void FixedUpdate()
        {
            Vector2 direction = _currentWaypoint.position - transform.position;
            direction.Normalize();
            
            // if distance is less than speed
            if (Vector2.Distance(transform.position, _currentWaypoint.position) <= _speed * Time.fixedDeltaTime)
            {
                OnReachWaypoint();
                return;
            }

            // move toward waypoint
            transform.position += (Vector3)direction * (_speed * Time.fixedDeltaTime);
        }

        private void OnReachWaypoint()
        {
            transform.position = new Vector3(_currentWaypoint.position.x, _currentWaypoint.position.y,
                transform.position.z);
            
            if (HasReachedLastWaypoint())
            {
                if (_reverseMode)
                {
                    _reversing = !_reversing;
                    _index = _reversing ? _waypoints.Count - 2 : 1;
                }
                else
                {
                    _index = 0;
                }
            }
            else
            {
                _index += _reversing ? -1 : 1;
            }
        }

        private bool HasReachedLastWaypoint()
        {
            if (_reverseMode && _reversing)
            {
                return _index == 0;
            }
            return _index == _waypoints.Count - 1;            
        }
    }
}