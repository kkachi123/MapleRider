using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    Vector3 startPos;
    float width;

    void Start()
    {
        startPos = transform.position;
        width = GetComponent<BoxCollider2D>().size.x;
    }
    void FixedUpdate()
    {
        if(transform.position.x < startPos.x - width)
        {
            transform.position = startPos;
        }
        transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}
