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

    // method for creating particles and deleting them after lifetime ends
    // most particle events should last less than 5 sec
    protected virtual void HandleParticles(GameObject prefab, float lifetime = 5)
    {
        GameObject currentParticle = Instantiate
            (prefab, transform.position, Quaternion.identity, transform);
        currentParticle.GetComponent<ParticleSystem>().Play();
        Destroy(currentParticle, lifetime);
    }
}
