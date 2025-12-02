namespace Character.Scripts
{
    public class PlayerState_Crouching : PlayerState_Abstract
    {
        public override void EnterState(PlayerStateController stateController)
        {
            base.EnterState(stateController);
            
            _animator.SetBool("IsStanding", false);
            _animator.SetBool("IsCrouching", true);
            _animator.SetBool("IsProne", false);

            _input.Player.Crouch.performed += _ => TryStand();
            _input.Player.Jump.performed += _ => TryStand();
            _input.Player.Prone.performed += _ => TryProne();
        }
        
        private void TryStand()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Standing);
        }

        private void TryProne()
        {
            if(_playerRaycasts.IsGrounded) _stateController.SetState(Enum_PlayerStates.Crawling);
        }
    }
}
