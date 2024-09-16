using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyState
{

    private NavMeshTriangulation triangulation;

    private float _idleTime = 10;
    private float _idleTimer = 0f;

    
    public EnemyIdleState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        triangulation = NavMesh.CalculateTriangulation();
    }

    public override void EnterState()
    {
        base.EnterState();
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
        _idleTimer += Time.deltaTime;

        if(_idleTimer > _idleTime){
            _idleTimer = 0f;
            enemy.StateMachine.ChangeState(enemy.PatrolState);
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
