using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class RunUnitManager : MonoBehaviour
{
    //Animator animator;
    // NavMeshAgent agent;
    HighlightManager highlight = new HighlightManager();
    public Camera cam;
    internal static IList<Unit> selectedUnits = new List<Unit>();
    //private bool isClear

    void Start()
    {
        // animator = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // FindGameObjectUnderMouse();
        //HighlightUnits();
        MoveUnit();
    }


    public static void SetSelectedUnits(List<Unit> units)
    {
        foreach (var unit in units)
        {
            selectedUnits.Add(unit);
        }
    }
    public static void SetSelectedUnit(Unit unit)
    {
        selectedUnits.Add(unit);
    }
    public static void RemoveSelectedUnit(Unit unit)
    {
        selectedUnits.Remove(unit);
    }

    public static void ClearSelectUnits()
    {
        //HighlightManager.ClearHighlightUnits(selectedUnits);
        selectedUnits.Clear();
    }

    private void FindGameObjectUnderMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClearSelectUnits();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null  )
                {
                    Unit unit = hit.collider.gameObject.GetComponent<Unit>();
                    if (unit != null)
                    {
                        selectedUnits.Add(unit);
                    }
                }
            }
        }
    }

    private void MoveUnit()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                foreach (var unit in selectedUnits)
                {
                    unit.gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                }
            }

        }

        //if (agent.velocity.magnitude > 2f)
        //{
        //    animator.SetBool("walk", true);
        //}
        //else
        //{
        //    animator.SetBool("walk", false);
        //}
    }

}