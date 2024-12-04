using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BossState
{
    Idle,
    Move,
    Attack,
    Death
}
public interface IActive
{
    BossState Active();
}
public class BossBase
{
    public virtual void Active(BossStateControl manager , Animator animator) { }
}
