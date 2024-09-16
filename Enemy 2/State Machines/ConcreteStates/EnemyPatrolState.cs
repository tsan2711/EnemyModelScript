using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyState
{

    private float _patrolTime = 5f;
    private float _patrolTimer = 0f;
    private Vector3 _direction;
    private Vector3 _patrolPosition;
    private NavMeshTriangulation triangulation;

    public EnemyPatrolState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        triangulation = NavMesh.CalculateTriangulation();
    }


    public override void EnterState()
    {
        base.EnterState();
        _patrolPosition = GetRandomPosition();
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if(enemy.IsAggroed){
            if(enemy.StateMachine.CurrentEnemyState != enemy.MoveState){
                enemy.StateMachine.ChangeState(enemy.MoveState);
            }
        }

        enemy.Agent.SetDestination(_patrolPosition);    

        // make sure that enemy can reach the destination
        if(enemy.Agent.remainingDistance >= .1f) return;


        if (Vector3.Distance(enemy.transform.position, _patrolPosition) < .1f)
        {
            _patrolTimer += Time.deltaTime;
            if (_patrolTimer > _patrolTime)
            {
                _patrolTimer = 0f;
                enemy.StateMachine.ChangeState(enemy.IdleState);
            }
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

    private Vector3 GetRandomPosition()
    {
        int vertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }

}
