using System;
using UnityEngine;

namespace Platforms
{
    public class RotatingHazard: MonoBehaviour
    {
        [SerializeField] private float _speed1 = 10f;

        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, _speed1 * Time.deltaTime));
        }
    }
}