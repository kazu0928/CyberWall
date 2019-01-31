using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float inputX;
    public float inputY;

    //移動入力メソッド
    public float InputHorizontal()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        return inputX;
    }
    public float InputVertical()
    {
        inputY = Input.GetAxisRaw("Vertical");
        return inputY;
    }
    //キーが押されれば、正負を返す
    public bool InputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            return true;
        }
        return false;
    }
    //キーが押されれば、その次の左右上下を返す
    public GravityMode InputGravityChange(GravityMode mode)
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            switch (mode)
            {
                case GravityMode.Up:
                    return GravityMode.Left;
                case GravityMode.Right:
                    return GravityMode.Up;
                case GravityMode.Left:
                    return GravityMode.Down;
                case GravityMode.Down:
                    return GravityMode.Right;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            switch (mode)
            {
                case GravityMode.Up:
                    return GravityMode.Right;
                case GravityMode.Right:
                    return GravityMode.Down;
                case GravityMode.Left:
                    return GravityMode.Up;
                case GravityMode.Down:
                    return GravityMode.Left;
            }
        }
        return mode;
    }
}

