using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommand
{
     UnitGroup SelfGroup { get; set; }
    UnitGroup Target { get; set; }
    public virtual void DoCommand()
    {

    }
    public virtual void OnStay(Unit unit)
    {

    }
}

public class AttackCommand : UnitCommand
{
    public UnitGroup SelfGroup { get; set; }
    public UnitGroup Target { get; set; }

    public AttackCommand(UnitGroup paramSelf, UnitGroup paramTarget)
    {
        Target = paramTarget;
        SelfGroup = paramSelf;
        DoCommand();
        //SelfGroup.StartCoroutineCommand();
    }
    public override void DoCommand()
    {
        for (int i = 0; i < Target.units.Count; i++)
        {
            if(Target.units[i] != null)
            {
                SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
            }
        }
    }
    public override void OnStay(Unit unitOther)
    {
        foreach (var unitTarget in Target.units)
        {
           if( unitTarget == unitOther)
            {
                unitTarget.ReceiveDamage();
            }
        }
    }
    


}
public class MoveCommand : UnitCommand
{
    public UnitGroup SelfGroup { get; set; }
    public UnitGroup Target { get; set; }
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public MoveCommand(UnitGroup paramGroup, Vector3 paramNewPosition, Vector3 paramGroupOffset)
    {
        SelfGroup = paramGroup;
        NewPosition = paramNewPosition;
        GroupOffset = paramGroupOffset;
        DoCommand();
    }

    public override void DoCommand()
    {
        SelfGroup.MoveGroupToPoint2D(NewPosition, GroupOffset);
    }
    public override void OnStay(Unit unit)
    {

    }
}
public class StopCommand : UnitCommand
{
    public UnitGroup SelfGroup { get; set; }
    public UnitGroup Target { get; set; }
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public StopCommand()
    {

    }

    public override void DoCommand()
    {
        SelfGroup.MoveGroupToPoint2D(NewPosition, GroupOffset);
    }
    public override void OnStay(Unit unit)
    {

    }
}
