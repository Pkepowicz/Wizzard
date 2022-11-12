using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starff : MonoBehaviour
{
    public EnemyAI enemyAI;
    public Vector3 originalSize;
    private void Start()
    {
        originalSize = transform.localScale;
    }

    private void Update()
    {
        if (enemyAI.dir.x > 0)
            transform.localScale = originalSize;
        else if (enemyAI.dir.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
    }
}
