using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl2D : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] GameObject resultUI;
    private void Start()
    {
        resultUI.SetActive(false);
        SoundManager.sInstance.PlayBGM(SoundManager.BGM.Game);
    }
    void Update()
    {
        if (GameManager.Instance.bGameOver)
        {
            if(Input.GetMouseButtonDown(0))
                SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.ButtonClick);
            finalScore.text = "Score : " + GameManager.Instance.score;
            resultUI.SetActive(true);
        }
        else
            scoreText.text = "Score : " + GameManager.Instance.score;
    }
    public void TurnBackLobby()
    {
        SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.MoveScene);
        LoadingSceneManager.LoadScene("Lobby");
    }
}
