using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bressPrefab;
    public GameObject player;
    [SerializeField] GameObject kino;
    Animator anim;
    public float decreaseMp = 100.0f;
    public float coolTime = 0.5f;
    float maxCoolTime;
    bool shootBullet = false;
    
    private void Start()
    {
        anim = kino.GetComponent<Animator>();
        maxCoolTime = coolTime;
    }
    void Update()
    {
        if(shootBullet)
        {
            coolTime -= Time.deltaTime;
            if(coolTime <= 0.0f)
            {
                coolTime = maxCoolTime;
                shootBullet = false;
            }
        }
        if(Input.GetMouseButtonDown(0) && !shootBullet)
        {
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.Attack);
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            shootBullet = true;
        }
        else if (Input.GetMouseButtonDown(1) && player.GetComponent<Player2D>().playerMp > 0)
        {
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.Attack);
            anim.SetBool("IsAttack", true);
            bressPrefab.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1) || player.GetComponent<Player2D>().playerMp <= 0)
        { 
            anim.SetBool("IsAttack", false);
            bressPrefab.SetActive(false);
        }
        
        if(anim.GetBool("IsAttack"))
        {
            player.GetComponent<Player2D>().playerMp -= decreaseMp * Time.deltaTime;
        }
    }
}
