using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyControl : MonoBehaviour
{
    private void Start()
    {
        SoundManager.sInstance.PlayBGM(SoundManager.BGM.Lobby);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.sInstance.PlaySFX(SoundManager.SFX.ButtonClick);
        }
    }
    public void StartGame()
    {
        GameManager.Instance.score = 0;
        GameManager.Instance.level = 1;
        GameManager.Instance.bGameOver = false;
        GameManager.Instance.bossPhase = 0;
        GameManager.Instance.elitCount = 0;
        SoundManager.sInstance.PlaySFX(SoundManager.SFX.MoveScene);
        LoadingSceneManager.LoadScene("Play");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
