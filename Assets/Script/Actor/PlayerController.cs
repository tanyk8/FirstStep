using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 1f;

    public float collisionOffset = 0.5f;

    public ContactFilter2D movementFilter;

    Vector2 movementInput;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        movementFilter.SetLayerMask(LayerMask.GetMask("Collision"));
        movementFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying|| MenuManager.GetInstance().menuIsOpened)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        if (movementInput != Vector2.zero)
        {
            bool success=tryMove(movementInput);

            if (!success)
            {
                success = tryMove(new Vector2(movementInput.x,0));
                if (!success)
                {
                    success = tryMove(new Vector2(0,movementInput.y));
                }
            }

            animator.SetBool("isMoving",success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool tryMove(Vector2 direction)
    {
        int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollision,
                movespeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * movespeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

        if (movementInput != Vector2.zero && !DialogueManager.GetInstance().dialogueIsPlaying&&!MenuManager.GetInstance().menuIsOpened)
        {
            animator.SetFloat("Xinput", movementInput.x);
            animator.SetFloat("Yinput", movementInput.y);
        }
        

    }

}
