using System;
using System.Collections;
using Cinemachine;
using Player;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        #region events

        public Action OnPlayerDied;
        public Action OnPlayerTakeDamage;
        public Action<PlayerController> OnPlayerSpawn;

        #endregion
        
        public State CurrentState { get; private set; }

        private PlayerController _player;
        private PlayerSpawner _spawner;

        [SerializeField] GameObject _akariNormalPrefab;
        [SerializeField] GameObject _akariTFPrefab;
        [SerializeField] GameObject _tomoyaNormalPrefab;
        [SerializeField] GameObject _tomoyaTFPrefab;

        private void Start()
        {
            OnPlayerDied += OnPlayerDeath;
        }

        private void PlayerSpawned(PlayerController playerController)
        {
            _player = playerController;
        }

        private void OnPlayerDeath()
        {
            if (_spawner == null)
            {
                Debug.LogError("Player died but there is no active spawner so they will not respawn.");
                return;
            }

            StartCoroutine(SpawnPlayer());
        }

        private IEnumerator SpawnPlayer()
        {
            Destroy(_player.gameObject);
            GameObject prefab = _tomoyaTFPrefab;
            GameObject playerObject = Instantiate(prefab);
            playerObject.SetActive(false);
            _player = playerObject.GetComponent<PlayerController>();
            _player.transform.position = new Vector3(_spawner.transform.position.x, _spawner.transform.position.y,
                _player.transform.position.z);
            FocusCameraOn(_player.transform);
            yield return new WaitForSeconds(0.5f);
            _player.gameObject.SetActive(true);
            _player.Reset();
            OnPlayerSpawn?.Invoke(_player);
        }

        public void FocusCameraOn(Transform t)
        {
            GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>().Follow = t;
        }
        
        public void SetState(State state)
        {
            if (CurrentState == state)
            {
                Debug.LogError($"Tried to set state to {state}, but that was already the current state.");
                return;
            }

            Debug.Log($"Setting state to {state}.");
            switch (state)
            {
                case State.MainMenu:
                    SceneManager.LoadSceneAsync("Main");
                    break;
                case State.Intro:
                    SceneManager.LoadSceneAsync("Intro");
                    break;
                case State.Gameplay:
                    LoadGameplay();
                    break;
                case State.Debug:
                    SceneManager.LoadSceneAsync("Debug");
                    break;
            }
        }

        private void LoadGameplay()
        {
            int level = ServiceLocator.Instance.SaveManager.Level;
            if (level == 0)
            {
                SceneManager.LoadSceneAsync("LevelOne");
            }
            else
            {
                SceneManager.LoadSceneAsync("Gameplay");
            }
        }
        
        public void UnlockDoubleJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedDoubleJump = true;
        }
        
        public void UnlockTripleJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedTripleJump = true;
        }

        public void UnlockWallJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedWallJump = true;
        }
        
        public void ActivateSpawner(PlayerSpawner playerSpawner)
        {
            _spawner = playerSpawner;
        }

        public void RegisterPlayer(PlayerController playerController)
        {
            _player = playerController;
        }

        private IEnumerator PlayerDiesRoutine()
        {
            yield return null;
        }
    }

    public enum State
    {
        MainMenu,
        Gameplay,
        Debug,
        Intro
    }
}