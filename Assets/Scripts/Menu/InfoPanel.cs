using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InfoPanel : MonoBehaviour
{
    public GameObject frontImg;
    public GameObject targetImg;
    public GameObject targetList;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public static Sprite GetFrontImage<T>(T obj) where T : class
    {
        if(obj is UnitGroup) {
            return Resources.Load<Sprite>("Sprite/SqareUnit");
        }
        return null;
    }

    public void SelectUnit<T>(T obj) where T : class
    {
        if (obj is UnitGroup)
        {
            frontImg.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprite/sqareUnit");
        }
    }
    public void EmptyPanel<T>(T obj) where T : class
    {
        if (obj is UnitGroup)
        {
            frontImg.GetComponent<Button>().image.sprite = null;
        }
    }

}
