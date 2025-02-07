using UnityEngine;
public class Blackboard : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    [Header("Status")]
    public bool isAlive = true;
    public bool doPattern = false;
    public float hp = 800.0f;
    [HideInInspector] public float maxHp;

    [Header("Fade")]
    [HideInInspector] public bool isFade = true;
    [HideInInspector] public bool preFade = false;
    [HideInInspector] public SpriteRenderer bossImage;
    public float fadeTime = 2.0f;
    [HideInInspector] public float time = 0.0f;
    public Transform[] bossPos;
    public int movePosIndex = 0;
    [HideInInspector] public Vector3 movePos;

    [Header("Attack")]
    [HideInInspector] public BossAttackType attackType = BossAttackType.None;
    public float power = 30.0f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    [HideInInspector] public float addShootAngle = 30.0f;
    public int bulletCount = 15;
    [HideInInspector] public int maxBulletCount;

    [Header("Razer")]
    public GameObject[] Razers = new GameObject[3];
    public GameObject[] RazersWarningPrefab = new GameObject[3];
    [HideInInspector] public SpriteRenderer[] RazersWarning = new SpriteRenderer[3];
    public int razersCount = 0;

    [Header("ElitSummon")]
    public GameObject enemySpawner;
    [HideInInspector] public EnemySpawner eliteSummoner;
    public int summonEliteCount = 0;
    public void Init(int index)
    {
        for (int i = 0; i < RazersWarning.Length; i++)
        {
            RazersWarning[i] = RazersWarningPrefab[i].GetComponent<SpriteRenderer>();
        }
        eliteSummoner = enemySpawner.GetComponent<EnemySpawner>();
        bossImage = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hp = 800;
        maxHp = hp;
        fadeTime = 2.0f;
        power = 30.0f;
        maxBulletCount = bulletCount;
        movePosIndex = index;
        razersCount = 0;
        summonEliteCount = 0;
        PosSet();
    }
    public void PosSet()
    {
        movePos = bossPos[movePosIndex].position;
    }
}
