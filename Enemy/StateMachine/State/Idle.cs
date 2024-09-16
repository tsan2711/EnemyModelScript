

using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{
    private readonly IEnemy _enemy;
    public float idleTime = 7f;
    public float timer = 0f;
    public Idle(IEnemy enemy){
        Debug.Log("Idle Constructor");
        _enemy = enemy;

    }
    public void Tick()
    {
        timer += Time.deltaTime;
    }
    public void OnEnter()
    {
        timer = 0f;
        Debug.Log("Idling");
    }

    public void OnExit()
    {
    }
}
