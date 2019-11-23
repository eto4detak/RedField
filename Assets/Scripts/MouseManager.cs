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
                    WorldManager.infoPanel.SelectUnit(filter.selfGroup);
                    return;
                }
                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    ClickToTerrain(filterTerrain);
                    WorldManager.infoPanel.ClearPanel();
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
                //attack

                Unit unitTarget = mouseHit.collider.GetComponent(typeof(Unit)) as Unit;
                if (unitTarget)
                {
                    UnitGroup target = unitTarget.selfGroup;
                    if (target)
                    {
                        ClickRightAtUnit(target);
                        
                        return;
                    }
                }

                //move

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
    internal static void ClickRightAtUnit(UnitGroup target)
    {
        if (SelectObjects.HaveSelected())
        {
            UnitGroup.SetAttackCommand(SelectObjects.selectedGroups, target);
        }
        WorldManager.infoPanel.SetTarget(target);
    }
    internal static void ClickRightToTerrain(Terrain terrain, Vector3 newPosition)
    {
        UnitGroup.SetMoveCommand(SelectObjects.selectedGroups, newPosition);
        WorldManager.infoPanel.ClearTarget();
    }
}
