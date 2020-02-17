namespace Player.Data.Persistent {
    [System.Serializable]
    public class PersistentPlayerData
    {
        public int level;
        public int gold;

        public PersistentPlayerData(PlayerData playerData)
        {
            level = playerData.GetLevel();
            gold = playerData.GetGold();
        }
    }
}