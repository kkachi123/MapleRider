using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    protected Rigidbody2D rb;
    float yCosPlus = 1.0f;
    float ySpeed = 3.0f;
    float yCos = 0.0f;

    public float speed = 200.0f;
    public float hp = 10.0f;
    public int addScore = 10;

    public GameObject shotEffectPrefab;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.score += addScore;
        }
    }
    protected void FixedUpdate()
    {
        if(!GameManager.Instance.bGameOver)
            Move_2D();
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EndLine" || collision.tag == "Player")
        {
            if(collision.tag == "Player")
                Instantiate(shotEffectPrefab, transform.position, transform.rotation);  
            Destroy(gameObject);
        }
    }
    protected virtual void Move_2D()
    {
        Vector2 pos = rb.transform.position;
        pos = new Vector2(
            pos.x + (-1 * speed * Time.deltaTime),
            pos.y + Mathf.Sin(yCos) * ySpeed
            );
        yCos += yCosPlus * Time.deltaTime;
        rb.MovePosition(pos);
    }
}
