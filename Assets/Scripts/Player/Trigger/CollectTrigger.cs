using Collectible;
using UnityEngine;
using Utilities.Behaviour.Trigger;
using Utilities.Extensions;

namespace Player.Trigger
{
    public class CollectTrigger : TriggerController
    {
        protected override void OnTriggerEnterAction(Collider other)
        {
            if (other.GetComponent<CollectibleItem>(out var collectibleItem))
            {
                collectibleItem.Collect();
            }
        }
    }
}