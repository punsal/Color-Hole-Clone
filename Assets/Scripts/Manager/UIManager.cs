using EventArguments;
using Player.Data;
using TMPro;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;

namespace Manager {
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject panelLevelFailed;

        [Header("Level Information")]
        [SerializeField] private TextMeshProUGUI textCurrentLevel;
        [SerializeField] private TextMeshProUGUI textNextLevel;

        [Header("Gold Information")] 
        [SerializeField] private TextMeshProUGUI textGold;
        
        private Subscription<GameEventType> gameEventSubscription;
        private Subscription<PlayerData> playerDataSubscription;

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
            playerDataSubscription = PublisherSubscriber.Subscribe<PlayerData>(PlayerDataHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
            PublisherSubscriber.Unsubscribe(playerDataSubscription);
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            switch (gameEventType)
            {
                case GameEventType.LevelStart:
                    panelLevelFailed.SetActive(false);
                    break;
                case GameEventType.LevelFailed:
                    panelLevelFailed.SetActive(true);
                    break;
                case GameEventType.LevelContinue:
                    panelLevelFailed.SetActive(false);
                    break;
                default: 
                    //do nothing
                break;
            }
        }

        private void PlayerDataHandler(PlayerData playerData)
        {
            textCurrentLevel.text = playerData.GetLevel().ToString();
            textNextLevel.text = (playerData.GetLevel() + 1).ToString();
            textGold.text = playerData.GetGold().ToString();
        }
    }
}
