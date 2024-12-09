using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElitBullet : BulletBase
{
    public GameObject player;

    void Start()
    {
        SettingDirection();
    }
    protected override void FixedUpdate()
    {
        if(player != null)
            Move_2D();
    }
    void SettingDirection()
    {
        if (player != null)
            shootDirection = (player.transform.position - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            collision.GetComponent<PlayerControl>().playerHp -= power;
            collision.GetComponent<PlayerControl>().StartCoroutine(collision.GetComponent<PlayerControl>().PlayerHit());
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
