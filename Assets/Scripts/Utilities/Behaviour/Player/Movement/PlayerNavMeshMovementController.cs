using UnityEngine;
using UnityEngine.AI;
using Utilities.Manager.EventArgs;
using Utilities.Publisher_Subscriber_System;

namespace Utilities.Behaviour.Player.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerNavMeshMovementController : MonoBehaviour
    {
        private NavMeshAgent agent;

        private Subscription<InputEventArgs> inputEventSubscription;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
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
            agent.nextPosition = transform.position + inputEventArgs.delta;
        }
    }
}