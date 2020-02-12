using UnityEngine;
using Utilities.Manager;
using Utilities.Manager.EventArgs;
using Utilities.Publisher_Subscriber_System;

namespace Utilities.Behaviour.Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        private Transform playerTransform;
        private Vector3 playerPosition;

        private Subscription<InputEventArgs> inputEventSubscription;

        private void Awake()
        {
            playerTransform = transform;
            playerPosition = playerTransform.position;
        }

        private void OnEnable()
        {
            inputEventSubscription = PublisherSubscriber.Subscribe<InputEventArgs>(Move);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(inputEventSubscription);
        }

        private void Move(InputEventArgs inputEventArgs)
        {
            playerPosition += inputEventArgs.delta;
            playerTransform.position = playerPosition;
        }
    }
}
