using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float speed = 2;
    public bool moveLeft;

    void Update()
    {
        if (moveLeft)
        {
            transform.position += Time.deltaTime * Vector3.left * speed;
            if (transform.localPosition.x < -10)
            {
                moveLeft = false;
            }
        }
        else
        {
            transform.position += Time.deltaTime * Vector3.right * speed;
            if (transform.localPosition.x > 8)
            {
                moveLeft = true;
            }
        }
    }
}
