using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject enemySkeleton;
    public float spawnTime = 10f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player2D_Manager>().gameObject;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (player.GetComponent<Player2D_StatsHolder>().Health <= 0)
            return;

        Vector3 ranPos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
        Instantiate(enemySkeleton, ranPos, transform.rotation);  
    }
}
