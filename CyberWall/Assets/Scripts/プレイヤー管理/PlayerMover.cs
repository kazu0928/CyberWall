using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpeedTrigger
{
    OnObject,
}

public class PlayerMover
{
    public float acceleSpeed = 50;  //現在の移動速度
    public float saveAcceleSpeed = 50;
    public float plusSpeed;
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PlayerMover(float acSpeed)
    {
        acceleSpeed = acSpeed;
    }
    /// <summary>
    /// 通常時のまっすぐな移動
    /// </summary>
    public void MoveStraightRb(float acceleSpeed)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        rb.AddForce((PlayerObjectManager.Instance.PlayerObject.transform.forward * acceleSpeed - rb.velocity) * Time.deltaTime * 60);
    }
    public void MoveDown(float accelSpeed)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        rb.AddForce((Vector3.down * acceleSpeed - rb.velocity) * Time.deltaTime * 60);
    }
    /// <summary>
    /// 左右移動
    /// </summary>
    public void MoveControlRb(float inputX, float RightLeftSpeed)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        //rb.AddTorque((PlayerObjectManager.Instance.PlayerObject.transform.up * inputX * RightLeftSpeed ) * Time.deltaTime * 60);
        rb.AddForce((PlayerObjectManager.Instance.PlayerObject.transform.right * inputX * RightLeftSpeed-rb.velocity)* Time.deltaTime * 60);
    }
    // 前後移動
    public void MoveControlRbFB(float inputY, float RightLeftSpeed)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        rb.AddForce((PlayerObjectManager.Instance.PlayerObject.transform.forward * inputY * RightLeftSpeed-rb.velocity)* Time.deltaTime * 60);      
    }
    public void MoveControlRbTube(float inputX, float RightLeftSpeed, Vector3 nomalVector)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        rb.AddForce((Vector3.ProjectOnPlane(PlayerObjectManager.Instance.PlayerObject.transform.right, nomalVector) * inputX * RightLeftSpeed - rb.velocity) * Time.deltaTime * 60);
        //rb.AddTorque((PlayerObjectManager.Instance.PlayerObject.transform.up * inputX * RightLeftSpeed) * Time.deltaTime * 60);
    }
    /// <summary>
    /// ジャンプ
    /// </summary>
    public void JumpRb(float jumpPower)
    {
        Vector3 jumpSpeed = PlayerObjectManager.Instance.PlayerObject.transform.up * jumpPower;
        PlayerObjectManager.Instance.rb.AddForce(jumpSpeed, ForceMode.Impulse);
    }
    /// <summary>
    /// 入力時にプレイヤーに角度をつける(傾ける)
    /// </summary>
    /// <param name="inputX">入力値</param>
    public void PlayerTilt(float tiltSpeed, float tiltReturnSpeed, float inputX, float inputY = 0)
    {
        //角度付け用のオブジェクトを取得
        Transform transform = PlayerObjectManager.Instance.TiltObject.transform;
        float x = transform.localRotation.eulerAngles.x;
        //現在の傾き角度取得
        float z = transform.localRotation.eulerAngles.z;
        //プレイヤーの傾き処理
        transform.localRotation = Quaternion.Euler(x, 0, z - inputX * Time.deltaTime * 60 * tiltSpeed);
        //傾きすぎの防止処理
        z = transform.localRotation.eulerAngles.z;
        if (z > 180)
        {
            z -= 360;//0～180、0～-180にする
        }
        if (z > 20)
        {
            transform.localRotation = Quaternion.Euler(x, 0, 20);
        }
        else if (z < -20)
        {
            transform.localRotation = Quaternion.Euler(x, 0, -20);
        }
        //傾きを戻す処理
        transform.localRotation = Quaternion.Euler(x, 0, Mathf.Sign(z) * (Mathf.Clamp(Mathf.Abs(z) - tiltReturnSpeed * Time.deltaTime * 60, 0, 20)));
        z = transform.localRotation.eulerAngles.z;

        //現在の傾き角度取得
        x = transform.localRotation.eulerAngles.x;
        //プレイヤーの傾き処理
        transform.localRotation = Quaternion.Euler(x + inputY * Time.deltaTime * 60 * tiltSpeed, 0, z);
        //傾きすぎの防止処理
        x = transform.localRotation.eulerAngles.x;
        if (x > 180)
        {
            x -= 360;//0～180、0～-180にする
        }
        if (x > 20)
        {
            transform.localRotation = Quaternion.Euler(20, 0, z);
        }
        else if (x < -20)
        {
            transform.localRotation = Quaternion.Euler(-20, 0, z);
        }
        //傾きを戻す処理
        transform.localRotation = Quaternion.Euler(Mathf.Sign(x) * (Mathf.Clamp(Mathf.Abs(x) - tiltReturnSpeed * Time.deltaTime * 60, 0, 20)), 0,z);

    }
    /// <summary>
    /// スピードアップ
    /// </summary>
    public void UpSpeed(float upSpeed, float maxSpeed)
    {
        acceleSpeed += upSpeed;
        if (acceleSpeed > maxSpeed + plusSpeed)
        {
            acceleSpeed = maxSpeed + plusSpeed;
        }
    }
    /// <summary>
    /// 追加スピードアップ分のマイナス
    /// </summary>
    public void MinusOverSpeed(float speed)
    {
        if (plusSpeed > 0)
        {
            plusSpeed -= speed;
        }
        if (plusSpeed < 0)
        {
            plusSpeed = 0;
        }
    }
    /// <summary>
    /// 一定の速度以上でなんかする
    /// 今は集中線
    /// </summary>
    public void SpeedOnObject(float speed,SpeedTrigger speedTrigger, GameObject obj = null)
    {
        switch (speedTrigger)
        {
            case SpeedTrigger.OnObject:
                if (obj == null)
                {
                    return;
                }
                if (acceleSpeed > speed)
                {
                    obj.SetActive(true);
                }
                else
                {
                    obj.SetActive(false);
                }
                break;
        }
    }
    //ここから他クラスからの呼び出し
    ///////////////////////////////////////////////////////////////////
    //速度を追加する（プラマイ）
    public void PlusSpeedChange(float speed)
    {
        acceleSpeed += speed;
        PlayerObjectManager.Instance.rb.velocity = Vector3.zero;
        if (acceleSpeed < 3)
        {
            acceleSpeed = 3;
        }
        PlayerObjectManager.Instance.rb.AddForce(PlayerObjectManager.Instance.PlayerObject.transform.forward * acceleSpeed, ForceMode.Impulse);
    }
    public void SaveAndSpeedChangeDirect(float speed)
    {
        saveAcceleSpeed = acceleSpeed;
        acceleSpeed = speed;
        PlayerObjectManager.Instance.rb.velocity = Vector3.zero;
        PlayerObjectManager.Instance.rb.AddForce(PlayerObjectManager.Instance.PlayerObject.transform.forward * acceleSpeed, ForceMode.Impulse);
    }
    public void SaveSpeedReflection()
    {
        acceleSpeed = saveAcceleSpeed;
    }
}
