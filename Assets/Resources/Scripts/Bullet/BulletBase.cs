using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletBase : MonoBehaviour
{
    public GameObject shotEffectPrefab;
    [HideInInspector] public Rigidbody2D rb;

    public float power = 10.0f;
    public float speed = 400.0f;

    public Vector2 shootDirection;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootDirection = Vector2.right;
    }
    void Update()
    {
        BulletDestroy();
    }
    protected virtual void FixedUpdate()
    {
        Move_2D();
    }
    void BulletDestroy()
    {
        Destroy(gameObject, 3.0f);
    }
    protected void Move_2D()
    {
        Vector2 pos = rb.transform.position;
        rb.MovePosition(pos + shootDirection * speed * Time.deltaTime);
    }
}
