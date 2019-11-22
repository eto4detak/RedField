using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    public static List<Unit> allowedSelectObj = new List<Unit>(); // массив всех юнитов, которых мы можем выделить
    public static List<Unit> selectedObjects; // выделенные объекты

    public GUISkin skin;
    private Rect rect;
    private bool draw;
    private Vector2 startPos;
    private Vector2 endPos;
    private float minY = 0;

    void Awake()
    {
        selectedObjects = new List<Unit>();
    }

    private void Update()
    {
        foreach (var selected in selectedObjects)
        {
            Debug.Log(selected.name + " command = " + selected.command);
        }
    }


    void OnGUI()
    {
        TryDrawSelectBox();
    }

    private void TryDrawSelectBox()
    {
        GUI.skin = skin;
        GUI.depth = 99;
        if (Input.GetMouseButtonDown(0))
        {
           // Deselect();
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
            endPos = Input.mousePosition;
            if (startPos == endPos) return;
            endPos = GetAvailablePosition(endPos);
            selectedObjects.Clear();

            DrawSelectBox();
        }
    }

    private void DrawSelectBox()
    {
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


    // проверка, добавлен объект или нет
    bool CheckUnit(Unit unit)
    {
        bool result = false;
        foreach (Unit u in selectedObjects)
        {
            if (u == unit) result = true;
        }
        return result;
    }


    public static void SetAllowed(Unit obj)
    {
        allowedSelectObj.Add(obj);
    }

    private static void Select()
    {
        if (selectedObjects.Count > 0)
        {
            for (int j = 0; j < selectedObjects.Count; j++)
            {
                Unit unit = selectedObjects[j];
                if (unit != null)
                {
                    HighlightManager.HighlightUnit(unit);
                }
            }
        }
    }

    public static void SelectUnit(Unit unit)
    {
        selectedObjects.Add(unit);
        Select();
    }
    //public static void DeselectUnit(Unit unit)
    //{
    //    selectedObjects.Remove(unit);
    //}

    public static void Deselect()
    {
        selectedObjects.Clear();
        HighlightManager.Clear();
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

    public void SetSelected(Rect rect)
    {
        for (int j = 0; j < allowedSelectObj.Count; j++)
        {
            // трансформируем позицию объекта из мирового пространства, в пространство экрана
            Vector2 tmp1 = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
                Screen.height - Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);
            Vector2 tmp = new Vector2(Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).x,
                Camera.main.WorldToScreenPoint(allowedSelectObj[j].transform.position).y);

            if (rect.Contains(tmp)) // проверка, находится-ли текущий объект в рамке
            {
                if (selectedObjects.Count == 0)
                {
                    Debug.Log("allowedSelectObj");

                    selectedObjects.Add(allowedSelectObj[j]);
                }
                else if (!CheckUnit(allowedSelectObj[j]))
                {
                    Debug.Log("!CheckUnit(");

                    selectedObjects.Add(allowedSelectObj[j]);
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

    public static void ClearSelected()
    {
        selectedObjects.Clear();
        HighlightManager.Clear();
    }

}