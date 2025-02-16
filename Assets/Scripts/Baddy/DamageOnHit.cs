using System;
using Player;
using UnityEngine;

namespace Baddy
{
    public class DamageOnHit: MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
                PlayerController pc = col.collider.GetComponent<PlayerController>();
                pc.TakeDamage(1);
            }
        }
    }
}