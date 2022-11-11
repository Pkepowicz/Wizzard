using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour

{
   public List<Enemy> enemies = new List<Enemy>();
   [SerializeField] private float interval=10f;
   [SerializeField] private int numberOfCreatures = 2;
    private bool coroutineStarted = false;


   void Update()
   {
      if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !coroutineStarted)
      {
         StartCoroutine(Spawn(interval, enemies, numberOfCreatures));
            numberOfCreatures = numberOfCreatures * 2;
            coroutineStarted = true;
      }
  
   }

   private IEnumerator Spawn(float interval, List<Enemy> enemiesPossibleToSpawn, int creaturesNumber)
   {
       yield return new WaitForSeconds(interval);
           while ( creaturesNumber>0)
           {
              int value = Random.Range(0, enemiesPossibleToSpawn.Count);
               
                   
               if(creaturesNumber - enemiesPossibleToSpawn[value].cost>=0)
               {
                  GameObject newEnemy = Instantiate(enemiesPossibleToSpawn[value].enemyPrefab, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0),
                   Quaternion.identity);
                  creaturesNumber = creaturesNumber - enemiesPossibleToSpawn[value].cost;
                  
               }
              
           }
        coroutineStarted = false;


    }


   
   
}
[System.Serializable]
public class Enemy
{
   public GameObject enemyPrefab;
   public int cost;
}