using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
            
    }

    internal static void RightClick(GameObject owner, Vector3 point)
    {
        if(owner.GetComponent<Terrain>() is Terrain)
        {
            if(SelectObjects.selectedObjects.Count > 0)
            {
                foreach (var selectedUnit in SelectObjects.selectedObjects)
                {
                    Debug.Log(selectedUnit.name + " MoveToPoint ");

                    selectedUnit.MoveToPoint(point);
                }
            }
        }
    }

    internal static void LeftClick(GameObject owner, Vector3 point)
    {
        if (owner.GetComponent<Terrain>() is Terrain)
        {
            if (SelectObjects.selectedObjects.Count > 0)
            {
                SelectObjects.Deselect();
            }
        }
    }

    internal static void ClickAtUnit(Unit target)
    {
        if (Input.GetMouseButton(0))
        {
            SelectObjects.Deselect();
            Debug.Log("ClickAtUnit");

            SelectObjects.SelectUnit(target);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (SelectObjects.selectedObjects.Count > 0)
            {
                List<Unit> targets = new List<Unit>();
                targets.Add(target);
                foreach (var attacking in SelectObjects.selectedObjects)
                {
                    if (attacking != null)
                    {
                        Debug.Log(attacking.name + "attacing " + target.name);
                        attacking.SetAttackTarget(targets);
                    }
                }
            }
        }
    }


}
