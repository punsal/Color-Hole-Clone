using UnityEngine;
using UnityEngine.EventSystems;
using Utilities.Manager.EventArgs;
using Utilities.Publisher_Subscriber_System;

namespace Utilities.Manager
{
    public class InputManager : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private LayerMask hitMask;
    
        private Vector3 initialPositionVector;
        private Vector3 currentPositionVector;
        private Vector3 positionDeltaVector;

        private Ray cameraRay;
        private RaycastHit raycastHit;

        private InputEventArgs inputEventArgs;
    
        private void OnValidate()
        {
            if (mainCamera == null)
            {
                mainCamera = UnityEngine.Camera.main;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            cameraRay = mainCamera.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(cameraRay, out raycastHit, Mathf.Infinity, hitMask))
            {
                initialPositionVector = raycastHit.point;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            cameraRay = mainCamera.ScreenPointToRay(eventData.position);
            if (!Physics.Raycast(cameraRay, out raycastHit, Mathf.Infinity, hitMask)) return;
            currentPositionVector = raycastHit.point;
            positionDeltaVector = currentPositionVector - initialPositionVector;
            initialPositionVector = currentPositionVector;
        
            inputEventArgs = new InputEventArgs()
            {
                delta = positionDeltaVector
            };
            PublisherSubscriber.Publish(inputEventArgs);
        }
    }
}