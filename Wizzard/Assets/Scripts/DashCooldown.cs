using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCooldown : MonoBehaviour
{
    public Image imageCooldown;

    private bool isCooldown = false;
    // remaining cooldown
    private float cooldownTimer = 0;
    //original cooldown
    private float cooldownTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        imageCooldown.fillAmount = 0;
    }

    private void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    // Update is called once per frame
    private void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0;
        }
        else
        {
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void StartDashCooldown(float cdTime)
    {
        isCooldown = true;
        cooldownTimer = cdTime;
        cooldownTime = cdTime;
    }
}
