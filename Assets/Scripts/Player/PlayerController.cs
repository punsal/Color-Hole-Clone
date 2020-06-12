using Collectible.Linker;
using DG.Tweening;
using EventArguments;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Utilities.Behaviour.Player.Movement;
using Utilities.Extensions;
using Utilities.Publisher_Subscriber_System;

namespace Player
{
    [RequireComponent(typeof(PlayerNavMeshMovementController))]
    public class PlayerController : MonoBehaviour
    {
        private Transform playerTransform;
        private PlayerNavMeshMovementController playerNavMeshMovementController;
        private CustomTransform initialTransform;

        [Header("Reset Properties")]
        [SerializeField] private float resetXPosition = 0f;
        [SerializeField] private float resetDuration = 2f;

        [Header("Destination")] 
        #pragma warning disable 649
        [SerializeField] private Transform destinationTransform;
        #pragma warning restore 649
        [SerializeField] private float destinationDuration = 2f;
        
        [Header("Trigger Game Event")]
        [SerializeField] private GameEventType gameEventType = GameEventType.GroundCompleted;

        [Header("Camera")] 
        [SerializeField] private UnityEvent cameraAction = new UnityEvent();
        
        private Subscription<GameEventType> gameEventSubscription;
        private Subscription<GameEventType> resetEventSubscription;

        private void Awake()
        {
            if (playerTransform == null)
            {
                playerTransform = GetComponent<Transform>();
            }

            if (playerNavMeshMovementController == null)
            {
                playerNavMeshMovementController = GetComponent<PlayerNavMeshMovementController>();
            }
            
            initialTransform = new CustomTransform()
            {
                position = playerTransform.position,
                rotation = playerTransform.eulerAngles,
                scale = playerTransform.localScale
            };
        }

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(Move);
            resetEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(ResetTransform);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
            PublisherSubscriber.Unsubscribe(resetEventSubscription);
        }

        // ReSharper disable once ParameterHidesMember
        private void Move(GameEventType gameEventType)
        {
            if (this.gameEventType != gameEventType) return;
            var navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            playerNavMeshMovementController.enabled = false;
            playerTransform.DOMove(playerTransform.position.SetX(resetXPosition), resetDuration).OnComplete(() =>
            {
                if (destinationTransform == null) return;
                cameraAction.Invoke();
                playerTransform.DOMove(destinationTransform.position, destinationDuration).OnComplete(() =>
                {
                    navMeshAgent.enabled = true;
                    playerNavMeshMovementController.enabled = true;
                });
            });
        }

        // ReSharper disable once ParameterHidesMember
        private void ResetTransform(GameEventType gameEventType)
        {
            if (gameEventType != GameEventType.GameStart) return;
            var navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            playerNavMeshMovementController.enabled = false;
                
            playerTransform.position = initialTransform.position;
            playerTransform.eulerAngles = initialTransform.rotation;
            playerTransform.localScale = initialTransform.scale;

            navMeshAgent.enabled = true;
            playerNavMeshMovementController.enabled = true;
        }
    }
}