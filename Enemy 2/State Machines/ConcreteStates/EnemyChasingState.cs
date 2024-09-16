

using System;
using UnityEngine;

public class EnemyChasingState : EnemyState
{
    private Transform _playerTransform;
    public EnemyChasingState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        var playerChasing = enemy.GetEnemyAggroCheck().playerChasing;
        if (playerChasing == null)
        {
            Debug.LogError("Player is null in EnemyMovingState");
            return;
        }
        if(_playerTransform == playerChasing) return;
        _playerTransform = playerChasing;
    }
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        }

        enemy.Agent.SetDestination(_playerTransform.position); 

        if (enemy.IsInAttackRange) enemy.StateMachine.ChangeState(enemy.AttackState);


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }


}
