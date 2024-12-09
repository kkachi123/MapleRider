using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIControl : MonoBehaviour
{
    [SerializeField] GameObject[] bosses;
    [SerializeField] GameObject[] bossesUI;
    void Update()
    {
        for(int i = 0; i < bosses.Length; i++)
        {
            if (bosses[i].activeSelf)
                bossesUI[i].SetActive(true);
            else
                bossesUI[i].SetActive(false);
        }
    }
}
