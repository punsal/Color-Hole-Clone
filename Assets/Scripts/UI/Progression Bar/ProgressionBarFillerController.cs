using EventArguments;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Publisher_Subscriber_System;

namespace UI.Progression_Bar {
    [RequireComponent(typeof(Image))]
    public class ProgressionBarFillerController : MonoBehaviour
    {
        private Image imageFiller;

        [SerializeField] private int groundIndex = 1;

        private Subscription<GameEventType> levelStartEventSubscription;
        private Subscription<CollectedItemArgs> collectedItemEventSubscription;
        
        private void Awake()
        {
            if (imageFiller == null)
            {
                imageFiller = GetComponent<Image>();
            }
        }

        private void OnEnable()
        {
            levelStartEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(LevelStartEventHandler);
            collectedItemEventSubscription =
                PublisherSubscriber.Subscribe<CollectedItemArgs>(CollectedItemEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(levelStartEventSubscription);
            PublisherSubscriber.Unsubscribe(collectedItemEventSubscription);
        }

        private void LevelStartEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == GameEventType.LevelStart)
            {
                imageFiller.fillAmount = 0f;
            }
        }

        private void CollectedItemEventHandler(CollectedItemArgs args)
        {
            if (args.groundIndex != groundIndex) return;
            var fillAmount = args.currentItemCount * 100f / args.totalItemCount / 100f;
            imageFiller.fillAmount = fillAmount;
        }
    }
}
