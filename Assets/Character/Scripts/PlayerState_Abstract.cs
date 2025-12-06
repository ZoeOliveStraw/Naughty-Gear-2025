using System;
using UnityEngine;

namespace Character.Scripts
{
    public class PlayerState_Abstract : MonoBehaviour
    {
        protected PlayerStateController _stateController;
        protected InputSystem_Actions _input;
        protected BoxCollider _boxCollider;
        protected CharacterController _characterController;
        protected PlayerRaycasts _playerRaycasts;
        protected Animator _animator;
        protected PlayerMovement _playerMovement;

        [SerializeField] public float moveSpeed = 5;
        [SerializeField] private Vector3 colliderDimensions;
        [SerializeField] private float colliderVerticalPosition; 

        private void Awake()
        {
            _input = new InputSystem_Actions();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void EnterState(PlayerStateController stateController)
        {
            
            _stateController = stateController;
            _boxCollider = stateController.boxCollider;
            _characterController = stateController.characterController;
            _playerRaycasts = stateController.playerRaycasts;
            _animator = stateController.animator;
            _playerMovement = stateController.playerMovement;
            _playerMovement.movementSpeed = moveSpeed;
            SetCollisionDimensions();
        }

        // Update is called once per frame
        public virtual void UpdateState()
        {
            
        }

        public virtual void ExitState()
        {
            
        }

        private void SetCollisionDimensions()
        {
            _boxCollider.size = colliderDimensions;
            _boxCollider.center = new Vector3(0, colliderVerticalPosition, 0);
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
