using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Player;
using Project.Runner;

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

        private void FixedUpdate() {
            WaveNodes();
        }

        private void WaveNodes() {
           
            for (int i = 1; i < _stack.Count; i++) {
                if (!_stack[i].GetComponent<PickUp>().finishedPickingUp) {
                    continue;
                }
                Vector3 nodePosition = _stack[i].localPosition;
                Vector3 moveVector = new Vector3(PlayerMovementModule.Instance.dynamicJoystick.Horizontal, 0, PlayerMovementModule.Instance.dynamicJoystick.Vertical);

                nodePosition = Vector3.Lerp(nodePosition, new Vector3(nodePosition.x, nodePosition.y, (i * 0.11f) * -(moveVector.magnitude)), (_lerpDuration * i) * Time.deltaTime);


                //Vector3 previousNodePosition = _stack[i - 1].localPosition;
                
                /*nodePosition = new Vector3(
                    nodePosition.x,
                    i * _spaceBetweenNodes,
                    Mathf.Lerp(nodePosition.z, previousNodePosition.z, Time.deltaTime * _lerpDuration));*/

                _stack[i].localPosition = nodePosition;
            }
        }
        private void UpdateStack(bool isPickedUp, Transform node) {
            if (isPickedUp) {
                
                node.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                _stack.Add(node);
                float yPosition = _stack.Count * _spaceBetweenNodes;
                node.DOJump(new Vector3(0,5f,0), 1f, 1, 1f).OnComplete(()=> {
                    
                    node.SetParent(_stackParent);
                    
                    node.DOLocalMove(new Vector3(0,yPosition,0), 1f).OnComplete(()=> {
                        node.localPosition = new Vector3(0,yPosition,0);
                        node.localRotation = Quaternion.Euler(0,0,0);
                        node.GetComponent<PickUp>().finishedPickingUp = true;
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