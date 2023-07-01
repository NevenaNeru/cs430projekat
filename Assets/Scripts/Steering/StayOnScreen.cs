using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnScreen : MonoBehaviour
{
    [SerializeField] private float right;
    [SerializeField] private float left;
    [SerializeField] private float top;
    [SerializeField] private float bottom;

    void Update()
    {
        if (transform.position.x > right)
        {
            transform.position = new Vector2(left, transform.position.y);

        }
        if (transform.position.x < left)
        {
            transform.position = new Vector2(right, transform.position.y);

        }
        if (transform.position.y > top)
        {
            transform.position = new Vector2(transform.position.x, bottom);

        }
        if (transform.position.y < bottom)
        {
            transform.position = new Vector2(transform.position.x, top);

        }
    }
}
