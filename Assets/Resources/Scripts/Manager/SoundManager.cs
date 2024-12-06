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
        ButtonClick,
        MoveScene,
    }
    public enum EnemySFX
    {
        Attack,
        Hit,
        MonsterDie,
        BossAttackStand,
        BossAttack,
        BossDie,
    }
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] playersfxs;
    [SerializeField] AudioClip[] enemysfxs;

    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioPlayerSfx;
    [SerializeField] AudioSource audioEnemySfx;

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
        audioEnemySfx.PlayOneShot(enemysfxs[(int)sfxType]);
    }
}
