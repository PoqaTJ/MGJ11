using System;
using UnityEngine;

namespace Game
{
    public class Collectable: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            OnContact();
            Destroy(gameObject);
        }

        protected virtual void OnContact()
        {
            
        }
    }
}