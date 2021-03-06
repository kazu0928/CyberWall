﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    //プレイヤーオブジェクト
    private GameObject playerObj = null;
    public GameObject PlayerObject
    {
        get { return playerObj; }
    }
    //
    [SerializeField]
    private PlayerParameter parameter;
    public PlayerParameter PlayerParam
    {
        get { return parameter; }
    }
    private PlayerController controller;
    private GameObject speedEffectObject;
    //画面効果
    private GlitchFx fx;
    //現在のステージが何か
    [SerializeField]
    private StageMode nowStageMode;
    public StageMode NowStageMode
    {
        get { return nowStageMode; }
    }
    public bool fallPrev = false;
    //初期化
    void Start()
    {
        fallPrev = false;
        playerObj = Instantiate(parameter.PlayerObj, transform.position, transform.rotation).gameObject;
        fx = Camera.main.gameObject.GetComponent<GlitchFx>();
        //プレイヤーにイベント用のスクリプトをアタッチ
        playerObj.AddComponent<PlayerOnEvent>();
        PlayerObjectManager.Instance.init();
        if (parameter.SpeedEffectObject != null)
        {
            speedEffectObject = Instantiate(parameter.SpeedEffectObject, new Vector3(100, 100, -500), parameter.SpeedEffectObject.transform.rotation);
            controller = new PlayerController(parameter, speedEffectObject);
            return;
        }
        controller = new PlayerController(parameter);
    }
    private void Update()
    {
        if (PlayerObjectManager.Instance.PlayerObject == null)
        {
            return;
        }
        if (nowStageMode == StageMode.Tube)
        {
            controller.MoveTube();
        }
        else if(nowStageMode == StageMode.Nomal)
        {
            if (fallPrev==false)
            {
                controller.Move();
            }
            else
            {
                controller.MoveFallPrev();
            }
        }
        else if(nowStageMode == StageMode.Fall)
        {
            controller.MoveFall();
        }
    }
    //デバッグ用
    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(230,155,144,0.3f);
        Gizmos.DrawSphere(transform.position, 1);
        if (playerObj == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(playerObj.transform.position + playerObj.transform.up * 3 + -playerObj.transform.up * parameter.RayRangeGround, parameter.RadGround);
    }
    //--------------------
    //スピード変更（加算式）
    public void PlusSpeedChange(float speed)
    {
        controller.PlusSpeedChange(speed);
    }
    //ダメージエフェクト
    public void StartDamage()
    {
        StartCoroutine("damage");
    }
    public void ChangeStageMode(StageMode mode)
    {
        nowStageMode = mode;
    }
    public void SaveSpeedChange(float speed)
    {
        controller.SaveSpeedChange(speed);
    }
    public void SaveSpeedReflection()
    {
        controller.SaveSpeedReflection();
    }
    public void ChangeGravityModeNomal(GravityMode gravityMode)
    {
        controller.ChangeGravityModeNomal(gravityMode);
    }
    public float GetAccelSpeed()
    {
        return controller.GetAccelSpeed();
    }
    IEnumerator damage()
    {
        fx.enabled = true;
        yield return new WaitForSeconds(1);
        fx.enabled = false;
    }
    public void PlusUpSpeedAndMax(float up, float max)
    {
        controller.plusUpSpeedAndMax(up, max);
    }
    //加速
    public void PlusOverSpeed(float speed)
    {
        controller.PlusOverSpeed(speed);
    }
    public float GetMaxSpeed()
    {
       return controller.GetMaxSpeed();
    }
}
