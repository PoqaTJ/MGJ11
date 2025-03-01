using System;
using Services;
using UnityEngine;

namespace Game
{
    public class Collectable: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                OnContact();
                Destroy(gameObject);                
            }
        }

        protected virtual void OnContact()
        {
            
        }
    }
}