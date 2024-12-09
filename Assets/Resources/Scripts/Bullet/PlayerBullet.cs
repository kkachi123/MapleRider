using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy2D>().hp -= power;
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
        else if(collision.tag == "Elite")
        {
            Destroy(gameObject);
            collision.GetComponent<EliteProfile>().hp -= power;
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
        else if(collision.tag == "Boss")
        {
            Destroy(gameObject);
            collision.GetComponent<BossProfile>().hp -= power;
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
    }
}
