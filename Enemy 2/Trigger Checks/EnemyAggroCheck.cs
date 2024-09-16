using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    private List<Transform> playersInAggroRange = new List<Transform>();

    private Enemy _enemy;
    public Transform playerChasing;

    private void Start()
    {
        Debug.Log("Aggro Check Start");
        playerChasing = null;
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playersInAggroRange.Add(other.transform);
        playerChasing = other.transform;

        _enemy.SetAggroed(true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playersInAggroRange.Remove(other.transform);

        if(playerChasing == other.transform){
            playerChasing = playersInAggroRange.Find(x => x != other.transform && x != null);
        }


        _enemy.StateMachine.ChangeState(_enemy.IdleState);

        _enemy.StateMachine.ChangeState(_enemy.MoveState);

        if(playersInAggroRange.Count == 0)
        {
            _enemy.SetAggroed(false);
            return;
        }
    }


}
