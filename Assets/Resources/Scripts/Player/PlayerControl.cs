using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("View")]
    public Image hpBar;
    public GameObject hpObject;
    public Image mpBar;
    public GameObject mpObject;
    public GameObject levelUpEffectPrefab;
    [SerializeField] SpriteRenderer[] playerImages;

    [Header("Player Status")]
    public float playerHp = 100.0f;
    public float playerMp = 100.0f;

    float maxPlayerHp;
    float maxPlayerMp;
    
    public float recoverHp = 5.0f;
    public float recoverMp = 10.0f;

    public float decreaseMp = 100.0f;
    public float coolTime = 0.5f;
    [HideInInspector]public float maxCoolTime;

    public float hpRecoverTime = 3.0f;
    float maxHpRecoverTime;
    float speed = 300.0f;
    // fadeTime;
    float time = 0.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Init();
    }
    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {
            if (playerHp <= 0)
            {
                Death();
            }
            else
            {
                if (GameManager.Instance.score >= 1000 * GameManager.Instance.level)
                    LevelUp();
                Recover();
            }
        }
        MoveRange();
    }
    void FixedUpdate()
    {
        if(playerHp > 0)
            Move_2D();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            playerHp -= 10;
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            StartCoroutine(PlayerHit());
        }
    }
    void FillHpBar()
    {
        hpBar.fillAmount = playerHp / maxPlayerHp;
    }
    void FillMpBar()
    {
        mpBar.fillAmount = playerMp / maxPlayerMp;
    }
    void Init()
    {
        hpObject.SetActive(false);
        mpObject.SetActive(false);
        maxPlayerHp = playerHp;
        maxPlayerMp = playerMp;
        maxCoolTime = coolTime;
        maxHpRecoverTime = hpRecoverTime;
    }
    void Recover()
    {
        if(playerHp < maxPlayerHp)
        {
            hpRecoverTime -= Time.deltaTime;
            if (hpRecoverTime <= 0)
            {
                hpRecoverTime = maxHpRecoverTime;
                hpObject.SetActive(true);
                playerHp += recoverHp;
                FillHpBar();
            }
        }
        else
        {
            hpObject.SetActive(false);
        }

        if (playerMp < maxPlayerMp)
        {
            mpObject.SetActive(true);
            playerMp += recoverMp * Time.deltaTime;
            FillMpBar();
        }
        else
        {
            mpObject.SetActive(false);
        }
    }
    void Move_2D()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 pos = rb.transform.position;
        rb.MovePosition(new Vector2(pos.x + (x * speed * Time.deltaTime), pos.y + (y * speed * Time.deltaTime)));
    }
    void MoveRange()
    {
        if (this.transform.position.x > 547)
            this.transform.position = new Vector3(547, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.x < -547)
            this.transform.position = new Vector3(-547, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.y < -271)
            this.transform.position = new Vector3(this.transform.position.x, -271, this.transform.position.z);
        if (this.transform.position.y > 271)
            this.transform.position = new Vector3(this.transform.position.x, 271, this.transform.position.z);
    }

    void LevelUp()
    {
        GameManager.Instance.level++;
        Instantiate(levelUpEffectPrefab, this.transform.position, this.transform.rotation);
        recoverMp += 5;
        recoverHp += 5;
        hpRecoverTime -= 0.5f;
        if (hpRecoverTime < 0.5f)
            hpRecoverTime = 0.5f;
        maxCoolTime -= 0.1f;
        if (maxCoolTime < 0.1f)
            maxCoolTime = 0.1f;
        coolTime = maxCoolTime;
        playerHp = maxPlayerHp;
        playerMp = maxPlayerMp;
    }
    public IEnumerator PlayerHit()
    {
        int count = 5;
        hpObject.SetActive(true);
        while(count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            FillHpBar();
            for(int i = 0; i < playerImages.Length; i++)
            {
                Color playerAlpha = playerImages[i].color;
                if(playerAlpha.a == 0)
                    playerAlpha.a = 255;
                else
                    playerAlpha.a = 0;
                playerImages[i].color = playerAlpha;
            }
            count--;
        }
        hpObject.SetActive(false);
        for(int i = 0; i < playerImages.Length; i++)
        {
            Color playerAlpha = playerImages[i].color;
            playerAlpha.a = 255;
            playerImages[i].color = playerAlpha;
        }
    }
    void Death()
    {
        for (int i = 0; i < playerImages.Length; i++)
        {
            Color playerAlpha = playerImages[i].color;
            time += Time.deltaTime / 2;
            playerAlpha.a = Mathf.Lerp(1, 0, time);
            playerImages[i].color = playerAlpha;
        }
        if (playerImages[0].color.a <= 0)
            GameManager.Instance.bGameOver = true;
    }

}
