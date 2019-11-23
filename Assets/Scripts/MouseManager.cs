using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private RaycastHit mouseHit;
    private Ray mouseRay;
    void Start()
    {
        
    }
 
    void Update()
    {
        CheckLeftClick();
        CheckRightClick();
    }


    private void CheckLeftClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                Unit filter = mouseHit.collider.GetComponent(typeof(Unit)) as Unit;
                if (filter)
                {
                    SelectObjects.Deselect();
                    SelectObjects.SelectUnit(filter);
                    return;
                }
                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    ClickToTerrain(filterTerrain);
                    return;
                }
            }
        }
    }

    private void CheckRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                Unit filter = mouseHit.collider.GetComponent(typeof(Unit)) as Unit;
                if (filter)
                {
                    ClickRightAtUnit(filter);
                    return;
                }
                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    ClickRightToTerrain(filterTerrain, Input.mousePosition);
                    return;
                }
            }
        }
    }
    internal static void ClickToTerrain(Terrain terrain)
    {
        if (SelectObjects.HaveSelected())
        {
            SelectObjects.Deselect();
        }
    }
    internal static void ClickRightAtUnit(Unit target)
    {
        if (SelectObjects.HaveSelected())
        {
            List<Unit> targets = new List<Unit>();
            targets.Add(target);
            foreach (var attackingUnit in SelectObjects.selectedObjects)
            {
                if (attackingUnit != null)
                {
                    Debug.Log(attackingUnit.name + "attacing " + target.name);
                    attackingUnit.SetAttackTarget(targets);
                }
            }
            foreach (var attackingGroup in SelectObjects.selectedGroups)
            {
                if (attackingGroup != null)
                {
                    
                    Debug.Log(attackingGroup.name + " attackingGroup " + target.name);
                    attackingGroup.command = new AttackCommand(target);
                   // attackingGroup.SetAttackTarget(targets);
                }
            }
        }
    }
    internal static void ClickRightToTerrain(Terrain terrain, Vector3 newPosition)
    {
        Formation.SetFormationRow(newPosition);
    }


}
