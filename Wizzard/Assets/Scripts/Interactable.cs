using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : Collidable
{
    public GameObject hoverText;
    public SpriteRenderer box;
    private bool isFull = true;
    public Sprite emptyBox;
    
    

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if(hoverText)
                hoverText.SetActive(true);
            
            if (Input.GetKey(KeyCode.E)&&isFull)
            {
                box.sprite = emptyBox;
                isFull = false;
                
                Destroy(hoverText);
                
            }
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            Debug.Log("D");
        }
    }
    
}
