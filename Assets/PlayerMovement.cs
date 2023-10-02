using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movespeed = 5f;
    public float dashspeed = 30f;
    private float dashDuration = 0.15f;
    private float dashCooldown = 1f;
    private bool isDash = false;
    private bool canDash = true;

    public Rigidbody2D player;
    public Animator anim;
    public Collider2D playerCol;
    Vector2 movement;
    public float ActMovespeed;

    // Update is called once per frame
    private void Start()
    {
        ActMovespeed = movespeed;
    }

    void Update()
    {
        if (isDash)
        {
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
        }

    }

    private void FixedUpdate()
    {
        player.MovePosition(player.position + movement.normalized * ActMovespeed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        isDash = true;
        playerCol.enabled = false;
        anim.SetFloat("Dash", 1f);
        ActMovespeed = dashspeed;
        //player.MovePosition(player.position + movement * dashspeed * Time.fixedDeltaTime);
        yield return new WaitForSeconds(dashDuration);
        anim.SetFloat("Dash", 0f);
        ActMovespeed = movespeed;
        playerCol.enabled = true;
        isDash = false;
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
