using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HighlightManager
{
    private static List<GameObject> highlightedObjects = new List<GameObject>();
    public HighlightManager()
    {

    }


    public static void Clear()
    {
        if(highlightedObjects.Count > 0)
        {
            SetStandart(highlightedObjects);
            highlightedObjects.Clear();
        }
    }

    public static void HighlightUnit(Unit unit)
    {
        Renderer render = unit.gameObject.GetComponentInChildren<Renderer>();
        if(render != null)
        {
            foreach (var material in render.materials)
            {
                //material.shader = Shader.Find("Skybox/Procedural");
                material.shader = Shader.Find("GUI/Text Shader");
                highlightedObjects.Add(unit.gameObject);
            }
        }
    }

    public static void ClearHighlightUnits(IList<Unit> units)
    {
        foreach (var unit in units)
        {
            Renderer render = unit.gameObject.GetComponentInChildren<Renderer>();
            if (render != null)
            {
                foreach (var material in render.materials)
                {
                    material.shader = Shader.Find("Standard");
                }
            }
        }
    }

    public static void SetStandart(IList<GameObject> gameList)
    {
        foreach (var gObject in gameList)
        {
            Renderer render = gObject.GetComponentInChildren<Renderer>();
            if (render != null)
            {
                foreach (var material in render.materials)
                {
                    material.shader = Shader.Find("Standard");
                }
            }
        }
    }
}