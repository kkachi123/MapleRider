using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform shootPoint;
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public GameObject bress;

    [Header("Animation")]
    [SerializeField] GameObject kino;
    Animator animator;

    [Header("Player Status")]
    
    bool shootBullet = false;
    PlayerControl player;
    
    private void Start()
    {
        player = GetComponent<PlayerControl>();
        animator = kino.GetComponent<Animator>();
    }
    void Update()
    {
        if(player.playerHp > 0)
        {
            BulletCool();
            Active();
            if (animator.GetBool("IsAttack"))
            {
                player.playerMp -= player.decreaseMp * Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("IsAttack", false);
            bress.SetActive(false);
        }
    }
    void BulletCool()
    {
        if (shootBullet)
        {
            player.coolTime -= Time.deltaTime;
            if (player.coolTime <= 0.0f)
            {
                player.coolTime = player.maxCoolTime;
                shootBullet = false;
            }
        }
    }
    void Active()
    {
        if (Input.GetMouseButtonDown(0) && !shootBullet)
        {
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.Attack);
            Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            shootBullet = true;
        }
        else if (Input.GetMouseButtonDown(1) && player.playerMp > 0)
        {
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.Attack);
            animator.SetBool("IsAttack", true);
            bress.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1) || player.playerMp <= 0)
        {
            animator.SetBool("IsAttack", false);
            bress.SetActive(false);
        }
    }
}
