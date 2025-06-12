using UnityEngine;

public interface IState
{
    bool Istrigger { get; set; }
    public void Enter();
    public void Excute();
    public void Exit();
    public void SetAnimationTrigger();
}
