using DG.Tweening;
using EventArguments;
using UnityEngine;
using Utilities.Extensions;
using Utilities.Publisher_Subscriber_System;

namespace Platform.Playground {
    public class GateController : MonoBehaviour
    {
        private Transform gateTransform;
        
        [SerializeField] private float yPosition;
        [SerializeField] private float duration;
        [SerializeField] private GameEventType gameEventType;

        private float initialYPosition;
        
        private Subscription<GameEventType> gameEventSubscription;

        private void Awake()
        {
            if (gateTransform == null)
            {
                gateTransform = GetComponent<Transform>();
            }

            initialYPosition = gateTransform.position.y;
        }

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(Open);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        // ReSharper disable once ParameterHidesMember
        private void Open(GameEventType gameEventType)
        {
            if (this.gameEventType == gameEventType)
            {
                gateTransform.DOMove(gateTransform.position.SetY(yPosition), duration).OnComplete(() =>
                    {
                        gateTransform.DOMove(gateTransform.position.SetY(initialYPosition), duration).SetDelay(2f);
                    });
            }
        }
    }
}
