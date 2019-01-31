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
    //左右移動入力
    public void MoveLRInput()
    {
        playerMover.PlayerTilt(parameter.TiltSpeed, parameter.TiltReturnSpeed, playerInput.InputHorizontal());
    }
    //左右移動(RB)
    public void MoveLRRb()
    {
        if (playerGravity.isGround)
        {
            playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed);
        }
        else
        {
            playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftAirSpeed);
        }
    }
    public void MoveLRRbTube()
    {
        if (playerGravity.isGround)
        {
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftSpeed, playerGravity.nomalVector);
        }
        else
        {
            playerMover.MoveControlRbTube(playerInput.inputX, parameter.RightLeftAirSpeed, playerGravity.nomalVector);
        }
    }
    //前移動(RB)
    public void MoveStraight()
    {
        playerMover.MoveStraightRb(playerMover.acceleSpeed);
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
        playerGravity.GravityChange(gravityMode);
        playerMover.JumpRb(parameter.GravityChangeJumpPower);
        playerGravity.isGround = false;
        playerAnimator.PlayJump();
    }
    public void GravityRotationTube()
    {
        playerGravity.GravityChangeTube();
    }
    //時間でスピードアップ
    public void SpeedUpTime()
    {
        playerMover.UpSpeed(parameter.UpSpeed * Time.deltaTime * 60, parameter.MaxSpeed);
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

    public void PlusSpeedChange(float speed)
    {
        playerMover.PlusSpeedChange(speed);
    }
}
