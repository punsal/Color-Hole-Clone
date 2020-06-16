using System;
using Collectible;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Utilities.Behaviour.Trigger;

namespace Player.Trigger
{
    public class DisableTrigger : TriggerController
    {
        private Collider playerCollider;

        private void OnEnable()
        {
            if (playerCollider == null)
            {
                playerCollider = GetComponent<Collider>();
            }

            playerCollider.OnTriggerStayAsObservable()
                .Select(other => tags.Contains(other.tag) ? other.gameObject : null)
                .Subscribe(o =>
                {
                    if (o != null)
                    {
                        o.SetActive(false);
                    }
                });
        }

        protected override void OnTriggerEnterAction(Collider other)
        {
            var collectibleItem = other.GetComponent<CollectibleItem>();
            if (collectibleItem == null) return;
            collectibleItem.Fall();
        }
    }
}