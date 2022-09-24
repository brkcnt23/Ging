using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Player;

namespace Objects {
    public class StackController : MonoBehaviour {
        public static StackController Instance;
        [SerializeField] private Transform _pickUpParent;
        [SerializeField] private float _spaceBetweenNodes;
        [SerializeField] private float _lerpDuration;

        private Transform _stackParent;
        private Transform _playerTransform;
        private List<Transform> _stack = new List<Transform>();
        public int ListCount => _stack.Count;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            Setup();
            
        }

        private void Update() {
            // Simple fix for the child-parent-parent position relationship.
            //transform.localPosition = new Vector3(0, _playerTransform.position.y * 1, 0);
        }

        private void FixedUpdate() {
           // WaveNodes();
        }

        public void TakeItAll() {
               // when pickup a node take it to translate i
        }

        private void WaveNodes() {
            for (int i = 1; i < _stack.Count; i++) {
                Vector3 nodePosition = _stack[i].localPosition;
                Vector3 previousNodePosition = _stack[i - 1].localPosition;
                nodePosition = new Vector3(
                    Mathf.Lerp(nodePosition.x, previousNodePosition.x, Time.deltaTime * _lerpDuration),
                    i * -_spaceBetweenNodes,
                    nodePosition.z);

                _stack[i].localPosition = nodePosition;
            }
        }
        private void UpdateStack(bool isPickedUp, Transform node) {
            if (isPickedUp) {
                
                node.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                
                node.DOJump(new Vector3(0,5f,0), 1f, 1, 1f).OnComplete(()=> {
                    _stack.Add(node);
                    node.SetParent(_stackParent);
                    node.DOLocalMove(Vector3.zero, 1f).OnComplete(()=> {
                        node.localPosition = new Vector3(0,ListCount * _spaceBetweenNodes,0);
                        node.localRotation = Quaternion.Euler(0,0,0);
                    });
                    
                });
                
            }
            else {
                _stack.Remove(node);
                node.SetParent(_pickUpParent);
            }
        }

        public void Setup() {
            _stackParent = transform;
            _playerTransform = FindObjectOfType<PlayerController>().transform;
            _stack.Add(_playerTransform);
            PickUp.OnInteractNotTrigger += UpdateStack;
        }
    }
}