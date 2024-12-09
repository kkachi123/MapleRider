using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EliteStateControl : MonoBehaviour
{
    EliteBase action;
    EliteProfile profile;
    [SerializeField] GameObject hpSlider;
    Slider hpBar;
    void Awake()
    {
        hpBar = hpSlider.GetComponentInChildren<Slider>();
        profile = GetComponent<EliteProfile>();
        action = new EliteMove();
    }
    void OnEnable()
    {
        hpSlider.SetActive(true);
        action = new EliteMove();
    }

    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {
            hpBar.value = profile.hp / profile.maxHp;
            if (profile.hp <= 0.0f)
            {
                hpSlider.SetActive(false);
                ChangeState(new EliteDeath());
            }
            else
                TransitionState();
        }
    }

    public void ChangeState(EliteBase eliteBase)
    {
        action = eliteBase;
        TransitionState();
    }
    void TransitionState()
    {
        if (action != null)
        {
            action.Active(this , profile);
        }
    }
}
