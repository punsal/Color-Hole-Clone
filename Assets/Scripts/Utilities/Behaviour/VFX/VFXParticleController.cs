using EventArguments;
using UnityEngine;
using Utilities.Publisher_Subscriber_System;

namespace Utilities.Behaviour.VFX
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFXParticleController : MonoBehaviour
    {
        [SerializeField] private GameEventType eventType = GameEventType.LevelComplete;

        private ParticleSystem system;

        private Subscription<GameEventType> genericEventTypeSubscription;
        
        private void OnEnable()
        {
            system = GetComponent<ParticleSystem>();

            genericEventTypeSubscription = PublisherSubscriber.Subscribe<GameEventType>(LevelCompleteEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(genericEventTypeSubscription);
        }

        private void LevelCompleteEventHandler(GameEventType gameEventType)
        {
            if (gameEventType == eventType)
            {
                system.Play();
            }
        }
    }
}
