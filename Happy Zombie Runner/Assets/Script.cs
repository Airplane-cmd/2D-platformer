using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    bool Up = true;
    bool Right = true;
    SpriteRenderer Sprite;
    Rigidbody2D RD2D;
    public float RayDistance;
    public LayerMask Ground;
    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        RD2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit2D Up = Physics2D.Raycast(transform.position, Vector2.up, RayDistance, Ground);
        RaycastHit2D Down = Physics2D.Raycast(transform.position, Vector2.down, RayDistance, Ground);
        RaycastHit2D Left = Physics2D.Raycast(transform.position, Vector2.left, RayDistance, Ground);
        RaycastHit2D Right = Physics2D.Raycast(transform.position, Vector2.right, RayDistance, Ground);
        Debug.DrawRay(transform.position, Vector2.up * RayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.down * RayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * RayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * RayDistance, Color.red);
    }
    void Update()
    {
        
    }
}
