﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private GameObject gameObject;          //プレイヤーオブジェクト
    private PlayerMover playerMover;        //プレイヤーの動き
    private PlayerInput playerInput;        //プレイヤーの入力
    private PlayerGravity playerGravity;    //プレイヤーの重力
    private PlayerAnimator playerAnimator;  //プレイヤーのアニメーション
    private PlayerParameter parameter;      //パラメータ
    private GameObject speedEffectObject;

    private float plusTimeUpSpeed = 0;
    private float maxPlusSpeed = 0;
    private float minusMaxSpeed = 0;

    public PlayerController(PlayerParameter playerParameter, GameObject speedEffectObj = null)
    {
        parameter = playerParameter;
        playerMover = new PlayerMover(parameter.StartAccelSpeed);
        playerInput = new PlayerInput();
        playerGravity = new PlayerGravity();
        playerAnimator = new PlayerAnimator();
        gameObject = PlayerObjectManager.Instance.PlayerObject;
        if(speedEffectObj != null)
        {
            speedEffectObject = speedEffectObj;
        }
    }
    /// <summary>
    /// 左右移動入力
    /// </summary>
    public void MoveLRInput()
    {
        if (maxPlusSpeed < -50)
        {
            return;
        }
        playerMover.PlayerTilt(parameter.TiltSpeed, parameter.TiltReturnSpeed, playerInput.InputHorizontal());
    }
    //両方入力対応
    public void MoveInput()
    {
        if(maxPlusSpeed<-50)
        {
            return;
        }
        playerInput.InputVertical();
        playerInput.InputHorizontal();
        playerMover.PlayerTilt(parameter.TiltSpeed, parameter.TiltReturnSpeed, playerInput.inputX,playerInput.inputY);

    }
    /// <summary>
    /// 左右移動(RB)
    /// </summary>
    public void MoveLRRb()
    {
        if (playerGravity.isGround)
        {
            playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed + Mathf.Clamp((((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.RightLeftSpeed), 0, parameter.RightLeftSpeed));
        }
        else
        {
            playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftAirSpeed);
        }
    }
    //チューブ版
    public void MoveLRRbTube()
    {
        if (playerGravity.isGround)
        {
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftSpeed+Mathf.Clamp( (((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.RightLeftSpeed),0,parameter.RightLeftSpeed), playerGravity.nomalVector);
        }
        else
        {
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftAirSpeed, playerGravity.nomalVector);
        }
    }
    public void MoveLRFB()
    {
        playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed + Mathf.Clamp((((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.RightLeftSpeed), 0, parameter.RightLeftSpeed));
        playerMover.MoveControlRbFB(playerInput.inputY, parameter.RightLeftSpeed + Mathf.Clamp((((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.RightLeftSpeed), 0, parameter.RightLeftSpeed));
    }
    /// <summary>
    /// 前移動(RB)
    /// </summary>
    public void MoveStraight()
    {
        playerMover.MoveStraightRb(playerMover.acceleSpeed);
    }
    public void MoveDown()
    {
        playerMover.MoveDown(playerMover.acceleSpeed);
    }
    //重力変更
    public void GravityRotation()
    {
        GravityMode gravityMode = playerInput.InputGravityChange(playerGravity.mode);
        //入力されてなければそのまま
        if (gravityMode == playerGravity.mode||!(playerGravity.isGround))
        {
            playerGravity.GravityChange(playerGravity.mode);
            return;
        }
        SoundManager.Instance.ChangeGravityInput();//重力変更音
        playerGravity.GravityChange(gravityMode);
        playerMover.JumpRb(parameter.GravityChangeJumpPower);
        playerGravity.isGround = false;
        playerAnimator.PlayJump();
    }
    public void GravityRotationNoInput()
    {
        playerGravity.GravityChange(playerGravity.mode);
    }
    public void GravityRotationTube()
    {
        playerGravity.GravityChangeTube();
    }
    public void GravityRotationFall()
    {
        playerGravity.GravityChange(playerGravity.mode);
    }
    //時間でスピードアップ
    public void SpeedUpTime()
    {
        playerMover.UpSpeed((parameter.UpSpeed +plusTimeUpSpeed) * Time.deltaTime * 60, parameter.MaxSpeed + maxPlusSpeed);
        playerMover.MinusOverSpeed(5 * Time.deltaTime);
        playerMover.SpeedOnObject(parameter.SpeedOnEffectSpeed, SpeedTrigger.OnObject, speedEffectObject);
    }
    //重力
    public void GravityRb()
    {
        if (maxPlusSpeed < -50)
        {
            return;
        }
        playerGravity.GravityRb(parameter.StartGravitySpeed + Mathf.Clamp((((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.StartGravitySpeed), 0, parameter.StartGravitySpeed), parameter.GravitySpeed + Mathf.Clamp((((GetAccelSpeed() - parameter.MaxSpeed) / parameter.MaxSpeed) * parameter.GravitySpeed), 0, parameter.GravitySpeed));
        if (playerGravity.OnGround(parameter.RadGround, parameter.RayRangeGround, parameter.HitLayerGround))
        {
            playerAnimator.PlayRunBoad();
            playerAnimator.StopJump();
        }
    }
    //ジャンプ処理
    public void JumpRb()
    {
        //TODO:
        if (playerGravity.isGround)
        {
            if (playerInput.InputJump())
            {
                playerMover.JumpRb(parameter.JumpPower);
                playerGravity.isGround = false;
                playerAnimator.PlayJump();
            }
        }
    }
    //まとめて使う用
    public void Move()
    {
        MoveLRInput();
        GravityRotation();
        SpeedUpTime();
        MoveStraight();
        MoveLRRb();
        GravityRb();
        JumpRb();
    }
    public void MoveTube()
    {
        MoveLRInput();
        GravityRotationTube();
        SpeedUpTime();
        MoveStraight();
        MoveLRRbTube();
        GravityRb();
    }
    public void MoveFall()
    {
        MoveInput();
        GravityRotationFall();
        SpeedUpTime();
        MoveDown();
        MoveLRFB();
    }
    public void MoveFallPrev()
    {
        MoveLRInput();
        GravityRotationTube();
        SpeedUpTime();
        MoveStraight();
        MoveLRRb();
        GravityRb();
        JumpRb();
    }

    public void PlusSpeedChange(float speed)
    {
        playerMover.PlusSpeedChange(speed);
    }
    public void SaveSpeedChange(float speed)
    {
        playerMover.SaveAndSpeedChangeDirect(speed);
    }
    public void SaveSpeedReflection()
    {
        playerMover.SaveSpeedReflection();
    }
    public void ChangeGravityModeNomal(GravityMode gravityMode)
    {
        playerGravity.mode = gravityMode;
    }
    public float GetAccelSpeed()
    {
        return playerMover.acceleSpeed;
    }
    public void plusUpSpeedAndMax(float up,float max)
    {
        plusTimeUpSpeed += up;
        
        maxPlusSpeed += max;
        if(maxPlusSpeed>parameter.MaxSpeed)
        {
            maxPlusSpeed = parameter.MaxSpeed;
        }
        if(plusTimeUpSpeed>4)
        {
            plusTimeUpSpeed = 3;
        }
    }
    public float GetMaxSpeed()
    {
        return parameter.MaxSpeed + maxPlusSpeed;
    }
    //最初の加速
    public void PlusOverSpeed(float speed)
    {
        playerMover.PlusOverSpeed(speed);
    }
}
