//
//MainCameraManager.cs
//メインカメラを管理するクラス
//参照 PlayerManager.Instance
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : SingletonMono<MainCameraManager>
{
    //カメラの位置
    private Transform managerPos;
    private Transform childPos;
    private Transform cameraPos;
    private Transform rotationPos;

    //現在のカメラモード（保存、確認用）
    public CameraMode nowCameraMode;

    //カメラのパラメータ
    [SerializeField]
    private CameraParameter mainCameraParameter;

    //注視点
    private Transform lookPosition = null;

    //現在の角度
    private Vector2 lookAngles;

    void Start()
    {
        Init();//初期化
    }

    void Update ()
    {
		
	}
    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        //Rotation->Manager->Distance->Camera
        //カメラオブジェクト初期化、親子付け
        //重力変更時の回転用
        rotationPos = new GameObject().transform;
        rotationPos.name = "Rotation";
        //通常の回転用
        managerPos = transform;
        managerPos.transform.parent = rotationPos;
        managerPos.localPosition = Vector3.zero;
        //カメラとの距離
        childPos = new GameObject().transform;
        childPos.parent = managerPos;
        childPos.name = "CameraDistanceChild";
        childPos.localPosition = Vector3.zero;
        //注視点の位置
        cameraPos = Camera.main.transform;
        cameraPos.parent = childPos;
        cameraPos.localPosition = Vector3.zero;

        //角度の初期化
        lookAngles = mainCameraParameter.LookAngles;


        //プレイヤーの取得
        if (PlayerObjectManager.Instance.PlayerObject == null || lookPosition != null)
        {
            return;
        }
        lookPosition = PlayerManager.Instance.PlayerObject.transform;

        //カメラモードの設定
        nowCameraMode = mainCameraParameter.CameraMode;
    }
}
