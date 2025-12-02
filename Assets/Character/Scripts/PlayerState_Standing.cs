namespace Character.Scripts
{
    public class PlayerState_Standing : PlayerState_Abstract
    {
        public override void EnterState(PlayerStateController stateController)
        {
            base.EnterState(stateController);
            
            _animator.SetBool("IsStanding", true);
            _animator.SetBool("IsCrouching", false);
            _animator.SetBool("IsProne", false);

            _input.Player.Crouch.performed += _ => TryCrouch();
            _input.Player.Prone.performed += _ => TryProne();
            _input.Player.Jump.performed += _ => TryJump();
        }

        private void TryCrouch()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Crouching);
        }

        private void TryProne()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Crawling);
        }

        private void TryJump()
        {
            
        }
    }
}
