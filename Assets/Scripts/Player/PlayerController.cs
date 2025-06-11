using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private IStateMachine m_playerSM;
    public Animator m_amin;
    public IMovement m_movement;
    public ISword m_Sword;
    
    private void Awake()
    {
        m_amin = GetComponent<Animator>();
        m_playerSM = new PLayerStateMachine(this);
        m_movement = GetComponent<IMovement>();
    }



    private void Start() {
        m_playerSM.Init<IdleState_Player>();
    }
    private void Update() 
    {
        if(m_playerSM != null)
            m_playerSM.Update();
    }
}
