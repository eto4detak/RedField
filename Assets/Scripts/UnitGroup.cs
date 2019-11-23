using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroup : MonoBehaviour, IFrontImage
{   

    public UnitCommand command;
    int unitIndent = 2;
    internal List<Unit> units = new List<Unit>();

    public Sprite FrontImage { get; set; }

    private void Awake()
    {
        WorldManager.groupCount++;
        units.AddRange(GetComponents<Unit>());
        command = new StopCommand();
    }

    protected virtual void Start()
    {
       
    }

    protected virtual void Update()
    {
        
    }






    public static void CreateUnits(Vector3 position)
    {
        UnitGroup group = GetPrefab();
        if (group != null)
        {
            UnitGroup cloneUnitGroup = Instantiate(group, position, Quaternion.identity);
            cloneUnitGroup.name = "UnitGroup " + WorldManager.groupCount;
            foreach (var unit in cloneUnitGroup.GetComponentsInChildren<Unit>())
            {
                unit.selfGroup = cloneUnitGroup;
                cloneUnitGroup.units.Add(unit);
            }
        }
    }
    public IEnumerator DoCommand()
    {
        while (true)
        {
            command.DoCommand();
            yield return new WaitForSeconds(0.5f);
        }
    }
    public static UnitGroup GetPrefab()
    {
        return Resources.Load<UnitGroup>("Prefabs/UniversalGroup");
    }



    public void MoveGroupToPoint2D(Vector3 newPosition2d, Vector3 groupOffset3d)
    {
        Ray ray = Camera.main.ScreenPointToRay(newPosition2d);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3[] newPositions = Formation.GetSquareGroupPositions(hit.point, units.Count, unitIndent);
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null) continue;
                units[i].agent.destination = newPositions[i] + groupOffset3d;
            }
        }
    }

    public void MoveGroupToPoint3D(Vector3 newPosition)
    {
        Vector3[] newPositions = Formation.GetSquareGroupPositions(newPosition, units.Count, unitIndent);
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] == null) continue;
            
            units[i].agent.destination = newPositions[i];
            //units[i].agent.destination = newPosition;
        }

    }

    public static void SetMoveCommand(List<UnitGroup> groups, Vector3 newPosition)
    {
        if (groups.Count > 0)
        {
            Vector3[] groupPositions = Formation.GetSquareGroupPositions(new Vector3(), SelectObjects.selectedGroups.Count, Formation.groupIndent);
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].command = new MoveCommand(groups[i], newPosition, groupPositions[i]);
            }
        }
    }

    public static void SetAttackCommand(List<UnitGroup> attackingGroups, UnitGroup target)
    {

        foreach (var attackingGroup in attackingGroups)
        {
            if (attackingGroup != null)
            {

                attackingGroup.command = new AttackCommand(attackingGroup, target);
            }
        }
    }

    public void StartCoroutineCommand()
    {
        StartCoroutine(DoCommand());
    }
    public void StopCoroutineCommand()
    {
        StopCoroutine(DoCommand());
    }



}

