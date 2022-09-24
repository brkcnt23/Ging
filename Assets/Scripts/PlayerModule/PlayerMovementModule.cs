using System;
using Cinemachine;
using UnityEngine;
namespace Project.Runner {
    public class PlayerMovementModule : PlayerMobuleBase {
        
        #region Module Data
        
        public static PlayerMovementModule Instance { get; private set; }
        
        public CinemachineVirtualCamera virtualCamera;
        Vector3 difference;
        Vector3 originalPosition;
        bool dragging;
        
        public GameObject VisualHolder;
        
        public float moveSpeed;
        public DynamicJoystick dynamicJoystick;
        /*CinemachineVirtualCamera vcam = ...;
        var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = bla;*/

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public override void Initialize(PlayerMainModule mainModule) {
            base.Initialize(mainModule);
        }
        
        public override void Activate() {
            base.Activate();
        }

        public override void Deactivate() {
            base.Deactivate();
        }
        
        protected override void OnPlayerGeneralStateChanged(PlayerGeneralState generalState) {
            base.OnPlayerGeneralStateChanged(generalState);
        }

        protected override void OnPlayerMovementStateChanged(PlayerMovementState movementState) {
            base.OnPlayerMovementStateChanged(movementState);
        }

        protected override void UnUpdate() {
            base.UnUpdate();

        }

        protected override void UnFixedUpdate() {
            base.UnFixedUpdate();
        }
        
        

        private void FixedUpdate()
        {
            Vector3 moveVector = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
            if (moveVector != Vector3.zero)
            {
                VisualHolder.transform.rotation = Quaternion.LookRotation(moveVector);
            }
        }

        private void LateUpdate()
        {
            /*if (Input.GetMouseButton(0))
            {
                difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - virtualCamera.transform.position;
                if(dragging == false)
                {
                    dragging = true;
                    originalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    dragging = false;
                }

                if (dragging)
                {
                    virtualCamera.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.x = originalPosition.x - difference.x;
                }
            }*/
        }

        protected override void UnLateUpdate() {
            base.UnLateUpdate();
            
        }
        
        public override void Uninitialized() {
            base.Uninitialized();
        }
        
        #endregion
        
    }
}