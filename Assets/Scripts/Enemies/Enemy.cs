using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    public float speed = 20;
    public float turnSpeed = 5;
    [Header("Enemy Attack")]
    [SerializeField] private Transform attackPoint;



    public bool facingRight = true;
    public int facingDir = 1;

    public float attackRange;
    public float damage;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator _anim;
    public Transform target;
    protected IStateMachine _enemySM;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        FindTarget();
        SetupDefaultDir(false);
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
        if (target == null)
            return;

        int newDirection = (target.position.x < transform.position.x) ? -1 : 1;
        if (newDirection != facingDir)
        {
            Debug.Log("ChangeDir");
            FlipController(newDirection);
        }
    }
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    public void SetupDefaultDir(bool right)
    {
        if (right)
        {
            facingDir = 1;
            facingRight = right;
        }
        else
        {
            facingDir = -1;
            facingRight = right;
        }
    }
    public bool CanAttack() => Vector2.Distance(attackPoint.position, target.position) < attackRange;
    public void AnimationtriggerBase() => _enemySM.GetCurrentState().SetAnimationTrigger();
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
