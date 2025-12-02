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
        }
    }
}
