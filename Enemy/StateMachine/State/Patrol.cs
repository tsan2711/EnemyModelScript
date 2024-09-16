using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : IState
{
    private readonly IEnemy _enemy;
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Animator _animator;
    private static NavMeshTriangulation triangulation;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private float _patrolSpeed = 3.5f;
    private float _initialSpeed;
    public float patrolTime = 5f;
    public float timer = 0f;
    public Patrol(IEnemy enemy, NavMeshAgent navMeshAgent, Animator animator)
    {
        Debug.Log("Patrol Constructor");
        _enemy = enemy;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        if(triangulation.vertices == null) triangulation = NavMesh.CalculateTriangulation();
    }
    
    public void Tick()
    {
        timer += Time.deltaTime;
    }

    public void OnEnter()
    {
        timer = 0f;
        _initialSpeed = _navMeshAgent.speed;
        _animator.SetFloat(Speed, 1f);
        _navMeshAgent.SetDestination(GetRandomPoint());

        Debug.Log("Patrolling");
    }

    public void OnExit()
    {
        _animator.SetFloat(Speed, 0);
        _navMeshAgent.speed = _initialSpeed;
        Debug.Log("Patrol -> Idle");
    }

    private Vector3 GetRandomPoint(){
        var randomPoint = triangulation.vertices[Random.Range(0, triangulation.vertices.Length)];
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 10, NavMesh.AllAreas)){
            return hit.position;
        }
        Debug.LogError("Failed to find a random point");
        return Vector3.zero;
    }
}
