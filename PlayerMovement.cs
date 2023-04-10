using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] Camera mainCam;
   
    

    Rigidbody2D rigbody2D;
    Animator charachterAnimator;
   CapsuleCollider2D capCollider;
    BoxCollider2D feetCollider;

    public Transform gun;
    public GameObject bulletGun;
    float playerGrav = 2.5f;
    bool isAlive = true;


    Vector2 moveInput;


    void Start()
    {
        charachterAnimator = GetComponent<Animator>();
        rigbody2D = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        playerGrav = rigbody2D.gravityScale;
        feetCollider = GetComponent<BoxCollider2D>();
       
    }

    
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FlipStripe();
        ClimbLadder();
        Die();
        
    }

   void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigbody2D.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rigbody2D.velocity.y);
        rigbody2D.velocity = playerVelocity;
    }

    void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            charachterAnimator.SetBool("Climbing", false);
            rigbody2D.gravityScale = playerGrav;
            return;
        }
        Vector2 climbVelocity = new Vector2(rigbody2D.velocity.x, moveInput.y * climbSpeed);
        rigbody2D.velocity = climbVelocity;
        rigbody2D.gravityScale = 0f;

        bool playerStoppedOnLadder = Mathf.Abs(rigbody2D.velocity.y) > Mathf.Epsilon;
        charachterAnimator.SetBool("Climbing", playerStoppedOnLadder);
    }
    void FlipStripe()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigbody2D.velocity.x) > Mathf.Epsilon;
        charachterAnimator.SetBool("Running", playerHasHorizontalSpeed);
        if (playerHasHorizontalSpeed)
        {
            
            transform.localScale = new Vector2(Mathf.Sign(rigbody2D.velocity.x), 1f);
        }
        
    }

    void Die()
    {
        if (rigbody2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            charachterAnimator.SetTrigger("Death");
            rigbody2D.velocity = deathKick;
            StartCoroutine(FindObjectOfType<GameSession>().processPlayerDeath());
           
        }

    }

    

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        Instantiate(bulletGun, gun.position, transform.rotation);
    }

    
}
