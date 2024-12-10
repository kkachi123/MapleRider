using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteProfile : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    public float hp = 300.0f;
    [HideInInspector]public float maxHp;

    [Header("Move")]
    public float moveSpeed = 100.0f;
    public Transform[] bossPos;
    public int movePosIndex = 0;
    Vector3 movePos;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public GameObject player;
    public Transform shootPoint;

    public int bulletCount = 5;
    int maxBulletCount;

    public float power = 10.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        maxBulletCount = bulletCount;
        Init(0);
    }
    public void Init(int index)
    {
        animator.SetFloat("Speed", 10.0f);
        hp = 100 * GameManager.Instance.level;
        maxHp = hp;
        moveSpeed = 100.0f * GameManager.Instance.level;
        power = 10.0f * GameManager.Instance.level;
        maxBulletCount = bulletCount;
        if (power > 100.0f) power = 100.0f;
        movePosIndex = index;
        PosSet();
    }
    public void PosSet()
    {
        movePos = bossPos[movePosIndex].position;
    }
    public bool IsMoving()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
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
            animator.SetBool("IsAttack", true);
            return false;
        }
        return true;
    }
    public bool IsShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        bullet.GetComponent<ElitBullet>().player = player;
        bullet.GetComponent<ElitBullet>().power = power;

        bulletCount--;
        if (bulletCount <= 0)
        {
            animator.SetFloat("Speed", 0.0f);
            animator.SetBool("IsAttack", false);

            bulletCount = maxBulletCount;
            
            return false;
        }
        return true;
    }

    public void EliteDeath()
    {
        animator.SetTrigger("Death");
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        GameManager.Instance.elitCount--;
        GameManager.Instance.score += 200;
    }
}
