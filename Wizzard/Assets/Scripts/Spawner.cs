using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private float interval = 3f;
    private int numberOfCreatures;
    private int waveNumber = 1;
    private bool coroutineStarted = false;


    void Update()
    {
        if (!coroutineStarted && GameObject.FindGameObjectsWithTag("Enemy").Length <= 2)
        {
            // logarythmic enemy number scaling, first wave 6 enemies, 2nd 18, 5th 33, 10th 46 which is softcap
            numberOfCreatures = (int)Mathf.Ceil(40 * Mathf.Log10(((waveNumber + 1) / 2))) + 15;
            StartCoroutine(Spawn(interval, enemies, numberOfCreatures));
            coroutineStarted = true;

        }

    }

    private IEnumerator Spawn(float interval, List<Enemy> enemiesPossibleToSpawn, int creaturesNumber)
    {
        yield return new WaitForSeconds(interval);
        while (creaturesNumber > 0)
        {
            int value = Random.Range(0, enemiesPossibleToSpawn.Count);

            if (creaturesNumber - enemiesPossibleToSpawn[value].cost >= 0)
            {
                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                GameObject newEnemy = Instantiate(enemiesPossibleToSpawn[value].enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].position
                    + offset, Quaternion.identity);
                creaturesNumber = creaturesNumber - enemiesPossibleToSpawn[value].cost;
            }

        }
        waveNumber += 1;
        coroutineStarted = false;

    }
}
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}