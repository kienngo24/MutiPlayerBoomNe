using System;
using System.Collections.Generic;
using UnityEngine;

public interface IStateMachine
{
    void CreateState();
    void Init<T>()  where T : IState;
    void ChangeState<T>() where T : IState;
    void SetState(IState curState);
    IState GetState<T>()  where T : IState;
    void Update();
}
