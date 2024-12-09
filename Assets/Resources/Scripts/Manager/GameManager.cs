using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager sInstance;

    public static GameManager Instance
    {
        get
        { 
            if(sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }
    private void Awake()
    {
        if(sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public bool bGameOver = false;
    public int score = 0;
    public int level = 1;
    public int elitCount = 0;
    public int bossPhase = 0;
}
