using UnityEngine;

namespace NPCs.Scripts
{
    public class State_Search : State_Abstract
    {
        [SerializeField] private float searchRadius;
        [SerializeField] private Color visionConeColor;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float timeToSeesPlayer;
        [SerializeField] private float waitAtStopDuration = 1;

        private Vector3 _currentTarget;
        private Vector3 _startPosition;
        private float _timeElapsed;
        private float _searchDuration;
        private bool _isWaiting;
        private float _currentStopDuration;

        public override void EnterState(GuardStateController controller)
        {
            base.EnterState(controller);
            _searchDuration = GuardStateController.timeToSearch;
            GuardStateController.timeToSeePlayer = timeToSeesPlayer;
            _navMeshAgent.speed = moveSpeed;
            _vision.SetVisionConeColor(visionConeColor);
            _startPosition = transform.position;
            _timeElapsed = 0;
        }

        public override void UpdateState()
        {
            if (_vision.CanSeeObjectWithTag("Player"))
            {
                GuardStateController.SetState(Enum_GuardStates.CanSeePlayer);
            }
        
            if (_timeElapsed == 0)
            {
                NavigateToNextNode();
            }
        
            if (_navMeshAgent.remainingDistance == 0 && _currentStopDuration < waitAtStopDuration)
            {
                Debug.LogWarning($"WAITING. Current stop duration: {_currentStopDuration}");
                _isWaiting = true;
            }

            if (_isWaiting)
            {
                _currentStopDuration += Time.deltaTime;
            }

            if (_navMeshAgent.remainingDistance == 0 && _currentStopDuration >= waitAtStopDuration)
            {
                NavigateToNextNode();
                _currentStopDuration = 0;
                _isWaiting = false;
            }

            if (_timeElapsed >= _searchDuration && _navMeshAgent.remainingDistance == 0)
            {
                GuardStateController.SetState(Enum_GuardStates.Patrol);
            }
            _timeElapsed += Time.deltaTime;
        }
    
        private void NavigateToNextNode()
        {
            if (SearchNodeManager.Instance != null )
            {
                _currentTarget = SearchNodeManager.Instance.GetRandomNodeInRange(_startPosition, searchRadius);
            }
            _navMeshAgent.SetDestination(_currentTarget);
        }
    }
}
