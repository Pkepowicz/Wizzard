using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : Collidable
{
    public GameObject hoverText;
    public SpriteRenderer box;
    public TMPro.TextContainer text;
    private bool isFull = true;
    public Sprite emptyBox;
    //Just for debug 
    public int score = 0;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            hoverText.SetActive(true);
            if (Input.GetKey(KeyCode.E)&&isFull)
            {
                box.sprite = emptyBox;
                isFull = false;
                score += 10;
                Destroy(hoverText);
            }
            
        }
        
    }
    
    
}
