using System;
using UnityEngine;
using Interfaces;
using Objects;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out IInteract interact))
            {
                interact.InteractNotTrigger();
            }
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteract interactable))
            {
                interactable.Interact();
            }
        }
    }
}
