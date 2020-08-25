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
    public Transform legsPos;
    public LayerMask groundLayer;
    public float repulsiveForce;
    public float rayDistance;
    public float checkRadius;


    // Awake срабатывает в самом начале. Обычно используется для кэширования. Функция Unity
    private void Awake()
    {
        // берём компоненты, навешенные на сам объект и его потомков
        rb2D = GetComponent<Rigidbody2D>();
        // эти компоненты находятся в Sprite, поэтому пишем GetComponentInChildren<>
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    // FixedUpdate - больше подходит для обновлений с использованием физики. Функция Unity
    private void FixedUpdate()
    {
        StateUpdate();
        animator.SetBool("canMove", false);
        Move();
    }


    // Update - обновляет кадры. Функция Unity
    private void Update()
    {
        UI_Controller();
    }

    // Функция для проверки земли под ногами
    private void StateUpdate()
    {
        //Cоздаем невидимый круг вокруг объекта, который проверяет столкновение с другими объектами слоя groundLayer
        onGrounded = Physics2D.OverlapCircle(legsPos.position, checkRadius, groundLayer); //Один из способов проверить землю под ногами. 
        //onGrounded = true если есть столкновение
    }

    //функция движения с помощью кнопок
    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //простая функция передвижения, подрубаем анимацию, меняем направление, поворачиваем спрайт в нужное направление.
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
            //Гасим ускорение у rigidbody2D, чтобы не было сопротивлений и багов.
            rb2D.velocity = Vector3.zero;
            rb2D.AddForce(Vector2.up * forceOfJump, ForceMode2D.Force); //AddForce - функция rigidbody2D
            //AddForce(направление движения силы, модификация силы (в 2D есть Force и Impulse)
        }
    }


    //Функция контроля графики. Можешь не вникать, будем под моим контролем
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

    //Функция получения дамага от мобов. ОНА ПУБЛИЧНАЯ!!!
    //В последствии надо будет изменить. Пока получает только один аргумент
    public void GetDamage(float damageFromEnemy)
    {
        StartCoroutine(changeAnimation()); //Это куратина (Объясню в дс)
        health = health - (damageFromEnemy / 100f); // Здоровице отнимается, дамаг врагов делится на сто, по сколько здоровье принято у нас за 1
        rb2D.velocity = Vector3.zero; // Отталкиваем игрока в противопложную сторону его направления.
        rb2D.AddForce(new Vector2(1f, -0.5f * -direction) * direction * repulsiveForce, ForceMode2D.Impulse);
    }

    IEnumerator changeAnimation() // Это куратина, можешь пока не вникать (Но это крутая вещь)
    {
        animator.SetBool("getDamage", true);
        yield return new WaitForSeconds(.3f);
        animator.SetBool("getDamage", false);
    }
}
