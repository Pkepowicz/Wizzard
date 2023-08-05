using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Destructable : Collidable
{
    // Start is called before the first frame update
    public Sprite[] statusList;
    public SpriteRenderer spriteRenderer;
    public GameObject gameObject;
    public int MaxHP;
    public float  HP;
    
    
    
    public void TakeDamage(float dmg){
        HP = HP-dmg;
        
        ChangeSprite();
        if(HP <= 0){
            DestroyThis();
        }
    }

    public void ChangeSprite(){
        if (HP <= 0.8 * MaxHP && HP > 0.5 * MaxHP )
        {
            spriteRenderer.sprite = statusList[0];
        }
        else if( HP <= 0.5 * MaxHP && HP > 0.3 * MaxHP)
        {
            spriteRenderer.sprite = statusList[1];
        }
        else if( HP <= 0.3 * MaxHP )
        {
            spriteRenderer.sprite = statusList[2];
        }
    }

    private void DestroyThis()
    {
        
        Destroy(gameObject);
    }
    
    
    
}
