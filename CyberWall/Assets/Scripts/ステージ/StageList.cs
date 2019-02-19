using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageList : SingletonMono<StageList>
{

    [SerializeField]
    private GameObject[] boxStage;
    [SerializeField]
    private GameObject[] tubeStage;
    [SerializeField]
    private GameObject[] fallStage;
    [SerializeField]
    private GameObject energyItem;
    [SerializeField]
    private GimicList[] gimicSet;

    [SerializeField]
    private GameObject[] debugStage;

    public GameObject[] BoxStage
    {
        get { return boxStage; }
    }
    public GameObject[] TubeStage
    {
        get { return tubeStage; }
    }
    public GameObject[] FallStage
    {
        get { return fallStage; }
    }
    public GameObject EnergyItem
    {
        get { return energyItem; }
    }
    public GameObject[] DebugStage
    {
        get { return debugStage; }
    }
}
