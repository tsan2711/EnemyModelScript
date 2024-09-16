using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInAttackRange : MonoBehaviour
{
    private List<Transform> playersInRangeAttack = new List<Transform>();


    private Enemy _enemy;
    public Transform playerAttacking;

    private void Start()
    {
        playerAttacking = null;
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playersInRangeAttack.Add(other.transform);

        if(playerAttacking != null) return;

        playerAttacking = other.transform;

        _enemy.SetInAttackRange(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playersInRangeAttack.Remove(other.transform);

        if(playerAttacking == other.transform) {
            playerAttacking = playersInRangeAttack.Find(x => x != other.transform && x != null);
        }

        if (playersInRangeAttack.Count == 0)
        {
            playerAttacking = null;
            _enemy.SetInAttackRange(false);
        }


    }
}
