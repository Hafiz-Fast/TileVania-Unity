using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float movespeed = 5f;
    [SerializeField] float jumpspeed = 5f;
    [SerializeField] float climbspeed = 5f;
    [SerializeField] Vector2 deathkick = new Vector2(0f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip jump;

    float currentgravity = 8f;
    Vector2 moveinput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D mycd;
    BoxCollider2D myfeet;
    static int lives = 1;

    bool isAlive = true;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mycd= GetComponent<CapsuleCollider2D>();
        myfeet=GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        run();
        flipsprite();
        Climbup();
        Die();
        Exit();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveinput =value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (myfeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (value.isPressed)
            {
                AudioSource.PlayClipAtPoint(jump, Camera.main.transform.position);
                rb.velocity = new Vector2(0f, jumpspeed);
            }
        }
    }
    void run()
    {
        Vector2 temp=new Vector2(moveinput.x * movespeed,rb.velocity.y);
        rb.velocity = temp;

        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isrunning", isMoving);
    }

    void flipsprite()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Climbup()
    {
        if (myfeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 temp = new Vector2(rb.velocity.x, moveinput.y * climbspeed);
            rb.velocity = temp;
            rb.gravityScale = 0;
            bool isverticalMoving = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            animator.SetBool("isclimbing", isverticalMoving);
        }
        else
        {
            animator.SetBool("isclimbing", false);
            rb.gravityScale = currentgravity;
        }
    }

    void Die()
    {
        if (mycd.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            animator.SetTrigger("Dying");
            rb.velocity = deathkick;
            AudioSource.PlayClipAtPoint(death, Camera.main.transform.position);
            Invoke("LoadScene", 0.5f);
            lives++;
            if (lives == 4)
            {
                Invoke("loadfirst", 0.5f);
                lives = 1;
            }
        }
    }

    void LoadScene()
    {
        int currentscene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentscene);
    }

    void loadfirst()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
