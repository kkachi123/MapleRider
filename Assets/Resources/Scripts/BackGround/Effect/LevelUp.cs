using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 2.0f; 
        StartCoroutine("LevelUp_Time");
        SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.LevelUp);
    }
    IEnumerator LevelUp_Time()
    {
        // Wait for the animation to finish
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            yield return null;

        Destroy(gameObject);
    }
}
