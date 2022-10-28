using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // WORK IN PROGRESS
    // for now it spawns enemies in circle around player on set intervals
    // TODO: Change it to something more fitting

    public Transform target;
    public GameObject meleeEnemy;
    public float radius;
    public float meleeEnemySpawnTime;
    private float meleeEnemyLastSpawn;

    private void Update()
    {
        spawn();
    }

    private void spawn()
    {
        if (Time.time - meleeEnemySpawnTime > meleeEnemyLastSpawn)
        {
            Vector3 pos = Circle(target.position, radius);
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, target.position - pos);
            Instantiate(meleeEnemy, pos, rot);
            meleeEnemyLastSpawn = Time.time;
        }
    }

    Vector3 Circle(Vector3 center, float rad)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + rad * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + rad * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
