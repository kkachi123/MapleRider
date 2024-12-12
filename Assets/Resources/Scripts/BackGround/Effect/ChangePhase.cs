using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhase : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] GameObject[] backGrounds;

    [Header("FadeScreen")]
    [SerializeField] GameObject fadeScreen;
    [SerializeField] Transform movePos;
    [SerializeField] Transform endPos;
    [SerializeField] float moveSpeed = 3000.0f;

    [Header("Boss")]
    [SerializeField] GameObject[] bosses;

    [Header("Player")]
    [SerializeField] GameObject player;
    Vector3 startPos;
    void OnEnable()
    {
        player.GetComponent<CircleCollider2D>().enabled = false;
        startPos = fadeScreen.transform.position;
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        while(fadeScreen.transform.position != movePos.position)
        {
            fadeScreen.transform.position = Vector3.MoveTowards(fadeScreen.transform.position, movePos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        for (int i = 0; i < backGrounds.Length; i++)
            backGrounds[i].SetActive(false);
        if (GameManager.Instance.bossPhase == 1)
        {
            backGrounds[1].SetActive(true);
            SoundManager.sInstance.PlayBGM(SoundManager.BGM.FinalPhase1);
            for(int i = 0; i < bosses.Length; i++)
            {
                bosses[i].SetActive(true);
            }
        }
        else if (GameManager.Instance.bossPhase == 4)
        {
            backGrounds[2].SetActive(true);
            SoundManager.sInstance.PlayBGM(SoundManager.BGM.FinalPhase2);
        }
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        while (fadeScreen.transform.position != endPos.position)
        {
            fadeScreen.transform.position = Vector3.MoveTowards(fadeScreen.transform.position, endPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        SoundManager.sInstance.StopSFX();
        fadeScreen.transform.position = startPos;
        this.gameObject.SetActive(false);
        player.GetComponent<CircleCollider2D>().enabled = true;
    }
}
