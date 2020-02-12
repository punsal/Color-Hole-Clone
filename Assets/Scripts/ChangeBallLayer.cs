using UnityEngine;
using System.Collections;

public class ChangeBallLayer : MonoBehaviour
{
    [SerializeField] private LayerMask onEnterLayer;
    [SerializeField] private LayerMask onExitLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"Collectible"))
        {
            other.gameObject.layer = 11;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*if(other.gameObject.CompareTag($"Collectible"))
        {
            other.gameObject.layer = 10;
        }*/
    }
}