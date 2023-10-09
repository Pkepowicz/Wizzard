using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : Fighter
{
    public Vector3 velocity;

    public Transform textureManager;
    public Camera mainCam;
    
    // For testing new effects
    public GameObject regen;
    public int score;
    [Header("Dash settings")] 
    private bool canDash = true;
    private bool isDashing = false;
    public float dashingPower = 20f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public float dashImmunityTime = 0.3f;

    public GameObject startDashParticle;
    public GameObject endDashParticle;

    public DashCooldown dashCDManager;
    public GameObject deadScreen;
    public GameObject spritePlayer;
    public TMP_Text timer;
    public float time = 10;

    private bool _isDead = false;
    
      protected override  void Destroy()
      {
          Time.timeScale = 0f;
          deadScreen.SetActive(true);
          spritePlayer.SetActive(false);
      }

    
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        else if (time > 0)
        {
            time -= Time.deltaTime;
            timer.text = (int)(time/60)+":" + (int)(time%60);
        }
        else if(time<=0 && !_isDead)
        {
            Death();
            _isDead = true;
        }
        
    }

    private void FixedUpdate()
    {
        // if the player is already dashing, he can't do anything
        if (isDashing || !canMove)
        {
            return;
        }
        // Getting movement input on each separate axis
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // For testing new effects
        /*if (Input.GetButtonDown("Submit"))
        {
            Instantiate(regen, transform.position, Quaternion.identity, gameObject.transform);
        }*/
        
        // looking at mouse position 
        Vector2 toTarget = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        textureManager.transform.up = toTarget;

        UpdateMotor(new Vector2(x, y).normalized);
    }
    
    private IEnumerator Dash()
    {
        Vector2 targetPosition = rb.velocity.normalized != Vector2.zero ? rb.velocity.normalized : mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        canDash = false;
        isDashing = true;
        HandleParticles(startDashParticle);
        StartCoroutine(StartImmunityPeriod(dashImmunityTime));
        rb.velocity = targetPosition * dashingPower;
        dashCDManager.StartDashCooldown(dashingCooldown);
        SoundManager.PlaySound("Dash", transform.position);
        yield return new WaitForSeconds(dashingTime);
        
        HandleParticles(endDashParticle, gameObject);
        rb.velocity = Vector3.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    protected override void Death() {
        hitPoint = 0;
        isAlive = false;
      
        
        SoundManager.PlaySound(deathSound, transform.position);
        Debug.Log("Player is dead.");
        anim.SetTrigger("death");
        NetworkServices.Statistics.UpdateBestScore((float)ScoreManager.score,
            (() => {
                Debug.Log("UpdateBestScore successful.");
            }),
            (error => {
                Debug.LogError(error.GenerateErrorMessage());
            }));
    }
    private void Awake()
    {
        timer.text = "1:00";
    }
}
