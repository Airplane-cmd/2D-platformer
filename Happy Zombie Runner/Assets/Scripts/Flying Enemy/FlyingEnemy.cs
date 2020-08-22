using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    //Скрипт врага. Обычный моб, который ходит из одной точки в другую. Если игрок до него докоснется, получит урон
    public Transform[] moveMarks;
    public float speed;
    public float damage;

    //Заметь какого типа переменная ниже
    private Player player;
    private SpriteRenderer sprite;

    // Awake срабатывает в самом начале. Обычно используется для кэширования. Функция Unity
    private void Awake()
    {
        // берём компонент, навешенный на потомка Sprite
        sprite = GetComponentInChildren<SpriteRenderer>();
        // FindObjectOfType - находит объект какого-то класса на сцене. У нас класс Player
        player = FindObjectOfType<Player>();
    }

    // FixedUpdate - больше подходит для обновлений с использованием физики. Функция Unity
    private void FixedUpdate()
    {
        PatrolBetweenTwoMarks();
    }

    //Функция патруля между двумя точками.
    //Ну тут, думаю, просто. Триггером того, что объект дошёл до точки является sprite.flipX
    //sprite.flipX поворачивает спрайт в зависимости от значения true, false
    void PatrolBetweenTwoMarks()
    {
        if (sprite.flipX == false && transform.position.x != moveMarks[0].transform.position.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveMarks[0].transform.position, speed * Time.deltaTime);
        }
        else if (transform.position.x == moveMarks[0].transform.position.x && sprite.flipX == false)
        {
            sprite.flipX = true;
        }
        else if (sprite.flipX == true && transform.position.x != moveMarks[1].transform.position.x)
        {

            transform.position = Vector2.MoveTowards(transform.position, moveMarks[1].transform.position, speed * Time.deltaTime);
        }
        else if (transform.position.x == moveMarks[1].transform.position.x && sprite.flipX == true)
        {
            sprite.flipX = false;
        }
    }

    //OnTriggerEnter2D - проверяет столкнулся ли объект с какой-нибудь коллизией
    //Trigger означает, что сам объект по сути может проходить через коллизию, но так же может и реагировать на неё
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") player.GetDamage(damage); //Проверка тега коллизи, при true условии бьём игрока
    }
}
