using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collidable : MonoBehaviour
{
    public Collider2D Collider;
    private Collider2D[] hits = new Collider2D[5];
    public ContactFilter2D filter;

    protected virtual void Update()
    {
        // Collision engine
        Collider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {

    }
}
