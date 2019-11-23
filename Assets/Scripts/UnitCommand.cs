using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommand
{

}


public class AttackCommand : UnitCommand
{
    private UnitGroup groupTarget;
    private Unit unitTarget;
    public AttackCommand(Unit paramTarget)
    {
        if (paramTarget.selfGroup)
        {
            groupTarget = paramTarget.selfGroup;
        }
        else
        {
            unitTarget = paramTarget;
        }
    }

}