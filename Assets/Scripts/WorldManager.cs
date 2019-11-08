using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    private HumanWarrior humanWarrior;

    public static BottomPanel bottomPanel;
    public static Character character;
    public static SelectObjects selectObjects;
    public static float startPositionY = 5.5f;
    public static float minPositionY = -0.1f;
    public static float deltaPositionY = 0.1f;


    void Awake()
    {
        bottomPanel = (BottomPanel)FindObjectOfType(typeof(BottomPanel));
        character = (Character)FindObjectOfType(typeof(Character));
        selectObjects = (SelectObjects)FindObjectOfType(typeof(SelectObjects));
        
    }
    void Start()
    {
        SetSettings();
        CreateHumanWarrior();
    }



    void Update()
    {
        
    }
    private void SetSettings()
    {
        RectTransform rt = bottomPanel.GetComponent(typeof(RectTransform)) as RectTransform;
        selectObjects.SetForbiddenPosition(rt.offsetMax);
    }

    public void CreateHumanWarrior()
    {

        GameObject prefab = HumanWarrior.GetPrefab();
        if (prefab != null)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 position = transform.position;
                position.x += i * 1.0f;
                position.y = 5f;
                GameObject prefabWrapper =  Instantiate(prefab, position, Quaternion.identity);
                HumanWarrior warrion = prefabWrapper.GetComponent<HumanWarrior>();

               // warrion.NewPosition = position +  new Vector3(i * 1.0f, 0, i * 1.0f);
                warrion.NewPosition = new Vector3(10f, 5f, 10f);
                //SelectObjects.unit.Add(warrion.gameObject);
            }
        }
    }
}
