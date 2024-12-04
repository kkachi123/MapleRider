using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour, IActive
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public BossState Active()
    {
        StartCoroutine(Death());
        return BossState.Death;
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        GameManager.Instance.elitCount--;
        GameManager.Instance.score += 200;
    }
}
