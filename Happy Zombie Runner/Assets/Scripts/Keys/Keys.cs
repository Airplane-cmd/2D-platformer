using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public string whatIsTheKey;
    private Player player;
    private SpriteRenderer sprite;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetKey(whatIsTheKey, sprite);
            Destroy(gameObject);
        }
    }
}
