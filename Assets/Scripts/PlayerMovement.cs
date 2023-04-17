using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // yukarı aşapı gitme yok
    // zıplayınca uçuyor
    public float movePower = 10f;
    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    private void Update()
    {
        Restart();
        if (alive)
        {
            Hurt();
            Die();
            Attack();
            Run();
            MoveUp();
            MoveDown();
        }
    }

    void Run()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isRun", false);

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
            moveVelocity = Vector3.left;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
            moveVelocity = Vector3.right;

            transform.localScale = new Vector3(direction, 1, 1);
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void MoveUp()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isClimb", true);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
            moveVelocity = Vector3.up;
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isClimb", false);
        }
    }

    void MoveDown()
    {
        Vector3 moveVelocity = Vector3.zero;
        anim.SetBool("isClimb", true);

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            if (!anim.GetBool("isJump"))
                anim.SetBool("isRun", true);
            moveVelocity = Vector3.down;
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isClimb", false);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("attack");
        }
    }

    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }

    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }
}