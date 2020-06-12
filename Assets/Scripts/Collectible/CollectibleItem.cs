using System;
using EventArguments;
using Manager;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;

namespace Collectible {

    public enum CollectibleType
    {
        Default,
        Enemy,
        Linker
    }
    
    [RequireComponent(typeof(Rigidbody))]
    public class CollectibleItem : MonoBehaviour
    {
        #region Components
        
        private Rigidbody collectibleItemRigidbody;
        
        #endregion

        #region LayerMask Properties
        
        private int inHoleLayer;
        private int onGroundLayer;
        
        #endregion

        private bool isActive;
        private bool isFeltIn;
        private bool isCollected;

        [SerializeField] private int groundIndex = 1;
        [SerializeField] private CollectibleType collectibleType = CollectibleType.Default;
        
        private Subscription<CollectibleItemArgs> collectibleItemEventSubscription;
        private Subscription<GroundCompletedEventArgs> groundCompletedEventSubscription;
        
        private void Awake()
        {
            if (collectibleItemRigidbody == null)
            {
                collectibleItemRigidbody = GetComponent<Rigidbody>();
            }
            collectibleItemRigidbody.isKinematic = true;

            inHoleLayer = LayerMask.NameToLayer("InHole");
            onGroundLayer = LayerMask.NameToLayer("OnGround");
        }

        private void OnEnable()
        {
            if (collectibleType == CollectibleType.Default)
            {
                collectibleItemEventSubscription =
                    PublisherSubscriber.Subscribe<CollectibleItemArgs>(CollectibleItemEventHandler);
            }

            if (collectibleType == CollectibleType.Enemy)
            {
                groundCompletedEventSubscription =
                    PublisherSubscriber.Subscribe<GroundCompletedEventArgs>(GroundCompleteEventHandler);
            }

            if (collectibleType == CollectibleType.Linker)
            {
                collectibleItemRigidbody.isKinematic = true;
            }
        }

        private void OnDisable()
        {
            if (collectibleType == CollectibleType.Default)
            {
                PublisherSubscriber.Unsubscribe(collectibleItemEventSubscription);
            }

            if (collectibleType == CollectibleType.Enemy)
            {
                PublisherSubscriber.Unsubscribe(groundCompletedEventSubscription);
            }
        }

        private void Update()
        {
            if (isActive)
            {
                collectibleItemRigidbody.AddForce(collectibleType == CollectibleType.Linker ?  Physics.gravity * 10f : Physics.gravity);
            }
        }

        public void Activate()
        {
            isActive = true;
            collectibleItemRigidbody.isKinematic = false;
            ChangeLayer(inHoleLayer);
        }

        public void Deactivate()
        {
            if (isFeltIn) return;
            isActive = false;
            ChangeLayer(onGroundLayer);
        }

        public void Fall()
        {
            isFeltIn = true;
        }

        public void Collect()
        {
            if (collectibleType == CollectibleType.Linker)
            {
                gameObject.SetActive(false);
                return;
            }
            if (isCollected) return;
            isCollected = true;
            PublisherSubscriber.Publish(collectibleType == CollectibleType.Enemy 
                ? GameEventType.LevelFailed 
                : GameEventType.ItemCollected);
        }

        private void ChangeLayer(int layer)
        {
            gameObject.layer = layer;
        }

        private void CollectibleItemEventHandler(CollectibleItemArgs collectibleItemArgs)
        {
            if (collectibleItemArgs.index == groundIndex)
            {
                PublisherSubscriber.Publish(GameEventType.IncrementCollectibleItemCount);
            }
        }

        private void GroundCompleteEventHandler(GroundCompletedEventArgs args)
        {
            if (groundIndex == args.currentGroundIndex)
            {
                isCollected = true;
            }
        }
    }
}
