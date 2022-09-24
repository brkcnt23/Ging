using System;
using UnityEngine;
using Interfaces;

namespace Objects {
    public class FollowUp : MonoBehaviour, IInteract {
        public static Action<bool, Transform> OnInteract;

        private bool _isFollowedUp;
        private Collider _collider;

        private void Awake() {
            _collider = GetComponent<Collider>();
        }

        public void Interact() {
            if (!_isFollowedUp) {
                AddToStack();
            }
            else {
                RemoveFromStack();
            }

            OnInteract?.Invoke(_isFollowedUp, transform);
        }

        public void InteractNotTrigger() {
            return;
        }

        private void AddToStack() {
            _isFollowedUp = true;
            _collider.isTrigger = false;
        }

        private void RemoveFromStack() {
            _isFollowedUp = false;
            _collider.isTrigger = true;
            gameObject.SetActive(false);
        }
    }
}