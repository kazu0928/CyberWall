using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover
{
    /// <summary>
    /// 入力時にプレイヤーに角度をつける(傾ける)
    /// </summary>
    /// <param name="tiltReturnSpeed">戻る速さ</param>
    /// <param name="inputX">入力値</param>
    public void PlayerTilt(float tiltSpeed, float tiltReturnSpeed, float inputX)
    {
        //角度付け用のオブジェクトを取得
        Transform transform = PlayerObjectManager.Instance.TiltObject.transform;
        //現在の傾き角度取得
        float z = transform.localRotation.eulerAngles.z;
        Debug.Log(z);
        //プレイヤーの傾き処理
        transform.localRotation = Quaternion.Euler(0, 0, z - inputX * Time.deltaTime * 60 * tiltSpeed);
        //傾きすぎの防止処理
        z = transform.localRotation.eulerAngles.z;
        if (z > 180)
        {
            z -= 360;//0～180、0～-180にする
        }
        if (z > 20)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 20);
        }
        else if (z < -20)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -20);
        }
        //傾きを戻す処理
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Sign(z) * (Mathf.Clamp(Mathf.Abs(z) - tiltReturnSpeed * Time.deltaTime * 60, 0, 20)));
    }

}
