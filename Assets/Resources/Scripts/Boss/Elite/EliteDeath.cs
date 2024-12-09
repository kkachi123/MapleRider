using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteDeath : EliteBase
{
    bool couroutine = false;
    public override void Active(EliteStateControl manager, EliteProfile profile)
    {
        if (!couroutine)
        {
            couroutine = true;
            profile.EliteDeath();
        }
    }
}
