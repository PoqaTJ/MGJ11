using System;
using UnityEngine;

namespace Platforms
{
    public class RotatingHazard: MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 1f;

        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, _rotationSpeed));
        }
    }
}