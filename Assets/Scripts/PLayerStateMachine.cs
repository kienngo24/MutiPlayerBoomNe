using System;
using System.Collections.Generic;
using UnityEngine;

public class PLayerStateMachine : IStateMachine
{
    private Dictionary<Type,IState> _statesDirtionary = new Dictionary<Type,IState>();

    private IState _curState;
    private readonly IStateFactory _factory;
    public PLayerStateMachine(PlayerController player)
    {
        _factory = new PlayerStateFactory(player, this);
        CreateState();
    }
    public void CreateState() => _statesDirtionary = CreateStateFactory.Instance.GenerateState(_factory);

    public void Init<T>()  where T : IState 
    {
        if(GetState<T>() == null)
            return;
        SetState(GetState<T>());

        _curState.Enter();
        
    }
    public void ChangeState<T>() where T : IState 
    {
        if(GetState<T>() == null)
            return;
        _curState.Exit();
        Init<T>();
        

    }
    
    public void SetState(IState curState) => _curState = curState;
    public IState GetState<T>() where T : IState => _statesDirtionary[typeof(T)];
    
    

    public void Update()
    {
        if (_curState != null)
           _curState.Excute();
    }
    

}
