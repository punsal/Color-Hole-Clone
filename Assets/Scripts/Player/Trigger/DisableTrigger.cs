using Collectible;
using UnityEngine;
using Utilities.Behaviour.Trigger;

namespace Player.Trigger
{
    public class DisableTrigger : TriggerController
    {
        protected override void OnTriggerEnterAction(Collider other)
        {
            var collectibleItem = other.GetComponent<CollectibleItem>();
            if (collectibleItem == null) return;
            collectibleItem.Fall();
        }
    }
}