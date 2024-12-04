using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1000f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x - moveSpeed * Time.deltaTime, pos.y);
        rb.MovePosition(pos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndLine")
        {
            Destroy(gameObject);
        }
    }
}
