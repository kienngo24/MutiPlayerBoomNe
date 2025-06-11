using Unity.VisualScripting;
using UnityEngine;

public class MoveState_EnemyMelee : GroundState_EnemyMelee
{
    private Transform target;
    public MoveState_EnemyMelee(Enemy enemy, IStateMachine stateMachine, string animName) : base(enemy, stateMachine, animName)
    {
    }
    
    public override void Enter()
    {
        target = _enemy.target;
        base.Enter();
    }
    public override void Excute()
    {
        base.Excute();
        
        if (target == null)
        {
            _machine.ChangeState<IdleState_EnemyMelee>();
            return;
        }
        _enemy.transform.position = 
                Vector3.MoveTowards(_enemy.transform.position,target.transform.position,_enemy.speed * Time.deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
        _enemy.transform.position += Vector3.zero;
    }
}
