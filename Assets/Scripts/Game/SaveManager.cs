using System;
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
    }
}