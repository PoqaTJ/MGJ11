﻿using UnityEngine;

namespace Game.Services
{
    public class ServiceLocator : MonoBehaviour
    {
#region singleton
        private static readonly string _serviceLocatorName = "ServiceLocator";
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.Log("Looking for ServiceLocator object in current scenes.");
                    _instance = GameObject.Find(_serviceLocatorName).GetComponent<ServiceLocator>();

                    if (_instance == null)
                    {
                        Debug.Log("Loading ServiceLocator from Resources.");
                        var go = Resources.Load(_serviceLocatorName) as GameObject;
                        _instance = go.GetComponent<ServiceLocator>();
                    }
                }
                return _instance;
            }
        }

        private static ServiceLocator _instance;
#endregion

        // List services here
        //public MenuManager MenuManager;
    }
}