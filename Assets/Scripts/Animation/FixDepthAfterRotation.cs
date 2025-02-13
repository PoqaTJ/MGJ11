using System;
using UnityEngine;

namespace Animation
{
    public class FixDepthAfterRotation: MonoBehaviour
    {
        private float posZ;

        void Start()
        {
            posZ = transform.position.z;
        }

        private void LateUpdate()
        {
            if (transform.position.z != posZ)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, posZ);
            }
        }
    }
}