using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProfile : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public float hp = 800.0f;
    [HideInInspector] public float maxHp;

    [Header("Fade")]
    [HideInInspector] public bool preFade = false;
    SpriteRenderer bossImage;
    public float fadeTime = 2.0f;
    float time = 0.0f;
    public Transform[] bossPos;
    public int movePosIndex = 0;
    Vector3 movePos;

    [Header("Attack")]
    [HideInInspector] public BossAttackType attackType = BossAttackType.None;
    public float power = 30.0f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    float addShootAngle = 30.0f;
    public int bulletCount = 15;
    int maxBulletCount;

    [Header("Razer")]
    [SerializeField] GameObject[] Razers = new GameObject[3];
    [SerializeField] GameObject[] RazersWarningPrefab = new GameObject[3];
    SpriteRenderer[] RazersWarning = new SpriteRenderer[3];
    public int razersCount = 0;

    [Header("ElitSummon")]
    [SerializeField]GameObject enemySpawner;
    EnemySpawner eliteSummoner;
    public int summonEliteCount = 0;

    private void Awake()
    {
        for(int i = 0; i < RazersWarning.Length; i++)
        {
            RazersWarning[i] = RazersWarningPrefab[i].GetComponent<SpriteRenderer>();
        }
        eliteSummoner = enemySpawner.GetComponent<EnemySpawner>();
        bossImage = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Init(0);
    }
    public void Init(int index)
    {
        hp = 800;
        maxHp = hp;
        fadeTime = 2.0f;
        power = 30.0f;
        maxBulletCount = bulletCount;
        movePosIndex = index;
        razersCount = 0;
        summonEliteCount = 0;
        PosSet();
    }
    public void PosSet()
    {
        movePos = bossPos[movePosIndex].position;
    }
    public bool FadeIn()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        Color imgAlpha = bossImage.color;
        if(imgAlpha.a > 0)
        {
            time += Time.deltaTime / fadeTime;
            imgAlpha.a = Mathf.Lerp(1, 0, time);
            bossImage.color = imgAlpha;
            return true;
        }
        time = 0.0f;
        imgAlpha.a = 0;
        transform.position = movePos;
        switch (movePosIndex)
        {
            case 0:
                movePosIndex = 1;
                break;
            case 1:
                movePosIndex = 2;
                break;
            case 2:
                movePosIndex = 0;
                break;
        }
        return false;
    }
    public bool FadeOut()
    {
        Color imgAlpha = bossImage.color;
        if (imgAlpha.a < 1)
        {
            time += Time.deltaTime / fadeTime;
            imgAlpha.a = Mathf.Lerp(0, 1, time);
            bossImage.color = imgAlpha;
            return true;
        }
        time = 0.0f;
        imgAlpha.a = 1;
        animator.SetBool("Fade", false);
        bossImage.color = imgAlpha;
        this.GetComponent<CircleCollider2D>().enabled = true;
        return false;
    }
    public bool IsShooting()
    {
        float angleStart = 0;
        switch(movePosIndex)
        {
            case 0:
                angleStart = 0;
                break;
            case 1:
                angleStart = 90;
                break;
            case 2:
                angleStart = 180;
                break;
        }
        angleStart += addShootAngle;
        addShootAngle += 30.0f;
        for(int i = 1; i < 6; i++)
        {
            float angle = angleStart + i * 30f;
            float radian = angle * Mathf.Deg2Rad;
            Vector2 shootDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bullet.GetComponent<BossBullet>().power = power;
            bullet.GetComponent<BossBullet>().spreadDirection = shootDirection;
        }
        

        bulletCount--;
        if (bulletCount <= 0)
        {
            bulletCount = maxBulletCount;
            addShootAngle = 30.0f;
            animator.SetBool("Shoot", false);
            return false;
        }
        return true;
    }
    public bool RazerWarning()
    {
        if (Razers[0].activeSelf) return false;
        Color imgAlpha = RazersWarning[0].color;
        for(int i = 0; i < RazersWarningPrefab.Length; i++)
            RazersWarningPrefab[i].GetComponent<RazerMove>().isWarning = true;
        if (imgAlpha.a < 1)
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.BossAttackStand);
            time += Time.deltaTime / fadeTime;
            imgAlpha.a = Mathf.Lerp(0, 1, time);
            for (int i = 0; i < razersCount; i++)
                RazersWarning[i].color = imgAlpha;
            return true;
        }
        time = 0.0f;
        imgAlpha.a = 0;
        for (int i = 0; i < RazersWarning.Length; i++)
        {
            RazersWarning[i].color = imgAlpha;
            RazersWarningPrefab[i].GetComponent<RazerMove>().isWarning = false;
        }
        for(int i = 0; i < razersCount; i++)
        {
            Razers[i].SetActive(true);
        }
        animator.SetBool("Shoot", false);
        return false;
    }
    public void EliteSummon()
    {
        animator.SetBool("Shoot", false);
        eliteSummoner.EliteSpawn(summonEliteCount);
    }

    public void BossDeath()
    {
        animator.SetTrigger("Death");
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        GameManager.Instance.score += 5000;
        GameManager.Instance.bGameOver = true;
    }
}
