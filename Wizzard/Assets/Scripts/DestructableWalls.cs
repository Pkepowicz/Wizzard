using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableWalls : Destructable
{
    public GameObject fog;
    
    private void DestroyThis()
    {
        
        Destroy(gameObject);
        Destroy(fog);
    }
}
