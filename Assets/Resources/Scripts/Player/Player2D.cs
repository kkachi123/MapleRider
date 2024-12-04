using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2D : MonoBehaviour
{
    private Rigidbody2D rb;
    float maxSpeed = 300.0f;

    public Image imgHPBar;
    public GameObject hpObject;
    public GameObject mpObject;
    public Image imgMPBar;

    public GameObject levelUpEffectPrefab;
    public float maxPlayerHp = 100.0f;
    [HideInInspector] public float playerHp;
    public float maxPlayerMp = 100.0f;
    [HideInInspector] public float playerMp;
    public float recoverHp = 5.0f;
    public float recoverMp = 10.0f;

    [SerializeField] SpriteRenderer[] playerImages;

    PlayerAttack playerAttack;
    void Awake()
    {
        Application.runInBackground = true;
        rb = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        hpObject.SetActive(false);
        mpObject.SetActive(false);
        playerHp = maxPlayerHp;
        playerMp = maxPlayerMp;
    }
    void Update()
    {
       ShowHpBar(playerHp);
        if(GameManager.Instance.score >= 1000 * GameManager.Instance.level)
        {
            GameManager.Instance.level++;
            Instantiate(levelUpEffectPrefab, this.transform.position, this.transform.rotation);
            recoverMp += 5;
            playerAttack.coolTime -= 0.1f;
            if(playerAttack.coolTime < 0.1f)
                playerAttack.coolTime = 0.1f;
            playerHp = maxPlayerHp;
            playerMp = maxPlayerMp;
        }
        if(playerHp <= 0)
         {
              GameManager.Instance.bGameOver = true;
         }
        if(playerMp < maxPlayerMp)
        {
            mpObject.SetActive(true);
            playerMp += recoverMp * Time.deltaTime;
            ShowMpBar(playerMp);
        }
        else
        {
            mpObject.SetActive(false);
        }
        MoveRange();
    }
    void FixedUpdate()
    {
        if(!GameManager.Instance.bGameOver)
            Move_2D();
    }
    void Move_2D()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 pos = rb.transform.position;
        rb.MovePosition(new Vector2(pos.x + (x * maxSpeed * Time.deltaTime), pos.y + (y * maxSpeed * Time.deltaTime)));
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            GameManager.Instance.score += 100;
            Destroy(collision.gameObject);
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.GetCoin);
        }
        else if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        { 
            playerHp -= 10;
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
            StartCoroutine(PlayerHit());
        }
    }
    public IEnumerator PlayerHit()
    {
        int count = 5;
        hpObject.SetActive(true);
        while(count > 0)
        {
            yield return new WaitForSeconds(0.1f);
            
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

    void ShowHpBar(float Hp)
    {
        imgHPBar.fillAmount = Hp / 100.0f;
    }
    void ShowMpBar(float Mp)
    {
        imgMPBar.fillAmount = Mp / maxPlayerMp;
    }
}
