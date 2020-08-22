using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxClouds : MonoBehaviour
{
    public float startPositionX;
    public float endPositionX;
    public float speed;

    //Функция параллакса для облаков, передвигаем их, пока они не будут на конечной позиции
    //После возвращаем их на стартовую позицию

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= endPositionX)
        {
            transform.position = new Vector2(startPositionX, transform.position.y);
        }
    }
}
