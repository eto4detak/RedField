﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWarrior : Unit
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RunToPoint();
    }
    public static GameObject GetPrefab()
    {
        if (prefab == null)
        {
            prefab = GameObject.Find("HumanWarrior");
        }
        return prefab;
    }




}
