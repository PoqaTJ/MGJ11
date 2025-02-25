using System;
using UnityEngine;

namespace Platforms
{
    public class RotatingHazard: MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;

        private void FixedUpdate()
        {
            transform.Rotate(new Vector3(0, 0, _speed));
        }
    }
}