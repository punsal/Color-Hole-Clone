using UnityEngine;

namespace Utilities.Behaviour.Follow
{
    public class SimpleFollow : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private Transform targetTransform;
        #pragma warning restore 649
        
        private Transform followerTransform;
        private Vector3 offset;
        
        private void Awake()
        {
            followerTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            offset = followerTransform.position - targetTransform.position;
        }

        private void Update()
        {
            followerTransform.position = targetTransform.position + offset;
        }
    }
}
