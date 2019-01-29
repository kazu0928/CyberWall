using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parameter/PlayerParameter")]
public class PlayerParameter : ScriptableObject
{
    [SerializeField]
    private GameObject playerObj;       //プレイヤーオブジェクト
    [SerializeField]
    private float startAccelSpeed;      //最初の移動速度
    [SerializeField]
    private float rightLeftSpeed;       //左右移動速度
    [SerializeField]
    private float upSpeed;              //上昇速度
    [SerializeField]
    private float maxSpeed;             //最高速度
    [SerializeField]  
    private float tiltSpeed;            //傾き速度
    [SerializeField][Range(0, 100)]
    private float tiltReturnSpeed;      //傾きが戻る速度
    //重力
    [SerializeField]
    private float rayRangeGround;       //接地判定のレイの長さ
    [SerializeField]
    private int hitLayerGround;         //当たる地面レイヤー
    [SerializeField]
    private float radGround = 0.7f;     //半径
    [SerializeField]
    private float gravitySpeed = 500;   //重力速度
    [SerializeField]
    private float startGravitySpeed;    //重力初速

    [SerializeField]
    private GameObject speedEffetObj = null;   //集中線のオブジェクト
    [SerializeField]
    private float speedOnEffectSpeed;   //集中線の出る速度

    //getter
    public GameObject PlayerObj
    {
        get { return playerObj; }
    }
    public float StartAccelSpeed
    {
        get { return startAccelSpeed; }
    }
    public float RightLeftSpeed
    {
        get { return rightLeftSpeed; }
    }
    public float UpSpeed
    {
        get { return upSpeed; }
    }
    public float MaxSpeed
    {
        get { return maxSpeed; }
    }
    public float TiltSpeed
    {
        get { return tiltSpeed; }
    }
    public float TiltReturnSpeed
    {
        get { return tiltReturnSpeed; }
    }
    public float RayRangeGround
    {
        get { return rayRangeGround; }
    }
    public LayerMask HitLayerGround
    {
        get { return hitLayerGround; }
    }
    public float RadGround
    {
        get { return radGround; }
    }
    public float GravitySpeed
    {
        get { return gravitySpeed; }
    }
    public float StartGravitySpeed
    {
        get { return startGravitySpeed; }
    }


    public GameObject SpeedEffectObject
    {
        get { return speedEffetObj; }
    }
    public float SpeedOnEffectSpeed
    {
        get { return speedOnEffectSpeed; }
    }
}
