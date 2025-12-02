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

        private void Awake()
        {
            _input = new InputSystem_Actions();
        }

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
