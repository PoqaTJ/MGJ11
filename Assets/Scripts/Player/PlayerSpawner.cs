using System;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerSpawner: MonoBehaviour
    {
        public string ID { get; private set; }
        public bool Default = false;
        
        private void Awake()
        {
            ID = $"{SceneManager.GetActiveScene().name}{(int)transform.position.x},{(int)transform.position.y}";
            gameObject.name = ID;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                ServiceLocator.Instance.GameManager.ActivateSpawner(this);
            }
        }
    }
}