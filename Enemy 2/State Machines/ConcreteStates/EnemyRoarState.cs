using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoarState : EnemyState
{
    public EnemyRoarState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
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
