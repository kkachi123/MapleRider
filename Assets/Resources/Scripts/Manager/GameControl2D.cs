using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl2D : MonoBehaviour
{
    [Header("Score Panel")]
    [SerializeField] GameObject textScreen;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Result Panel")]
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] GameObject resultUI;

    [Header("Warning")]
    [SerializeField] GameObject warningScreen;

    int timer = 0;
    float deltaTime = 0;
    int prePhase = 0;
    private void Start()
    {
        timer = 0;
        deltaTime = 0;
        resultUI.SetActive(false);
        warningScreen.SetActive(false);
        SoundManager.sInstance.PlayBGM(SoundManager.BGM.Game);
    }
    void Update()
    {
        if (GameManager.Instance.bGameOver)
        {
            if(Input.GetMouseButtonDown(0))
                SoundManager.sInstance.PlaySFX(SoundManager.SFX.ButtonClick);
            finalScore.text = "Score : " + GameManager.Instance.score;
            textScreen.SetActive(false);
            resultUI.SetActive(true);
        }
        else
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= 1.0f)
            {
                timer++;
                deltaTime = 0;
            }
            timerText.text = timer.ToString();
            levelText.text = "Lv." + GameManager.Instance.level;
            scoreText.text = "Score : " + GameManager.Instance.score;
        }
        SpawnBoss();
        if (Input.GetKeyDown(KeyCode.Space))
            GameManager.Instance.score = 5000;
    }
    public void SpawnBoss()
    {
        if (GameManager.Instance.bossPhase != prePhase)
        {
            prePhase = GameManager.Instance.bossPhase;
            if(prePhase == 1 || prePhase == 4)
            {
                SoundManager.sInstance.PlaySFX(SoundManager.SFX.Warning);
                warningScreen.SetActive(true);
            }
        }
    }
    public void TurnBackLobby()
    {
        SoundManager.sInstance.PlaySFX(SoundManager.SFX.MoveScene);
        LoadingSceneManager.LoadScene("Lobby");
    }
}
