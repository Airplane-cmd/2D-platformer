using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private bool DownRight;
    private bool DownLeft;
    private bool RightLeft;
    private SpriteRenderer sprite;
    private Rigidbody2D RD2D;
    public float RayDistance;
    public Transform DownRightLeg;
    public Transform DownLeftLeg;
    public Transform Head;
    public Transform HeadTarget;
    public Transform RightTarget;
    public Transform LeftTarget;
    public LayerMask Ground;
    public float BasicPositionX;
    public float BasicPositionY;
    //public float LeftFuckingTargetPositionX;
    //public float LeftFuckingTargetPositionY;
    //public float RightFuckingTargetPositionX;
    //public float RightFuckingTargetPositionY;
    //public float HeadFuckingTargetPositionX;
    //public float HeadFuckingTargetPositionY;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>(); 
        RD2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        BasicPositionX = transform.position.x;
        BasicPositionY = transform.position.y;
        DownRight = Physics2D.Raycast(DownRightLeg.position, new Vector2(RightTarget.position.x - BasicPositionX, RightTarget.position.y - BasicPositionY), RayDistance, Ground);
        DownLeft = Physics2D.Raycast(DownLeftLeg.position, new Vector2(LeftTarget.position.x - BasicPositionX, LeftTarget.position.y - BasicPositionY), RayDistance, Ground);
        RightLeft = Physics2D.Raycast(transform.position, new Vector2(HeadTarget.position.x - BasicPositionX, HeadTarget.position.y - BasicPositionY), RayDistance, Ground);
        Debug.DrawRay(DownRightLeg.position, new Vector2(RightTarget.position.x - BasicPositionX, RightTarget.position.y - BasicPositionY) * RayDistance, Color.red);
        Debug.DrawRay(DownLeftLeg.position, new Vector2(LeftTarget.position.x - BasicPositionX, LeftTarget.position.y - BasicPositionY) * RayDistance, Color.red);
        Debug.DrawRay(Head.position, new Vector2(HeadTarget.position.x - BasicPositionX, HeadTarget.position.y - BasicPositionY) * RayDistance, Color.red);
        if(DownRight==true)
            Debug.Log("Ground2");
        if (DownLeft == true)
            Debug.Log("Ground1");
        if (RightLeft == true)
            Debug.Log("Forward");
    }
}
