using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : IState
{
    private readonly IEnemy _enemy;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private readonly PlayerToChaseDetector _playerToChaseDetector;
    private float _chaseSpeed = 5f;
    private float _initialSpeed;
    public Chase(IEnemy enemy, NavMeshAgent navMeshAgent, Animator animator, PlayerToChaseDetector playerToChaseDetector){
        _enemy = enemy;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _playerToChaseDetector = playerToChaseDetector;
    }
    public void Tick()
    {
        if(_playerToChaseDetector.PlayerInRange){
            _navMeshAgent.SetDestination(_playerToChaseDetector.detectPlayer.position);
        } else {
            _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
        }
    }

    public void OnEnter()
    {
        _initialSpeed = _navMeshAgent.speed;
        _navMeshAgent.speed = _chaseSpeed;
        _animator.SetFloat("Speed", 1);
    }

    public void OnExit()
    {
        _animator.SetFloat("Speed", 0);
        _navMeshAgent.speed = _initialSpeed;
    }



}
