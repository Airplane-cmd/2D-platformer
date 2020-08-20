using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float forceOfJump = 6f;
    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rb2D;
    private float direction = -1;
    private float health = 1f;
    private bool onGrounded = false;

    public Transform healthBar;
    public Transform legsRayPos;
    public LayerMask groundLayer;
    public float repulsiveForce;
    public float rayDistance;
    public float checkRadius;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        StateUpdate();
        animator.SetBool("canMove", false);
        Move();
    }

    private void Update()
    {
        UI_Controller();
    }

    private void StateUpdate()
    {
        onGrounded = Physics2D.OverlapCircle(legsRayPos.position, checkRadius, groundLayer);
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            animator.SetBool("canMove", true);
            direction = -1;
            sprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            animator.SetBool("canMove", true);
            direction = 1;
            sprite.flipX = true;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && onGrounded)
        {
            rb2D.velocity = Vector3.zero;
            rb2D.AddForce(Vector2.up * forceOfJump, ForceMode2D.Force);
        }
    }

    private void UI_Controller()
    {
        if (health < 0)
        {
            health = 0;
        }
        healthBar.localScale = new Vector2(health, healthBar.localScale.y);
        if (onGrounded)
        {
            animator.SetBool("grounded", false);
        }
        else
        {
            animator.SetBool("grounded", true);
        }
    }

    public void GetDamage(float damageFromEnemy)
    {
        StartCoroutine(changeAnimation());
        print(damageFromEnemy / 100f);
        health = health - (damageFromEnemy / 100f);
        rb2D.velocity = Vector3.zero;
        rb2D.AddForce(new Vector2(1f, -0.5f * -direction) * direction * repulsiveForce, ForceMode2D.Impulse);
    }

    IEnumerator changeAnimation()
    {
        animator.SetBool("getDamage", true);
        yield return new WaitForSeconds(.3f);
        animator.SetBool("getDamage", false);
    }
}
