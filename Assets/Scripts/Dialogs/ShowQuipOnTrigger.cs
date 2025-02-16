using System;
using Services;
using UnityEngine;

namespace Dialogs
{
    public class ShowQuipOnTrigger: MonoBehaviour
    {
        [SerializeField] private QuipDefinition _quip;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                ServiceLocator.Instance.DialogManager.ShowQuip(_quip);
                Destroy(gameObject);
            }
        }
    }
}