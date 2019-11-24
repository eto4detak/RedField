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
        SelfGroup.StartCoroutineCommand();
    }
    public override void DoCommand()
    {
        Debug.Log("AttackCommand " + Target.units.Count);

        if(Target.units.Count > 0)
        {
            for (int i = 0; i < Target.units.Count; i++)
            {
                if (Target.units[i] != null)
                {
                    SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
                    return;
                }
            }
            
            SelfGroup.StopCoroutineCommand();
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

        SelfGroup.MoveGroupToPoint2D(NewPosition, GroupOffset);
        DoCommand();

        SelfGroup.StartCoroutineCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("MoveCommand");
        if (SelfGroup.CheckStopped())
        {
            SelfGroup.command = new StopCommand(SelfGroup);
        }
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
    public StopCommand(UnitGroup paramSelfGroup)
    {
        SelfGroup = paramSelfGroup;
        SelfGroup.StopCoroutineCommand();
        DoCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("StopCommand");

    }

    public override void OnStay(Unit unitOther)
    {
        //foreach (var unitTarget in Target.units)
        //{
        //    if (unitTarget == unitOther)
        //    {
        //        unitTarget.ReceiveDamage();
        //    }
        //}
    }
}

public class PursueCommand : UnitCommand
{
    public UnitGroup SelfGroup { get; set; }
    public UnitGroup Target { get; set; }
    public PursueCommand(UnitGroup paramGroup, UnitGroup paramTarget)
    {
        SelfGroup = paramGroup;
        Target = paramTarget;

        DoCommand();
        //SelfGroup.StopCoroutineCommand();
        SelfGroup.StartCoroutineCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("PursueCommand");

        if (Target.units.Count > 0)
        {
            for (int i = 0; i < Target.units.Count; i++)
            {
                if (Target.units[i] != null)
                {
                    SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
                    return;
                }
            }

            SelfGroup.StopCoroutineCommand();
        }

        //if (SelfGroup.CheckStopped())
        //{
        //    SelfGroup.command = new StopCommand(SelfGroup);
        //}
    }
    public override void OnStay(Unit unit)
    {

    }
}
