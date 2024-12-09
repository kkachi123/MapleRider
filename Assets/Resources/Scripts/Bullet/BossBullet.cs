using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BulletBase
{
    public Vector2 spreadDirection;
    void Start()
    {
        shootDirection = spreadDirection;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            collision.GetComponent<PlayerControl>().playerHp -= power;
            collision.GetComponent<PlayerControl>().StartCoroutine(collision.GetComponent<PlayerControl>().PlayerHit());
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
