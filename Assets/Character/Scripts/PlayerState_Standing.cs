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
        }
    }
}
