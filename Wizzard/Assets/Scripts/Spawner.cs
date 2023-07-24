using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public List<Enemy> enemies = new List<Enemy>();
   [SerializeField] private float interval = 3f;
   [SerializeField] private int numberOfCreatures;
   public int waveNumber = 1;
   private bool coroutineStarted = false;

    
   void Update()
   {
      if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !coroutineStarted)
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
        while (creaturesNumber>0)
        {
            int value = Random.Range(0, enemiesPossibleToSpawn.Count);
              
            if(creaturesNumber - enemiesPossibleToSpawn[value].cost>=0)
            {
                  GameObject newEnemy = Instantiate(enemiesPossibleToSpawn[value].enemyPrefab, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0),
                  Quaternion.identity);
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