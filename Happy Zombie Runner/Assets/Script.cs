using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public GameObject Spider;
    bool Up=true;
    bool Right=true;
    void Awake()
    {
        Instantiate(Spider,Spider.transform.position,Spider.transform.rotation);
    }
    void Update()
    {
        Up = GetComponent<SpriteRenderer>();
        Up.Flip.X=
    }
}
