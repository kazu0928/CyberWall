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
        playerMover.MoveControlRb(playerInput.inputX, parameter.RightLeftSpeed);
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
        if (gravityMode == playerGravity.mode)
        {
            playerGravity.GravityChange(playerGravity.mode);
            return;
        }
        playerGravity.GravityChange(gravityMode);
        playerMover.JumpRb(60);
        playerGravity.isGround = false;
        playerAnimator.PlayJump();
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
}
