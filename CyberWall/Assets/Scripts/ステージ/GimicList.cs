﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GimicList
{
    [SerializeField]
    private GameObject[] gimicSet;
    public GameObject[] GimicSet
    {
        get { return gimicSet; }
    }
}
