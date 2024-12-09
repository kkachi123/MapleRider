using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMove : EliteBase
{
    public override void Active(EliteStateControl manager, EliteProfile profile)
    {
        if(!profile.IsMoving())
            manager.ChangeState(new EliteAttack());
    }
}
