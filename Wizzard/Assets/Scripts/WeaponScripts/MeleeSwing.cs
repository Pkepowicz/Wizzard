using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : Wand
{
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetKey(KeyCode.Mouse1)) && (Time.time - lastShot) > reloadTime)
        {
            //Debug.Log("Ready to shoot");
            Shoot();
            lastShot = Time.time;
            SoundManager.PlaySound(AttackSound, projectileSpawnPoint.position);
        }

    }
}
