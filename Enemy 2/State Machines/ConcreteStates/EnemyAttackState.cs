

using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private Transform _playerAttackTransform;
    private Transform _playerAggroTransform;

    private float _timer = 0f;
    private float _timeBetweenAttacks = 2f;

    private float _exitTimer;
    private float _timeTillExit = 2f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        if (enemy.GetEnemyInAttackRange().playerAttacking == null) return;

        _playerAttackTransform = enemy.GetEnemyInAttackRange().playerAttacking;
        _playerAggroTransform = enemy.GetEnemyAggroCheck().playerChasing;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.Move(Vector3.zero);

        if (_timer > _timeBetweenAttacks)
        {
            _timer = 0f;

            enemy.Attack();
        }

        var playerInRangeAggro = enemy.GetEnemyAggroCheck().playerChasing; 
        var playerInRangeAttack = enemy.GetEnemyInAttackRange().playerAttacking;

        if (playerInRangeAggro == null && playerInRangeAttack == null)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        }

        if (playerInRangeAttack == null)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.StateMachine.ChangeState(enemy.MoveState);
                return;
            }

        }
        else
        {
            _exitTimer = 0f;
        }
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
