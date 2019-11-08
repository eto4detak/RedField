using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    public static List<GameObject> allowedSelectObj = new List<GameObject>(); // массив всех юнитов, которых мы можем выделить
    public static List<GameObject> selectedObjects; // выделенные объекты

    public GUISkin skin;
    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;
    private float minY = 0;

    void Awake()
    {
        selectedObjects = new List<GameObject>();
    }

    // проверка, добавлен объект или нет
    bool CheckUnit(GameObject unit)
    {
        bool result = false;
        foreach (GameObject u in selectedObjects)
        {
            if (u == unit) result = true;
        }
        return result;
    }


    public static void SetAllowed(GameObject obj)
    {
        allowedSelectObj.Add(obj);
    }

    void Select()
    {
        if (selectedObjects.Count > 0)
        {
            for (int j = 0; j < selectedObjects.Count; j++)
            {
                Unit unit = selectedObjects[j].GetComponent<Unit>();
                if (unit != null)
                {
                    //RunUnitManager.selectedUnits.Add(unit);
                    RunUnitManager.SetSelectedUnit(unit);
                    HighlightManager.HighlightUnit(unit);
                }
            }
        }
    }

    void Deselect()
    {
        if ( selectedObjects.Count > 0)
        {
            for (int j = 0; j < selectedObjects.Count; j++)
            {
                Unit unit = selectedObjects[j].GetComponent<Unit>();
                if (unit != null)
                {
                    
                }
            }
            RunUnitManager.ClearSelectUnits();
            HighlightManager.Clear();
        }
    }

    private Vector2 GetAvailablePosition(Vector2 point)
    {
        if (point.y < minY)
        {
            point.y = minY;
        }
        return point;
    }

    public void SetForbiddenPosition(Vector2 point)
    {
        minY = point.y;
    }


    void OnGUI()
    {

        GUI.skin = skin;
        GUI.depth = 99;

        if (Input.GetMouseButtonDown(0))
        {
            Deselect();
            startPos = Input.mousePosition;
            draw = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            draw = false;
            Select();
        }

        if (draw)
        {
            if (!CheckPosition(startPos, endPos)) return;
            selectedObjects.Clear();
            endPos = Input.mousePosition;
            if (startPos == endPos) return;

            endPos = GetAvailablePosition(endPos);

            rect = new Rect(Mathf.Min(endPos.x, startPos.x),
                            Screen.height - Mathf.Max(endPos.y, startPos.y),
                            Mathf.Max(endPos.x, startPos.x) - Mathf.Min(endPos.x, startPos.x),
                            Mathf.Max(endPos.y, startPos.y) - Mathf.Min(endPos.y, startPos.y)
                            );

            GUI.Box(rect, "");

            for (int j = 0; j < allowedSelectObj.Count; j++)
            {
                // трансформируем позицию объекта из мирового пространства, в пространство экрана
                Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
                    Screen.height - Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);

                if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
                {
                    if (selectedObjects.Count == 0)
                    {
                        selectedObjects.Add(allowedSelectObj[j]);
                    }
                    else if (!CheckUnit(allowedSelectObj[j]))
                    {
                        selectedObjects.Add(allowedSelectObj[j]);
                    }
                }
            }
        }
    }

    private bool CheckPosition(Vector2 startPos, Vector2 endPos)
    {
        if(startPos.y > minY)
        {
            return true;
        }
        return false;
    }
}