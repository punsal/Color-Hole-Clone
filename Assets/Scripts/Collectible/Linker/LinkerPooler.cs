using System;
using System.Collections.Generic;
using EventArguments;
using UnityEngine;
using Utilities.Object_Pooler_System;
using Utilities.Publisher_Subscriber_System;

namespace Collectible.Linker {

    [Serializable]
    public struct CustomTransform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }
    
    [ExecuteAlways]
    public class LinkerPooler : MonoBehaviour
    {
        [SerializeField] private List<CustomTransform> linkerTransforms;

        [SerializeField] private string spawnTag;

        private Subscription<GameEventType> levelStartEventSubscription;

        private void OnEnable()
        {
            levelStartEventSubscription = PublisherSubscriber.Subscribe<GameEventType>(LevelStartEventHandler);
        }

        private void OnDisable()
        {
            PublisherSubscriber.Unsubscribe(levelStartEventSubscription);
        }

        public void SaveLinkerTransforms()
        {
            var childCount = transform.childCount;
            if (childCount == 0) return;
            if (linkerTransforms == null)
            {
                linkerTransforms = new List<CustomTransform>();
            }
            linkerTransforms.Clear();

            for (var i = 0; i < childCount; i++)
            {
                var childTransform = transform.GetChild(i).transform;
                linkerTransforms.Add(new CustomTransform()
                {
                    position = childTransform.position,
                    rotation = childTransform.eulerAngles,
                    scale = childTransform.localScale
                });
            }
            Debug.Log("Transforms are saved.");
        }

        private void LevelStartEventHandler(GameEventType gameEventType)
        {
            if (gameEventType != GameEventType.LevelStart) return;
            foreach (var linkerTransform in linkerTransforms)
            {
                var tempGameObject = ObjectPooler.SharedInstance.GetPooledObject(spawnTag);
                tempGameObject.SetActive(true);
                var tempGameObjectTransform = tempGameObject.transform;
                tempGameObjectTransform.position = linkerTransform.position;
                tempGameObjectTransform.eulerAngles = linkerTransform.rotation;
                tempGameObjectTransform.localScale = linkerTransform.scale;
                tempGameObjectTransform.parent = transform;
            }
        }
    }
}
