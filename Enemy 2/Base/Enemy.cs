using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, IEnemy, IDamagable, IMoveable, ITriggerCheckable
{
    private ObjectPool<IEnemy> pool;
    [SerializeField] private EnemyAggroCheck EnemyAggroCheck;
    [SerializeField] private EnemyInAttackRange EnemyInAttackRange;


    #region NavMesh Variables
    public NavMeshAgent Agent { get; set; }


    #endregion

    #region State Machine Variables

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChasingState MoveState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyPatrolState PatrolState { get; set; }

    #endregion

    #region Implemented IDamagable Variables
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    #endregion

    #region Implemented IMoveable Variables
    public Rigidbody RB { get; set; }
    [field: SerializeField] public float Speed { get; set; } = 1f;
    public bool IsMoving { get; set; } = false;

    #endregion

    #region  ITriggerCheckable Variables
    public bool IsAggroed { get; set; }
    public bool IsInAttackRange { get; set; }

    #endregion


    #region Implemented IEnemy Variables
    public GameObject GameObject => gameObject;

    public NavMeshAgent GetAgent => Agent;

    [field: SerializeField] public int Hp { get; set; } = 100;
    [field: SerializeField] public int AttackValue { get; set; } = 10;
    [field: SerializeField] public string Name { get; set; } = "No Name";
    [field: SerializeField] public float RangeDetection { get; set; } = 5f;



    #endregion


    #region MonoBehaviour Callbacks
    private void Awake()
    {

        InitialSetUpStateMachine();


        Agent = GetComponent<NavMeshAgent>();
    }
    private void InitialSetUpStateMachine()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        MoveState = new EnemyChasingState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
        PatrolState = new EnemyPatrolState(this, StateMachine);

        StateMachine.Initialize(IdleState);
    }



    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }


    private void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }




    #endregion

    #region Implemented IDamagable Methods

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth > 0) return;

        Die();
    }

    public void Die()
    {
        Debug.Log("Dying");
    }


    #endregion


    #region Implemented IMoveable Methods

    public void Move(Vector3 direction)
    {
        IsMoving = true;


        Agent.Move(direction * Speed * Time.deltaTime);
        // RB.velocity = direction * Speed;
    }

    #endregion

    #region Implemented ITriggerCheckable Methods

    public void SetAggroed(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetInAttackRange(bool isInAttackRange)
    {   
        IsInAttackRange = isInAttackRange;
    }

    #endregion 

    #region Implemented IEnemy Methods
    public void Attack()
    {
        Debug.Log("Attacking");
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage");
    }

    public void SetPool(ObjectPool<IEnemy> _pool)
    {
        _pool = pool;
    }


    #endregion

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }



    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstep
    }

    #endregion


    #region  Getters and Setters

    public EnemyAggroCheck GetEnemyAggroCheck() => EnemyAggroCheck == null ? EnemyAggroCheck = GetComponentInChildren<EnemyAggroCheck>() : EnemyAggroCheck;
    public EnemyInAttackRange GetEnemyInAttackRange() => EnemyInAttackRange == null ? EnemyInAttackRange = GetComponentInChildren<EnemyInAttackRange>() : EnemyInAttackRange;

    #endregion


    #region Others



    #endregion
}
