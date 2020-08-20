using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform[] moveMarks;
    public float speed;
    public float damage;

    private Player player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    private void FixedUpdate()
    {
        PatrolBetweenTwoMarks();
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetDamage(damage);
    }
}
