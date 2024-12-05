using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillPos : MonoBehaviour
{
    [SerializeField]GameObject player;
    void FixedUpdate()
    {
        this.transform.position = player.transform.position;
    }
}
