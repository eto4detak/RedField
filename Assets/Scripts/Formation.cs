using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private static float groupIndent = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetFormationRow(Vector3 newPosition)
    {
        if (SelectObjects.HaveSelected())
        {
            foreach (var selectedUnit in SelectObjects.selectedObjects)
            {
                selectedUnit.MoveToPoint(newPosition);
            }
            if(SelectObjects.selectedGroups.Count > 0)
            {
                Vector3[] groupPositions = Formation.GetSquareGroupPositions(new Vector3(), SelectObjects.selectedGroups.Count, groupIndent);
                for (int i = 0; i < SelectObjects.selectedGroups.Count; i++)
                {

                    SelectObjects.selectedGroups[i].MoveGroupToPoint(newPosition,  groupPositions[i]);
                }
            }
        }
    }

    public static Vector3[] GetSquareGroupPositions(Vector3 point, int unitCount, float distance)
    {
        int countInRow = Mathf.FloorToInt(Mathf.Sqrt(unitCount));
        Vector3[] newPositions = new Vector3[unitCount];
        float deltaX = countInRow / 2 * distance;
        float deltaZ = unitCount / countInRow / 2 * distance;
        for (int i = 0; i < unitCount; i++)
        {
            float x = i % countInRow * distance - deltaX;
            float z = Mathf.CeilToInt(i / countInRow) * distance - deltaZ;
            newPositions[i] = point + new Vector3(x, 0, z);
        }
        return newPositions;
    }
}
