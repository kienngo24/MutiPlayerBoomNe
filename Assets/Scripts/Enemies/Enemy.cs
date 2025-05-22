using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public Animator _anim;
    public Transform target;
    protected IStateMachine _enemySM;
    protected virtual void Awake()
    {
        _anim = GetComponent<Animator>();
        _enemySM = new EnemyMeleeStateMachine(this);
        Invoke(nameof(FindTarget), 1);
    }
    public void FindTarget()
    {
        target = PlayerController.Instance.transform;
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}
