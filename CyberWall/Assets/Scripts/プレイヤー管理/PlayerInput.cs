using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float inputX;
    public float inputY;
    public bool androidFlag = false;

    //移動入力メソッド
    public float InputHorizontal()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        //android操作

        //float a =
        //(Quaternion.Euler(0, 0, -180) * Quaternion.Euler(-90, 0, 0) * Input.gyro.attitude * Quaternion.Euler(0, 0, 180)).eulerAngles.z;
        //if (a > 180)
        //{
        //    a -= 360;
        //}
        //if (Mathf.Abs(a) > 45)
        //{
        //    a = 45 * Mathf.Sign(a);
        //}
        //a /= 45;
        //inputX = -a;
        return inputX;
    }
    public float InputVertical()
    {
        //        float a =
        //(Quaternion.Euler(0, 0, -180) * Quaternion.Euler(-35, 0, 0) * Input.gyro.attitude * Quaternion.Euler(0, 0, 180)).eulerAngles.y;
        //        if (a > 180)
        //        {
        //            a -= 360;
        //        }
        //        if (Mathf.Abs(a) > 45)
        //        {
        //            a = 45 * Mathf.Sign(a);
        //        }
        //        a /= 45;
        //        inputY = a;
        inputY = Input.GetAxisRaw("Vertical");
        return inputY;
    }
    //キーが押されれば、正負を返す
    public bool InputJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")||TapUp())
        {
            return true;
        }
        return false;
    }
    //キーが押されれば、その次の左右上下を返す
    public GravityMode InputGravityChange(GravityMode mode)
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetButtonDown("GravityChangeL") || RightTap())
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
        if (Input.GetKeyDown(KeyCode.J) || Input.GetButtonDown("GravityChangeR") || LeftTap())
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
    private bool RightTap()
    {
        // タッチされているかチェック
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);
            if (touch.position.x >= Screen.width / 2)
            {
                if (touch.position.y <= Screen.height / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool LeftTap()
    {
        // タッチされているかチェック
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);
            if (touch.position.x <= Screen.width / 2)
            {
                if (touch.position.y <= Screen.height / 2)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool TapUp()
    {
        // タッチされているかチェック
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);
            if (touch.position.y >= Screen.height / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

