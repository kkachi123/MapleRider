using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    private Rigidbody2D rb;
    float maxSpeed = 200.0f;
    public float yCosPlus = 1.0f;
    public float ySpeed = 3.0f;
    public float hp = 10.0f;

    public GameObject shotEffectPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.score += 10;
        }
    }
    void FixedUpdate()
    {
        if(!GameManager.Instance.bGameOver)
            Move_2D();
    }
    float yCos = 0.0f;
    void Move_2D()
    {
        Vector2 pos = rb.transform.position;
        pos = new Vector2(
            pos.x + (-1 * maxSpeed * Time.deltaTime),
            pos.y + Mathf.Sin(yCos) * ySpeed
            );
        yCos += yCosPlus * Time.deltaTime;
        rb.MovePosition(pos);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EndLine" || collision.tag == "Player")
        {
            if(collision.tag == "Player")
                Instantiate(shotEffectPrefab, transform.position, transform.rotation);  
            Destroy(gameObject);
        }
    }
}
