using System;
using EventArguments;
using Player.Data;
using TMPro;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;
using Zenject;

namespace Manager {
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        #pragma warning disable 649
        [SerializeField] private GameObject panelLevelFailed;
        #pragma warning restore 649

        [Header("Level Information")]
        #pragma warning disable 649
        [SerializeField] private TextMeshProUGUI textCurrentLevel;
        [SerializeField] private TextMeshProUGUI textNextLevel;
        #pragma warning restore 649

        [Header("Gold Information")] 
        #pragma warning disable 649
        [SerializeField] private TextMeshProUGUI textGold;
        #pragma warning restore 649

        private PlayerData playerData;
        
        private Subscription<GameEventType> gameEventSubscription;

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        private void Update()
        {
            PlayerDataHandler();
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            this.playerData = playerData;
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

        private void PlayerDataHandler()
        {
            textCurrentLevel.text = playerData.GetLevel().ToString();
            textNextLevel.text = (playerData.GetLevel() + 1).ToString();
            textGold.text = playerData.GetGold().ToString();
        }
    }
}
