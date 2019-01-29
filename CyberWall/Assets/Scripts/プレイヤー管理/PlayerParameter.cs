using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/PlayerParameter")]
public class PlayerParameter : ScriptableObject
{
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]  
    private float tiltSpeed;
    [SerializeField]
    [Range(0, 100)]
    private float tiltReturnSpeed;
    //getter
    public GameObject PlayerObj
    {
        get { return playerObj; }
    }
    public float TiltSpeed
    {
        get { return tiltSpeed; }
    }
    public float TiltReturnSpeed
    {
        get { return tiltReturnSpeed; }
    }
}
