using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField]GameObject player;
    void Update()
    {
        this.transform.position = player.transform.position;
    }
}
