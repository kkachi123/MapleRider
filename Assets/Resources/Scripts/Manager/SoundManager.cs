using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sInstance;

    public enum BGM
    {
        Lobby,
        Game,
        FinalPhase1,
        FinalPhase2,
    }
    public enum PlayerSFX
    {
        Attack,
        Bress,
        GetCoin,
        PlayerDie,
        LevelUp,
    }
    public enum EnemySFX
    {
        Attack,
        Hit,
        MonsterDie,
        BossAttackStand,
        BossRazer,
        BossDie,
    }
    public enum SFX
    {
        ButtonClick,
        MoveScene,
        Warning,
    }
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] playersfxs;
    [SerializeField] AudioClip[] enemysfxs;
    [SerializeField] AudioClip[] sfxs;

    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioPlayerSfx;
    [SerializeField] AudioSource audioEnemySfx;
    [SerializeField] AudioSource audioSfx;

    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        sInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayBGM(BGM bgmType)
    {
        if (audioBgm.isPlaying)
            audioBgm.Stop();
        audioBgm.clip = bgms[(int)bgmType];
        audioBgm.loop = true;
        audioBgm.Play();
    }
    public void StopBGM()
    {
        audioBgm.Stop();
    }
    public void PlaySFXPlayer(PlayerSFX sfxType)
    {
        audioPlayerSfx.PlayOneShot(playersfxs[(int)sfxType]);
    }
    public void PlaySFXEnemy(EnemySFX sfxType)
    {
        if(sfxType == EnemySFX.BossAttackStand || sfxType == EnemySFX.BossRazer)
        {
            if(!audioEnemySfx.isPlaying)
                audioEnemySfx.PlayOneShot(enemysfxs[(int)sfxType]);
        }
        else
            audioEnemySfx.PlayOneShot(enemysfxs[(int)sfxType]);

    }
    public void PlaySFX(SFX sfxType)
    {
        audioSfx.PlayOneShot(sfxs[(int)sfxType]);
    }
    public void StopSFX()
    {
        audioSfx.Stop();
    }
}
