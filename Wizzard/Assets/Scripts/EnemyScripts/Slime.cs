using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject slimePrefab;
    public List<Transform> offsets;
    public float slimeKnockback = 0.5f;

    private GameObject slime;

    private void OnDestroy()
    {
        Damage dmg = new Damage
        {
            damageAmmount = 0,
            knockBack = slimeKnockback,
            origin = transform.position
        };
        for(int i = 0; i < offsets.Count; i++)
        {
            slime = Instantiate(slimePrefab, offsets[i].position, Quaternion.identity);
            slime.SendMessage("ReceiveDamage", dmg);
        }
    }
}
