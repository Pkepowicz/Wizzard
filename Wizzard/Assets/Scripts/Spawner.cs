using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] private GameObject minion;
   [SerializeField] private int numberOfCreatures = 2;
    int wave=1;

   

   void Update()
   {
      
      
       int currentNumbersOfCreatures = GameObject.FindGameObjectsWithTag("Enemy").Length;
       Debug.Log(currentNumbersOfCreatures);
       
       if (currentNumbersOfCreatures == 0)
       {   
           numberOfCreatures = 2;
           wave += 1;
       }
       
       
      if (numberOfCreatures > 0)
      {   
          numberOfCreatures -= 1;
          
          
          Spawn(minion);
      }
   }

   public void Spawn( GameObject enemy)
   {
      
      
      
         
         GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0), Quaternion.identity);
         
      
      
   }
   
}
