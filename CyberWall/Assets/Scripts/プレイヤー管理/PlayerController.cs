using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private GameObject gameObject;
    private PlayerMover playerMover;
    private PlayerInput playerInput;
    private PlayerParameter parameter;
    public PlayerController(PlayerParameter playerParameter)
    {
        playerMover = new PlayerMover();
        playerInput = new PlayerInput();
        parameter = playerParameter;
        gameObject = PlayerObjectManager.Instance.PlayerObject;
    }
    public void PlayerMoveLeftRight()
    {
        playerMover.PlayerTilt(parameter.TiltSpeed, parameter.TiltReturnSpeed, playerInput.InputHorizontal());
    }
}
