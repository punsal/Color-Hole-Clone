using Manager;
using Player.Data.Persistent;
using UnityEngine;
using Utilities.File_System;

namespace Player.Data {
    [ExecuteAlways]
    public class PlayerData : MonoBehaviour
    {
        [Header("Binary File Properties")]
        [SerializeField] private string fileName = "player";
        [SerializeField] private string fileExtension = "data";
        
        [Header("Data Properties")]
        [SerializeField] private int level;
        [SerializeField] private int gold;

        public static PlayerData instance;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (instance != this)
            {
                Destroy(gameObject);
            }

            Load();
        }

        public void Load()
        {
            var data = SaveSystem.Load<PersistentPlayerData>(fileName, fileExtension);
            if (data == null)
            {
                Debug.Log($"File is created at : {Application.persistentDataPath}/{fileName}.{fileExtension}");
                SetDefault();
                Save();
                return;
            }

            level = data.level;
            gold = data.gold;
        }

        public void Save()
        {
            instance.Save<PlayerData, PersistentPlayerData>(fileName, fileExtension);
        }
        
        private void SetDefault()
        {
            level = 1;
            gold = 0;
        }

        public int GetLevel() => level;
        
        public int GetGold() => gold;

        // ReSharper disable once ParameterHidesMember
        public void SetLevel(int level)
        {
            this.level = level;
            Save();
        }

        // ReSharper disable once ParameterHidesMember
        public void SetGold(int gold)
        {
            this.gold = gold;
            Save();
        }
    }
}