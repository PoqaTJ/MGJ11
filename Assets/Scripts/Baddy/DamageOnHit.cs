using System;
using Player;
using UnityEngine;

namespace Baddy
{
    public class DamageOnHit: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                PlayerController pc = col.GetComponent<PlayerController>();
                pc.TakeDamage(1, transform.position);                
            }
        }
    }
}