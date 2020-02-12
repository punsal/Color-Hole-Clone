using UnityEngine;

namespace Utilities.Behaviour.Camera
{
    public class CameraSmoothFollow : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private Transform target;
        #pragma warning restore 649
        
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset = Vector3.zero;

        [SerializeField] private bool isLookingAt = false;
        private void FixedUpdate ()
        {
            var desiredPosition = target.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            if (isLookingAt)
            {
                transform.LookAt(target);
            }
        }
    }
}
