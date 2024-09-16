using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{

    [Header("Enemy Spawner Settings")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    public SpawnMethod spawnMethod;

    public int maxEnemies = 5;

    public float spawnRate = 1f;

    private int currentEnemyIndex = 0;
    private NavMeshTriangulation triangulation;



    private ObjectPool<IEnemy> EnemyObjectPool;


    private void Awake()
    {
        triangulation = NavMesh.CalculateTriangulation();
        Debug.Log("Triangulation Vertices Count: " + triangulation.vertices.Length);
    }

    private void Start()
    {
        spawnMethod = SpawnMethod.RoundRobin;

        EnemyObjectPool = new ObjectPool<IEnemy>(
            CreateEnemy,
            OnTakeEnemyFromPool,
            OnReturnEnemyFromPool,
            OnDestroyEnemy,
            true,
            100,
            2000
            );

        StartCoroutine(SpawnEnemies());

    }





    #region Spawning

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (EnemyObjectPool.CountAll < maxEnemies)
            { // Need To Check Again

                IEnemy enemy = EnemyObjectPool.Get();
                int vertexIndex = UnityEngine.Random.Range(0, triangulation.vertices.Length);
                Debug.Log("Vertex Index: " + vertexIndex);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(triangulation.vertices[vertexIndex], out hit, 2.0f, NavMesh.AllAreas))
                {
                    var agent = enemy.GetAgent;
                    if(agent.Warp(hit.position)){
                        Debug.Log("Agent Warped");
                        agent.enabled = true;
                    } else {
                        Debug.Log("Agent Not Warped");
                    }

                }
            }

            WaitForSeconds rate = new WaitForSeconds(1 / spawnRate);
            yield return rate;
        }

    }




    private IEnemy CreateEnemy()
    {
        IEnemy enemyToBeReturned = null;
        switch (spawnMethod)
        {
            case SpawnMethod.RoundRobin:
                enemyToBeReturned = SpawnEnemyByRoundRobin();
                break;
            case SpawnMethod.Random:
                enemyToBeReturned = SpawnEnemyByRandom();
                break;
        }
        return enemyToBeReturned;
    }

    private IEnemy SpawnEnemyByRoundRobin()
    {
        if(enemyPrefabs.Count == 0)
        {
            Debug.LogError("No Enemy Prefabs Found");
            return null;
        }
        if(enemyPrefabs.Count == 1)
        {
            Instantiate(enemyPrefabs[0]).TryGetComponent(out Skeleton skeleton);
            skeleton.SetPool(EnemyObjectPool);
            return skeleton;
        }
        var enemyIndex = (currentEnemyIndex++) % enemyPrefabs.Count;
        Instantiate(enemyPrefabs[enemyIndex]).TryGetComponent(out Skeleton enemy);
        enemy.SetPool(EnemyObjectPool);

        return enemy;
    }

    private IEnemy SpawnEnemyByRandom()
    {
        var enemyIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        Instantiate(enemyPrefabs[enemyIndex]).TryGetComponent(out Skeleton enemy);
        enemy.SetPool(EnemyObjectPool);

        return enemy;
    }



    #endregion


    #region  Pooling Methods
    private void OnTakeEnemyFromPool(IEnemy enemy)
    {
        enemy.GameObject.SetActive(true);
    }

    private void OnReturnEnemyFromPool(IEnemy enemy)
    {
        enemy.GameObject.SetActive(false);
    }

    private void OnDestroyEnemy(IEnemy enemy)
    {
        Destroy(enemy.GameObject);
    }

    #endregion

}





public enum SpawnMethod
{
    RoundRobin,
    Random
}