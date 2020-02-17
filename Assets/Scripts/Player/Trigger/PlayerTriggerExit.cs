using System.Collections.Generic;
using Collectible;
using UnityEngine;

namespace Player.Trigger
{
    public class PlayerTriggerExit : MonoBehaviour
    {
        [SerializeField] protected List<string> tags;
        
        private void OnTriggerExit(Collider other)
        {
            if (!tags.Contains(other.tag)) return;
            var collectibleItem = other.GetComponent<CollectibleItem>();
            if (collectibleItem == null) return;
            collectibleItem.Deactivate();
        }
    }
}