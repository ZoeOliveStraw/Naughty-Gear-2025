using System;
using UnityEngine;

namespace Character.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        //Component references
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cameraTransform;
    
        public float movementSpeed = 5f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
    
        private InputSystem_Actions _controls;
        private Vector3 moveDir = Vector3.zero;
        private Vector3 currentMoveVelocity = Vector3.zero;

        private void Awake()
        {
            _controls = new InputSystem_Actions();
        }

        // Update is called once per frame
        void Update()
        {
            CalculateInputVector();
        }

        private void CalculateInputVector()
        {
            //camera values
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;
        
            //input values
            Vector2 inputVector = _controls.Player.Move.ReadValue<Vector2>();
        
        
            //Apply movement
            moveDir = forward * inputVector.y + right * inputVector.x;
            MoveCharacter(moveDir * movementSpeed * Time.deltaTime);
            
            if (inputVector != Vector2.zero)
            {
                Quaternion target = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
            }
        }
        
        public void MoveCharacter(Vector3 movement)
        {
            currentMoveVelocity += movement;
        }

        private void LateUpdate()
        {
            controller.Move(currentMoveVelocity);
            currentMoveVelocity = Vector3.zero;
        }

        private void OnEnable()
        {
            _controls.Enable();
        }
    
        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}
