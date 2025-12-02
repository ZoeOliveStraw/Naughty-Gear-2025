using System;
using UnityEngine;

namespace Character.Scripts
{
    public class PlayerState_Abstract : MonoBehaviour
    {
        protected PlayerStateController _stateController;
        protected InputSystem_Actions _input;
        protected CapsuleCollider _capsuleCollider;
        protected CharacterController _characterController;
        protected PlayerRaycasts _playerRaycasts;
        protected Animator _animator;
        protected PlayerMovement _playerMovement;

        [SerializeField] public float moveSpeed = 5;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void EnterState(PlayerStateController stateController)
        {
            _stateController = stateController;
            _capsuleCollider = stateController.capsuleCollider;
            _characterController = stateController.characterController;
            _playerRaycasts = stateController.playerRaycasts;
            _animator = stateController.animator;
            _playerMovement = stateController.playerMovement;
            _playerMovement.movementSpeed = moveSpeed;
        }

        // Update is called once per frame
        public virtual void UpdateState()
        {
            
        }

        public virtual void ExitState()
        {
            
        }
    
        protected float RotateTowardTarget()
        {
            Vector2 dir = _input.Player.Move.ReadValue<Vector2>();
            Vector3 targetPos = new Vector3(dir.x, 0, dir.y);
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            float angleToTarget = Quaternion.Angle(transform.rotation, targetRotation);
            //Debug.LogWarning($"Angle: {angleToTarget}");
            return angleToTarget;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}
