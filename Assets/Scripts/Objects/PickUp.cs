using System;
using UnityEngine;
using Interfaces;
using DG.Tweening;

namespace Objects {
    public class PickUp : MonoBehaviour, IInteract {
        public static Action<bool, Transform> OnInteractNotTrigger;

        private bool _isPickedUp;
        private Collider _collider;

        private void Awake() {
            _collider = GetComponent<Collider>();
        }

        public void Interact() {
            return;
        }

        public void InteractNotTrigger() {
            if (!_isPickedUp) {
               
                AddToStack();
            }
            else {
                RemoveFromStack();
            }

            OnInteractNotTrigger?.Invoke(_isPickedUp, transform);
        }

        private void AddToStack() {
            _isPickedUp = true;
        }

        private void RemoveFromStack() {
            _isPickedUp = false;
        }
    }
}