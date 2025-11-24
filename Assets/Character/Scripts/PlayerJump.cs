using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Scripts
{
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerRaycasts playerRaycasts;
        [FormerlySerializedAs("gravityPerFrame")] [SerializeField] private float gravityPerSecond;

        [SerializeField] private float jumpForce;
        private InputSystem_Actions _controls;
        private float _currentVerticalVelocity = -2f;

        private void Awake()
        {
            _controls = new InputSystem_Actions();
        }

        // Update is called once per frame
        void Update()
        {
            if (_controls.Player.Jump.WasPerformedThisFrame() && playerRaycasts.IsGrounded)
            {
                _currentVerticalVelocity = jumpForce;
            }
            else if(playerRaycasts.IsGrounded)
            {
                Debug.LogWarning("TRUE");
                //_currentVerticalVelocity = Mathf.Clamp(_currentVerticalVelocity - gravityPerSecond * Time.deltaTime, -1f, jumpForce);
            }
            
            Vector3 velocity = Vector3.zero;
            velocity.y = _currentVerticalVelocity * Time.deltaTime;
            playerMovement.MoveCharacter(velocity);
            _currentVerticalVelocity -= gravityPerSecond * Time.deltaTime;
            
            Debug.LogWarning($"Current vertical velocity: {_currentVerticalVelocity}");
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
