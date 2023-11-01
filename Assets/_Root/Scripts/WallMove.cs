using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float left = -10;
    public float right = 8;
    public float speed = 2;
    public bool moveLeft;

    void Update()
    {
        if (moveLeft)
        {
            transform.position += Time.deltaTime * Vector3.left * speed;
            if (transform.localPosition.x < left)
            {
                moveLeft = false;
            }
        }
        else
        {
            transform.position += Time.deltaTime * Vector3.right * speed;
            if (transform.localPosition.x > right)
            {
                moveLeft = true;
            }
        }
    }
}
