using Unity.VisualScripting;
using UnityEngine;

public class AttackState_EnemyMelee : EnemyState
{
    public AttackState_EnemyMelee(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
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
            _machine.ChangeState<IdleState_EnemyMelee>();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
