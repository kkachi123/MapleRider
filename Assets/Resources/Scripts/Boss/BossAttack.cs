using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour, IActive
{
    public GameObject bulletPrefab;
    public GameObject player;
    public Transform shootPoint;

    public int bulletCount = 5;
    int maxBulletCount;

    public float shootTerm = 0.3f;
    public float power = 10.0f;

    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        maxBulletCount = bulletCount;
    }
    public BossState Active()
    {
        shootTerm -= Time.deltaTime;
        if (shootTerm <= 0.0f)
        {
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.Attack);

            Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bulletPrefab.GetComponent<BossBullet>().player = player;
            bulletPrefab.GetComponent<BossBullet>().power = power;

            shootTerm = 0.3f;
            bulletCount--;

            if (bulletCount <= 0)
            {
                bulletCount = maxBulletCount;
                animator.SetFloat("Speed", 0.0f);
                animator.SetBool("IsAttack", false);
                return BossState.Idle;
            }
        }
        return BossState.Attack;
    }
}
