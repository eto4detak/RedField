using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static List<UnitGroup> allGroups = new List<UnitGroup>();
    public static Character character;
    public static float deltaPositionY = 0.1f;
    public static int groupCount = 0;
    private HumanWarrior humanWarrior;
    public static InfoPanel infoPanel;
    public static float minPositionY = -0.1f;
    public static PlayerInputHandler playerInputHandler;
    public static RunUnitManager runUnitManager;
    public static float startPositionY = 5.5f;
    public static SelectObjects selectObjects;


    void Awake()
    {
        infoPanel = (InfoPanel)FindObjectOfType(typeof(InfoPanel));
        character = (Character)FindObjectOfType(typeof(Character));
        selectObjects = (SelectObjects)FindObjectOfType(typeof(SelectObjects));
        runUnitManager = (RunUnitManager)FindObjectOfType(typeof(RunUnitManager));
        playerInputHandler = (PlayerInputHandler)FindObjectOfType(typeof(PlayerInputHandler));
        playerInputHandler = (PlayerInputHandler)FindObjectOfType(typeof(PlayerInputHandler));
    }



    void Start()
    {
        SetSettings();

        StartWorld();
        
       // CreateHumanWarrior();
    }



    void Update()
    {
        
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

                warrion.NewPosition = new Vector3(10f, 5f, 10f);
            }
        }
    }

    private void SetSettings()
    {
        RectTransform rt = infoPanel.GetComponent(typeof(RectTransform)) as RectTransform;
        selectObjects.SetForbiddenPosition(rt.offsetMax);
    }


    private void StartWorld()
    {
        
        UnitGroup.CreateUnits(new Vector3(40, 7f, 20));
        UnitGroup.CreateUnits(new Vector3(40, 5.6f, 40));
        UnitGroup.CreateUnits(new Vector3(60, 5.6f, 40));

    }

}
