using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject minion;
   [SerializeField] private float interval=10f;
   [SerializeField] private int numberOfCreatures = 2;


   void Update()
   {
      if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
      {
         StartCoroutine(Spawn(interval, minion,numberOfCreatures));
         numberOfCreatures = numberOfCreatures * 2;
      }
  
   }
//int currentNumbersOfCreatures = GameObject.FindGameObjectsWithTag("Enemy").Length;
   private IEnumerator Spawn(float interval, GameObject enemy, int creaturesNumber)
   {
       yield return new WaitForSeconds(interval);
           for (int i = 0; i <= creaturesNumber; i++)
           {
               GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0),
                   Quaternion.identity);
           }

           
       
   }


   
   
}
