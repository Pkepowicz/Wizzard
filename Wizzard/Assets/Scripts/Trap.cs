using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    
    public Sprite[] daggers;
    private SpriteRenderer spriteRenderer; 
    private int currentSpriteIndex = 0; 

    public float changeInterval = 1.0f;
    private float timer = 0.0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (daggers.Length > 0)
        {
            spriteRenderer.sprite = daggers[0]; 
        }
    }

    void Update()
    {
        timer += Time.deltaTime; // Inkrementacja timera

        if (timer >= 1)
        {
            timer = 0.0f; // Zresetowanie timera

            // Przesunięcie indeksu sprite'a
            currentSpriteIndex = (currentSpriteIndex + 1) %  daggers.Length;

            // Zmiana sprite'a na nowy
            spriteRenderer.sprite = daggers[currentSpriteIndex];
        }
    }
}
