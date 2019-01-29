//
// プレイヤーの重力系統を操るクラス
// 2019/01/29
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity
{
    private GameObject gameObject;

    public GravityMode mode;
    public bool isGround;
    public float time;

    //レイキャスト用
    private Vector3 nomalVector;    //地面の法線ベクトル
    private RaycastHit hitGround;   //スフィアキャストのヒット情報

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public PlayerGravity()
    {
        gameObject = PlayerObjectManager.Instance.PlayerObject;
        mode = GravityMode.Down;
        isGround = false;
        time = 0;
    }
    /// <summary>
    /// 重力
    /// </summary>
    public void GravityRb(float startGravitySpeed, float gravitySpeed)
    {
        Rigidbody rb = PlayerObjectManager.Instance.rb;
        if (!isGround)
        {
            time += Time.deltaTime;
        }
        //重力処理
        rb.AddForce(-gameObject.transform.up.normalized * startGravitySpeed * Time.deltaTime * 60 - gameObject.transform.up.normalized * time * Time.deltaTime * 60 * gravitySpeed, ForceMode.Acceleration);
        //TODO:OnTrigger的な処理の追加

        if (isGround)
        {
            time = 0;
        }
    }
    /// <summary>
    /// 接地判定
    /// </summary>
    /// <returns>接地しているかどうか</returns>
    public bool OnGround(float radGround, float rayRangeGround, LayerMask hitLayerGround)
    {
        //スフィアキャストを飛ばして判定（プレイヤーレイヤーには当たらない）
        if (Physics.SphereCast(gameObject.transform.position + gameObject.transform.up * 3, radGround, -gameObject.transform.up, out hitGround, rayRangeGround, ~(1 << hitLayerGround)))
        {
            //接地判定
            isGround = true;
            nomalVector = hitGround.normal;
            return true;
        }
        else
        {
            isGround = false;
            return false;
        }
        return false;
    }
    /// <summary>
    /// 重力変更
    /// </summary>
    public void GravityChange(GravityMode gravityMode)
    {
        mode = gravityMode;
        switch (mode)
        {
            case GravityMode.Left:
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(gameObject.transform.forward + Vector3.ProjectOnPlane(gameObject.transform.forward, Vector3.forward), Vector3.forward), 0.1f);
                break;
            case GravityMode.Up:
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(gameObject.transform.forward + Vector3.ProjectOnPlane(gameObject.transform.forward, -Vector3.up), -Vector3.up), 0.1f);
                break;
            case GravityMode.Right:
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(gameObject.transform.forward + Vector3.ProjectOnPlane(gameObject.transform.forward, -Vector3.forward), -Vector3.forward), 0.1f);
                break;
            case GravityMode.Down:
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(gameObject.transform.forward + Vector3.ProjectOnPlane(gameObject.transform.forward, Vector3.up), Vector3.up), 0.1f);
                break;
        }
    }
}
