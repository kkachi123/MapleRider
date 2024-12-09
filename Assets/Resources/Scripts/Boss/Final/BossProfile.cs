using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProfile : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public float hp = 300.0f;
    [HideInInspector] public float maxHp;

    [Header("Move")]
    public float fadeSpeed = 100.0f;
    public Transform[] bossPos;
    public int movePosIndex = 0;
    Vector3 movePos;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public GameObject player;
    public Transform shootPoint;

    public int bulletCount = 15;
    int maxBulletCount;

    public float power = 30.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Init(0);
    }
    public void Init(int index)
    {
        hp = 300;
        maxHp = hp;
        fadeSpeed = 300.0f;
        power = 30.0f;
        maxBulletCount = bulletCount;
        movePosIndex = index;
        PosSet();
    }
    public void PosSet()
    {
        movePos = bossPos[movePosIndex].position;
    }
    public bool IsFading()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, fadeSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePos) <= 0.1f)
        {
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
            //animator.SetFloat("BulletCount", maxBulletCount);
            animator.SetBool("Shoot", true);
            return false;
        }
        return true;
    }
    public bool IsShooting()
    {
        for(int i = 1; i < 6; i++)
        {
            float angle = 90 + i * 30f;
            float radian = angle * Mathf.Deg2Rad;
            Vector2 shootDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bulletPrefab.GetComponent<BossBullet>().power = power;
            bulletPrefab.GetComponent<BossBullet>().spreadDirection = shootDirection;
        }
        

        bulletCount--;
        if (bulletCount <= 0)
        {
            bulletCount = maxBulletCount;
            animator.SetBool("Shoot", false);
            //animator.SetFloat("BulletCount", bulletCount);

            return false;
        }
        return true;
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
