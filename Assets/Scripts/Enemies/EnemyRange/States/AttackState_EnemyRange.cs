using Unity.VisualScripting;
using UnityEngine;

public class AttackState_EnemyRange : EnemyState
{
    public AttackState_EnemyRange(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        if (Istrigger)
        {
            Debug.Log("IStrigger");
            _machine.ChangeState<IdleState_EnemyRange>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
