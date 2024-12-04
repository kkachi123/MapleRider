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
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.ButtonClick);
        }
    }
    public void StartGame()
    {
        SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.MoveScene);
        LoadingSceneManager.LoadScene("Play");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
