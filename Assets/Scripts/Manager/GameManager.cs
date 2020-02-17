using EventArguments;
using Player.Data;
using UnityEngine;
using UnityEngine.Events;
using Utilities.Publisher_Subscriber_System;

namespace Manager {
    public class GameManager : MonoBehaviour
    {
        [Header("Platform Properties")]
        [SerializeField] private Transform[] ground1Transforms;
        [SerializeField] private Transform[] ground2Transforms;
        
        [Header("Platform Information")]
        [SerializeField] private Transform currentGround1Transform;
        [SerializeField] private int currentGround1TransformIndex;
        [SerializeField] private Transform currentGround2Transform;
        [SerializeField] private int currentGround2TransformIndex;
        
        [Header("Ground Properties")]
        [SerializeField] private int itemCount;
        [SerializeField] private int currentItemCount;
        [SerializeField] private int totalItemCount;
        [SerializeField] private int currentGroundIndex;

        [SerializeField] private UnityEvent onGameStartEvent;
        
        private Subscription<GameEventType> gameEventSubscription;

        private void Awake()
        {
            //load playerdata
        }

        private void OnEnable()
        {
            gameEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(GameEventHandler);
        }

        private void Start()
        {
            PublisherSubscriber.Publish(GameEventType.GameStart);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(gameEventSubscription);
        }

        public void Continue()
        {
            //Do some rewarded ad action;
            PublisherSubscriber.Publish(GameEventType.LevelContinue);
        }

        public void NoThanks()
        {
            //Do some Inter ad action;
            PublisherSubscriber.Publish(GameEventType.LevelReload);
        }

        private void GameEventHandler(GameEventType gameEventType)
        {
            switch (gameEventType)
            {
                case GameEventType.GameStart:
                    var level = PlayerData.instance.GetLevel();
                    currentGround1TransformIndex = (level - 1) % ground1Transforms.Length;
                    currentGround2TransformIndex = (level - 1) % ground2Transforms.Length;

                    LoadLevel();

                    onGameStartEvent?.Invoke();
                    PublisherSubscriber.Publish(GameEventType.LevelStart);
                    PublisherSubscriber.Publish(PlayerData.instance);
                    break;
                case GameEventType.LevelStart:
                    currentGroundIndex = 1;
                    FindCollectibleItemCount();
                    break;
                case GameEventType.ItemCollected:
                    ItemCollected();
                    break;
                case GameEventType.LevelComplete:
                    PlayerData.instance.SetLevel(PlayerData.instance.GetLevel() + 1);
                    PlayerData.instance.SetGold(PlayerData.instance.GetGold() + 50);
                    PublisherSubscriber.Publish(GameEventType.GameStart);
                    Debug.Log("Level Completed");
                    break;
                case GameEventType.GroundCompleted:
                    itemCount = 0;
                    PublisherSubscriber.Publish(new GroundCompletedEventArgs()
                    {
                        currentGroundIndex = currentGroundIndex
                    });
                    currentGroundIndex++;
                    FindCollectibleItemCount();
                    break;
                case GameEventType.IncrementCollectibleItemCount:
                    currentItemCount = 0;
                    itemCount++;
                    totalItemCount = itemCount;
                    break;
                case GameEventType.LevelReload:
                    itemCount = 0;
                    PublisherSubscriber.Publish(GameEventType.GameStart);
                    break;
                default:
                    //do nothing;
                break;
            }   
        }

        private void LoadLevel()
        {
            if (currentGround1Transform != null)
            {
                Destroy(currentGround1Transform.gameObject);
            }

            if (currentGround2Transform != null)
            {
                Destroy(currentGround2Transform.gameObject);
            }

            currentGround1Transform = Instantiate(ground1Transforms[currentGround1TransformIndex]);
            currentGround2Transform = Instantiate(ground2Transforms[currentGround2TransformIndex]);
        }

        private void ItemCollected()
        {
            if (--itemCount != 0)
            {
                PublisherSubscriber.Publish(new CollectedItemArgs()
                {
                    groundIndex = currentGroundIndex,
                    currentItemCount = ++currentItemCount,
                    totalItemCount = totalItemCount
                });
            }
            else
            {
                PublisherSubscriber.Publish(new CollectedItemArgs()
                {
                    groundIndex = currentGroundIndex,
                    currentItemCount = 1,
                    totalItemCount = 1
                });
                
                PublisherSubscriber.Publish(currentGroundIndex == 1
                    ? GameEventType.GroundCompleted
                    : GameEventType.LevelComplete);
            }
        }

        private void FindCollectibleItemCount()
        {
            PublisherSubscriber.Publish(new CollectibleItemArgs()
            {
                index = currentGroundIndex
            });
        }
    }
}
