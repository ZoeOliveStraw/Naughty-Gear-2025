using Character.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private PlayerState_Abstract stateStanding;
    [SerializeField] private PlayerState_Abstract stateCrouching;
    [SerializeField] private PlayerState_Abstract stateAirborn;
    [SerializeField] private PlayerState_Abstract stateCrawling;
    
    [SerializeField] public CharacterController characterController;
    [SerializeField] public BoxCollider boxCollider;
    [SerializeField] public PlayerRaycasts playerRaycasts;
    [SerializeField] public Animator animator;
    [SerializeField] public PlayerMovement playerMovement;

    [SerializeField] private Enum_PlayerStates initialState;
    
    private PlayerState_Abstract _currentState;
    private Enum_PlayerStates _currentStateEnum;
    private Enum_PlayerStates _previousStateEnum;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetState(initialState);
    }
    
    public void SetState(Enum_PlayerStates state)
    {
        //Debug.LogWarning($"ENTERING STATE: {state}");
        _previousStateEnum = _currentStateEnum;
        _currentStateEnum = state;
        
        switch (state)
        {
            case Enum_PlayerStates.Standing:
                ChangeState(stateStanding);
                break;
            case Enum_PlayerStates.Crouching:
                ChangeState(stateCrouching);
                break;
            case Enum_PlayerStates.Crawling:
                ChangeState(stateCrawling);
                break;
            case Enum_PlayerStates.Airborne:
                ChangeState(stateAirborn);
                break;
        }
    }
    
    private void ChangeState(PlayerState_Abstract newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
            _currentState.enabled = false;
        }

        _currentState = newState;
        _currentState.enabled = true;
        newState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState();
    }
}
