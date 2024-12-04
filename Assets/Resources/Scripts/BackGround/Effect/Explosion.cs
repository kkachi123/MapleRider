using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 2.0f; // default 1.0f , Animation speed
        StartCoroutine("Explosion_Time");
        SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Hit);
    }
    IEnumerator Explosion_Time()
    {
        // Wait for the animation to finish
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        Destroy(gameObject);
    }

}
