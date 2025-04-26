using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IStateMachine _playerSM;
    public Animator _amin;
    private void Awake() {
        
        _amin = GetComponent<Animator>();
        _playerSM = new PLayerStateMachine(this);   
    }
    private void Start() {
        _playerSM.Init<IdleState_Player>();
    }
    private void Update() 
    {
        if(_playerSM != null)
            _playerSM.Update();
    }
}
