using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject minion;
   [SerializeField] private float interval=10f;
   [SerializeField] private int numberOfCreatures = 2;
    private bool coroutineStarted = false;


   void Update()
   {
      if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !coroutineStarted)
      {
         StartCoroutine(Spawn(interval, minion, numberOfCreatures));
            numberOfCreatures = numberOfCreatures * 2;
            coroutineStarted = true;
      }
  
   }
//int currentNumbersOfCreatures = GameObject.FindGameObjectsWithTag("Enemy").Length;
   private IEnumerator Spawn(float interval, GameObject enemy, int creaturesNumber)
   {
       yield return new WaitForSeconds(interval);
           while ( creaturesNumber>0)
           {
              int value = Random.Range(1, 2);
               
                   
               if(value==1)
               {GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0),
                   Quaternion.identity);
                  creaturesNumber = creaturesNumber - 1;
               }
               else
               {
                  
               }
           }
        coroutineStarted = false;


    }


   
   
}
