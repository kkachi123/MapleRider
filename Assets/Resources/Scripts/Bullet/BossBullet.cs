using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : BulletBase
{
    public GameObject player;
    Vector2 shootDirection;

    void Start()
    {
        SettingDirection();
    }
    void Update()
    {
       BulletDestroy();
    }
    void FixedUpdate()
    {
        if(player != null)
            Move_2D();
    }
    void SettingDirection()
    {
        // 플레이어를 향해 총알을 발사
        if (player != null)
            shootDirection = (player.transform.position - transform.position).normalized;
    }
    void BulletDestroy()
    {
        Destroy(gameObject, 3.0f);
    }
    void Move_2D()
    {
        rb.MovePosition(rb.position + shootDirection * maxSpeed * Time.deltaTime);
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
            collision.GetComponent<Player2D>().playerHp -= power;
            collision.GetComponent<Player2D>().StartCoroutine(collision.GetComponent<Player2D>().PlayerHit());
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
