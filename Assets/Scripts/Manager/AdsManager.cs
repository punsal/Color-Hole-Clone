using System;
using EventArguments;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Utilities.Publisher_Subscriber_System;

namespace Manager {
    public class AdsManager : MonoBehaviour, IUnityAdsListener
    {
        [SerializeField] private string gameId = "3472671";
        [SerializeField] private string interstitialAdPlacementId = "video";
        [SerializeField] private string rewardedAdPlacementId = "rewardedVideo";
        [SerializeField] private bool testMode = true;

        [SerializeField] private float wait = 2f;

        [SerializeField] private Button buttonContinue;
        
        private Subscription<GameEventType> gameEventSubscription;

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        private void Start () 
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize (gameId, testMode);
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            switch (gameEventType)
            {
                case GameEventType.LevelComplete:
                    Invoke(nameof(ShowInterstitial), wait);
                    break;
                case GameEventType.LevelContinue:
                    ShowRewardedAd();
                    break;
                case GameEventType.LevelReload:
                    ShowInterstitial();
                    break;
            }
        }
        
        private void ShowInterstitial()
        {
            Advertisement.Show(interstitialAdPlacementId);
        }

        private void ShowRewardedAd()
        {
            buttonContinue.interactable = false;
            Advertisement.Show(rewardedAdPlacementId);
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == rewardedAdPlacementId)
            {
                buttonContinue.interactable = true;
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            PublisherSubscriber.Publish(GameEventType.LevelReload);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            //do nothing.
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId != rewardedAdPlacementId)
            {
                return;
            }
            switch (showResult)
            {
                case ShowResult.Failed:
                    //do nothing.
                    break;
                case ShowResult.Skipped:
                    PublisherSubscriber.Publish(GameEventType.LevelReload);
                    break;
                case ShowResult.Finished:
                    //do nothing.
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(showResult), showResult, null);
            }
        }
    }
}
