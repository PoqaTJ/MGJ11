using System;
using Services;
using UnityEngine;

namespace Player
{
    public class PlayerSpawner: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                ServiceLocator.Instance.GameManager.ActivateSpawner(this);
            }
        }
    }
}