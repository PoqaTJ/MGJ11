using System;
using Services;
using UnityEditor;
using UnityEngine;

namespace Game
{
    public class SaveManager: MonoBehaviour
    {
        private SaveGame _save;
        private readonly string _playerPrefsKey = "save_game";

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(_playerPrefsKey))
            {
                _save = new SaveGame();
            }
            else
            {
                try
                {
                    string stringSave = PlayerPrefs.GetString(_playerPrefsKey);
                    _save = JsonUtility.FromJson<SaveGame>(stringSave);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Unable to parse save. {e}");
                    _save = new SaveGame();
                }
            }
        }

        public bool WatchedIntro
        {
            get => _save.SeenIntro;
            set
            {
                _save.SeenIntro = value;
                Save();
            }
        }

        public int SpawnerIndex
        {
            get => _save.Spawner;
            set
            {
                _save.Spawner = value;
                Save();
            }
        }
        
        public bool Transformed
        {
            get => _save.Transformed;
            set
            {
                _save.Transformed = value;
                Save();
            }
        }
        
        public bool UnlockedWallJump
        {
            get => _save.UnlockedWallJump;
            set
            {
                _save.UnlockedWallJump = value;
                Save();
            }
        }
        
        public bool UnlockedDoubleJump
        {
            get => _save.UnlockedDoubleJump;
            set
            {
                _save.UnlockedDoubleJump = value;
                Save();
            }
        }
        
        public bool UnlockedTripleJump
        {
            get => _save.UnlockedTripleJump;
            set
            {
                _save.UnlockedTripleJump = value;
                Save();
            }
        }
        
        public int Level
        {
            get => _save.Level;
            set
            {
                _save.Level = value;
                Save();
            }
        }
        
        public void Save()
        {
            PlayerPrefs.SetString(_playerPrefsKey, JsonUtility.ToJson(_save));
        }

        public void Destroy()
        {
            _save = new SaveGame();
            Save();
        }
        
#if UNITY_EDITOR
        [MenuItem("Save/Delete Save (only when stopped)")]
        private static void ClearSave()
        {
            ServiceLocator.Instance.SaveManager.Destroy();
        }

        [MenuItem("Save/Delete Save (only when stopped)", true)]
        private static bool ClearSaveValidate()
        {
            return EditorApplication.isPlaying;
        }

        [MenuItem("Save/EnableWallJump")]
        private static void DebugEnableWallJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedWallJump = !ServiceLocator.Instance.SaveManager.UnlockedWallJump;
            return ServiceLocator.Instance.SaveManager.UnlockedWallJump;
        }
        
        [MenuItem("Save/EnableDoubleJump")]
        private static void DebugEnableDoubleJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedDoubleJump = !ServiceLocator.Instance.SaveManager.UnlockedDoubleJump;
            return ServiceLocator.Instance.SaveManager.UnlockedDoubleJump;
        }
        
        [MenuItem("Save/EnableTripleJump")]
        private static void DebugEnableTripleJump()
        {
            ServiceLocator.Instance.SaveManager.UnlockedTripleJump = !ServiceLocator.Instance.SaveManager.UnlockedTripleJump;
            return ServiceLocator.Instance.SaveManager.UnlockedTripleJump;
        }
#endif
    }
}