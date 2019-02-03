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
}
