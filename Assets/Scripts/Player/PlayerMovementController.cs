using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        
        private void Update()
        {
            transform.position += Vector3.forward * (Time.deltaTime * 10f);
        }
    }
}
