using Collectible;
using UnityEngine;
using Utilities.Behaviour.Trigger;

namespace Player.Trigger
{
    public class PlayerTrigger : TriggerController
    {
        protected override void OnTriggerEnterAction(Collider other)
        {
            var collectibleItem = other.GetComponent<CollectibleItem>();
            if (collectibleItem == null) return;
            collectibleItem.Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!tags.Contains(other.tag)) return;
            var collectibleItem = GetComponent<CollectibleItem>();
            if (collectibleItem == null) return;
            collectibleItem.Deactivate();
        }
    }
}