using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public interface IEnemy
{
    GameObject GameObject { get; }
    NavMeshAgent GetAgent { get; }
    int Hp {get; set;}
    int AttackValue {get; set;}
    string Name {get; set;}
    float RangeDetection {get; set;}
    public void Attack();

    public void TakeDamage(int damage);

    public void Die();

    public void SetPool(ObjectPool<IEnemy> _pool);
}
