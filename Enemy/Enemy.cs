using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;

public class Enemy : MonoBehaviour, IEnemy
{
    private StateMachine _stateMachine;

    private void Awake() {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        var animator = GetComponent<Animator>();
        var playerToChaseDetector = gameObject.AddComponent<PlayerToChaseDetector>();


        _stateMachine = new StateMachine();

        var idle = new Idle(this);
        var patrol = new Patrol(this, navMeshAgent, animator);
        var chase = new Chase(this, navMeshAgent, animator, playerToChaseDetector);

        // Add At transitions
        At(idle, patrol, IsIdleTimerComplete());
        At(patrol, idle, IsPatrolTimerComplete());
        At(chase, idle, IsPlayerNotInRange());
        


        // Add Any transitions 
        Any(chase, IsPlayerInRange());
        
        _stateMachine.SetState(idle);
    

        // Settings
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        void Any(IState state, Func<bool> condition) => _stateMachine.AddAnyTransition(state, condition);
    
        // Conditions
        Func<bool> IsIdleTimerComplete() => () => idle.timer >= idle.idleTime;
        Func<bool> IsPatrolTimerComplete() => () => patrol.timer >= patrol.patrolTime;
        Func<bool> IsPlayerInRange() => () => playerToChaseDetector.PlayerInRange;
        Func<bool> IsPlayerNotInRange() => () => !playerToChaseDetector.PlayerInRange;
    }

    private void Update() => _stateMachine.Tick();

}
