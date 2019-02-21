using System.Collections;
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
        playerMover.PlayerTilt(parameter.TiltSpeed, parameter.TiltReturnSpeed, playerInput.InputHorizontal());
    }
    //両方入力対応
    public void MoveInput()
    {
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
            playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed + plusTimeUpSpeed * 20);
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
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftSpeed+plusTimeUpSpeed*20, playerGravity.nomalVector);
        }
        else
        {
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftAirSpeed, playerGravity.nomalVector);
        }
    }
    public void MoveLRFB()
    {
        playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed + plusTimeUpSpeed * 20);
        playerMover.MoveControlRbFB(playerInput.inputY, parameter.RightLeftSpeed + plusTimeUpSpeed * 20);
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
        playerGravity.GravityRb(parameter.StartGravitySpeed, parameter.GravitySpeed);
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
        if(maxPlusSpeed>120)
        {
            maxPlusSpeed = 120;
        }
        if(plusTimeUpSpeed>4)
        {
            plusTimeUpSpeed = 3;
        }
    }
}
