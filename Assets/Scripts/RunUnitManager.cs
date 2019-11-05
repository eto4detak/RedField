using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;

public class RunUnitManager : MonoBehaviour
{
    //Animator animator;
   // NavMeshAgent agent;
    public Camera cam;
    private IList<Unit> selectedUnits = new List<Unit>();

    // Use this for initialization
    void Start()
    {
       // animator = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindGameObjectUnderMouse();
        MoveUnit();
    }


    private void FindGameObjectUnderMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            selectedUnits.Clear();
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