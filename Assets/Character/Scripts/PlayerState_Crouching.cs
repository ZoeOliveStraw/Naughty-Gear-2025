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
        }
    }
}
