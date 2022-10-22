using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb; // declaring a variable, it's private so only this script can use it, it's good practice to do that
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    bool CanMove = true;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; // now in Unity, next to our player movement script we can see this value because we used [SerializeField]
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal"); // if it's GetAxis() the character will slide a little bit when the key is released but with GetAxisRaw() the speed drops to 0 instantly
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); // vector 2 is x, y, more used in 2d games


        if (Input.GetButtonDown("Jump")) //GetKeyDown() is used so we cannot just fly off by holding space which we can do with GetKey(). instead of GetKeyDown() i replaced it with GetButtonDown() and instead of "space" it's on "Jump"
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //x, y, z coordinates. vector3() is editing the "transform" part on the right when you select the player. we use 14f because its better practice
        }
        UpdateAnimationState();
        }
    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f) // moving right
        {
            state = MovementState.running;
            sprite.flipX = false;
            anim.SetBool("MovementState 0", true);
        }
        else if (dirX < 0f) // moving left
        {
            state = MovementState.running;
            sprite.flipX = true; // flips the character 180 degrees so he faces left when we're moving left
            anim.SetBool("MovementState 0", true);
        }
        else
        {
            state = MovementState.idle; // after this the idle animation starts playing
            anim.SetBool("MovementState 0", false);
        }

    }
    }
    /*
    private void FixedUpdate()
    {
        if (CanMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                anim.SetBool("IsMoving", success);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }
        }
        */