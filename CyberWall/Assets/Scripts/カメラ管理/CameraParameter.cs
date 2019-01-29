using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラ固定する座標
public enum CameraFixed
{
    XFixed,
    YFixed,
    ZFixed,
}

[CreateAssetMenu(menuName = "Parameter/CameraParameter")]
public class CameraParameter : ScriptableObject
{
    //メインカメラ初期値
    [SerializeField]
    private float distance;         //距離
    [SerializeField]
    private Vector2 lookAngles;     //角度
    [SerializeField]
    private Vector3 offsetPos;
    [SerializeField]
    private Vector3 plusTargetPos;
    [SerializeField]
    private float trackingSpeed;
    [SerializeField]
    private float maxRotation;
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private CameraMode cameraMode;
    [SerializeField]
    private CameraFixed[] cameraFixed;
    [SerializeField]
    private bool leapFlag;
    [SerializeField]
    private float yPoint;
    public float YPoint
    {
        get { return yPoint; }
    }
    public float Distance
    {
        get { return distance; }
    }
    public Vector2 LookAngles
    {
        get { return lookAngles; }
    }
    public Vector3 OffsetPos
    {
        get { return offsetPos; }
    }
    public Vector3 PlusTargetPos
    {
        get { return plusTargetPos; }
    }
    public float TrackingSpeed
    {
        get { return trackingSpeed; }
    }
    public float MaxRotation
    {
        get { return maxRotation; }
    }
    public float CameraSpeed
    {
        get { return cameraSpeed; }
    }
    public CameraMode CameraMode
    {
        get { return cameraMode; }
    }
    public CameraFixed[] CameraFixedPos
    {
        get { return cameraFixed; }
    }
    public bool LeapFlag
    {
        get { return leapFlag; }
    }
}
