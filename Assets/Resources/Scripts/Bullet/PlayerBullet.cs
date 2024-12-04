using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    void Update()
    {
        Destroy(gameObject, 3.0f);
    }
    void FixedUpdate()
    {
        Move_2D();
    }
    void Move_2D()
    {
        Vector2 pos = rb.transform.position;
        rb.MovePosition(new Vector2(pos.x + (maxSpeed * Time.deltaTime), pos.y));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy2D>().hp -= power;
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
        else if(collision.tag == "Boss")
        {
            Destroy(gameObject);
            collision.GetComponent<BossStateControl>().hp -= power;
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
    }
}
