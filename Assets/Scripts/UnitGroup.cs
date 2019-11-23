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
        units.AddRange(GetComponents<Unit>());
    }

    protected virtual void Start()
    {
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }


    public static void CreateUnits(Vector3 position)
    {
        UnitGroup group = GetPrefab();
        if (group != null)
        {
            UnitGroup cloneUnitGroup = Instantiate(group, position, Quaternion.identity);
            foreach (var unit in cloneUnitGroup.GetComponentsInChildren<Unit>())
            {
                unit.selfGroup = cloneUnitGroup;
                cloneUnitGroup.units.Add(unit);
            }
        }
    }


    public static UnitGroup GetPrefab()
    {
        return Resources.Load<UnitGroup>("Prefabs/UniversalGroup");
    }

    public void MoveGroupToPoint(Vector3 point, Vector3 offset)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3[] newPositions = Formation.GetSquareGroupPositions(hit.point, units.Count, unitIndent);
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null) continue;
                units[i].agent.destination = newPositions[i] + offset;
            }
        }
    }



}

