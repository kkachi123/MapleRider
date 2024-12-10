using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRazer : MonoBehaviour
{
    public GameObject shotEffectPrefab;
    public float power = 10.0f;
    float deltaTime = 0.0f;
    private void OnEnable()
    {
        deltaTime = 0.0f;
    }
    private void Update()
    {
        SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.BossRazer);
        deltaTime += Time.deltaTime;
        if (deltaTime >= 1.5f)
        {
            deltaTime = 0.0f;
            this.gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            collision.GetComponent<PlayerControl>().playerHp -= power;
            collision.GetComponent<PlayerControl>().StartCoroutine(collision.GetComponent<PlayerControl>().PlayerHit());
            Instantiate(shotEffectPrefab, transform.position, transform.rotation);
        }
    }
}
