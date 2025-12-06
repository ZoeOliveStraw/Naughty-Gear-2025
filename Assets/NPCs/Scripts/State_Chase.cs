using UnityEngine;

namespace NPCs.Scripts
{
    public class State_Chase : State_Abstract
    {
        [SerializeField] private Color visionConeColor;
        [SerializeField] private float moveSpeed;
    
        private bool _canSeePlayer;
        private Transform _playerTransform;
        private Vector3 _lastKnownPlayerPosition;
    
        public override void EnterState(GuardStateController controller)
        {
            base.EnterState(controller);
            _navMeshAgent.speed = moveSpeed;
            SetPlayerReference();
            _vision.SetVisionConeColor(visionConeColor);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _canSeePlayer = CanSeePlayer();
            if(_canSeePlayer) UpdatePlayerPosition();
            float distanceToTarget = _navMeshAgent.remainingDistance;
            if (distanceToTarget < 0.1f)
            {
                if (_canSeePlayer)
                {
                    Debug.LogWarning("PLAYER CAUGHT!");
                }
                else
                {
                    Debug.LogWarning("PLAYER LOST, EXITING STATE");
                    GuardStateController.SetState(Enum_GuardStates.LookAround);
                }
            }
        }

        private void SetPlayerReference()
        { 
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private bool CanSeePlayer()
        {
            return _vision.CanSeeObjectWithTag("Player");
        }

        private void UpdatePlayerPosition()
        {
            if (Vector3.Distance(_playerTransform.position, _lastKnownPlayerPosition) > 0.1f)
            {
                _lastKnownPlayerPosition = _playerTransform.position;
            }
            _navMeshAgent.SetDestination(_lastKnownPlayerPosition);
        }
    }
}
