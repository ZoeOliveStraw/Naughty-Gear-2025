namespace Character.Scripts
{
    public class PlayerState_Prone : PlayerState_Abstract
    {
        public override void EnterState(PlayerStateController stateController)
        {
            base.EnterState(stateController);
            
            _animator.SetBool("IsStanding", false);
            _animator.SetBool("IsCrouching", false);
            _animator.SetBool("IsProne", true);

            _input.Player.Jump.performed += _ => TryStand();
            _input.Player.Crouch.performed += _ => TryCrouch();
            _input.Player.Prone.performed += _ => TryStand();
        }
        
        private void TryCrouch()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Crouching);
        }

        private void TryStand()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Standing);
        }
    }
}
