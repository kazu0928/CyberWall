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
        lookPlayerSearch();//プレイヤーが格納されていない場合プレイヤーを探す
        if (lookPosition == null)
        {
            return;
        }
        //カメラ処理
        switch (nowCameraMode)
        {
            case CameraMode.NomalTPS:
                NomalCameraUpdate();//カメラの移動処理
                break;
            case CameraMode.FixedTPS:
                FixedCameraBoxUpdate();
                break;
            case CameraMode.TitleCamera:
                TitleCameraUpdate();
                break;
            case CameraMode.FixedEndressTPS:
                FixedCameraEndressUpdate();
                break;
            case CameraMode.FixedFallEndress:
                FallCameraEndressUpdate();
                break;
        }
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

        //カメラモードの設定
        nowCameraMode = mainCameraParameter.CameraMode;

        //プレイヤーの取得
        if (PlayerManager.Instance.PlayerObject == null || lookPosition != null)
        {
            return;
        }
        lookPosition = PlayerManager.Instance.PlayerObject.transform;
    }
    /// <summary>
    /// 通常時のカメラ挙動
    /// TPSで使うのはここ、あと初期化
    /// </summary>
    void NomalCameraUpdate()
    {
        if (mainCameraParameter.LeapFlag)
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = Vector3.Lerp(managerPos.position, lookPosition.position + mainCameraParameter.PlusTargetPos, mainCameraParameter.TrackingSpeed * Time.deltaTime);
            }
        }
        else
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = lookPosition.position + mainCameraParameter.PlusTargetPos;
            }
        }

        //カメラとの距離
        childPos.localPosition = new Vector3(0, 0, -mainCameraParameter.Distance);
        //カメラ角度
        managerPos.localEulerAngles = lookAngles;
        //カメラの位置
        cameraPos.localPosition = mainCameraParameter.OffsetPos;
        if (mainCameraParameter.RotationFlag)
        {
            rotationPos.localEulerAngles = lookPosition.rotation.eulerAngles;
        }
        //壁めり込み判定
        RaycastHit hit;
        if (Physics.Raycast(managerPos.position, childPos.position - managerPos.position, out hit, (childPos.position - managerPos.position).magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            childPos.position = hit.point;
        }
        //カメラの回転メソッド
        CameraRotation();
    }
    /// <summary>
    /// 固定カメラ挙動
    /// </summary>
    void FixedCameraBoxUpdate()
    {
        Vector3 vector = Vector3.zero;
        float yPos = 0;
        yPos = mainCameraParameter.YPoint;
        for (int i = 0; i < mainCameraParameter.CameraFixedPos.Length; i++)
        {
            switch (mainCameraParameter.CameraFixedPos[i])
            {
                case CameraFixed.XFixed:
                    vector += new Vector3(lookPosition.position.x, 0 + yPos, 0);
                    break;
                case CameraFixed.YFixed:
                    vector += new Vector3(0, lookPosition.position.y, 0);
                    break;
                case CameraFixed.ZFixed:
                    vector += new Vector3(0, 0 + yPos, lookPosition.position.z);
                    break;
            }
        }
        if (mainCameraParameter.LeapFlag)
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = Vector3.Lerp(new Vector3((vector + mainCameraParameter.PlusTargetPos).x, managerPos.position.y, managerPos.position.z), vector + mainCameraParameter.PlusTargetPos, mainCameraParameter.TrackingSpeed);
            }
        }
        else
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = vector + mainCameraParameter.PlusTargetPos;
            }
        }

        //カメラとの距離
        childPos.localPosition = new Vector3(0, 0, -mainCameraParameter.Distance);
        //カメラ角度
        managerPos.localEulerAngles = lookAngles;
        lookAngles = Vector2.Lerp(lookAngles, mainCameraParameter.LookAngles, 0.03f);
        //カメラの位置
        cameraPos.localPosition = mainCameraParameter.OffsetPos;
        if (mainCameraParameter.RotationFlag)
        {
            rotationPos.localEulerAngles = lookPosition.rotation.eulerAngles;
        }
        //壁めり込み判定
        RaycastHit hit;
        if (Physics.Raycast(managerPos.position, childPos.position - managerPos.position, out hit, (childPos.position - managerPos.position).magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            childPos.position = hit.point;
        }
    }
    /// <summary>
    /// 固定カメラエンドレス
    /// </summary>
    void FixedCameraEndressUpdate()
    {
        Vector3 vector = Vector3.zero;
        float yPos = 0;
        yPos = mainCameraParameter.YPoint + GameManager.Instance.cameraYPoint;
        for (int i = 0; i < mainCameraParameter.CameraFixedPos.Length; i++)
        {
            switch (mainCameraParameter.CameraFixedPos[i])
            {
                case CameraFixed.XFixed:
                    vector += new Vector3(lookPosition.position.x, 0 + yPos, 0);
                    break;
                case CameraFixed.YFixed:
                    vector += new Vector3(0, lookPosition.position.y, 0);
                    break;
                case CameraFixed.ZFixed:
                    vector += new Vector3(0, 0 + yPos, lookPosition.position.z);
                    break;
            }
        }
        if (mainCameraParameter.LeapFlag)
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = Vector3.Lerp(new Vector3((vector + mainCameraParameter.PlusTargetPos).x, managerPos.position.y, managerPos.position.z), vector + mainCameraParameter.PlusTargetPos, mainCameraParameter.TrackingSpeed);
            }
        }
        else
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = vector + mainCameraParameter.PlusTargetPos;
            }
        }

        //カメラとの距離
        childPos.localPosition = new Vector3(0, 0, -mainCameraParameter.Distance);
        //カメラ角度
        managerPos.localEulerAngles = lookAngles;
        lookAngles = Vector2.Lerp(lookAngles, mainCameraParameter.LookAngles + new Vector2(GameManager.Instance.cameraRotaX, GameManager.Instance.cameraRotaY), 0.03f);
        //カメラの位置
        cameraPos.localPosition = mainCameraParameter.OffsetPos;
        if (mainCameraParameter.RotationFlag)
        {
            rotationPos.localEulerAngles = lookPosition.rotation.eulerAngles;
        }
        //壁めり込み判定
        RaycastHit hit;
        if (Physics.Raycast(managerPos.position, childPos.position - managerPos.position, out hit, (childPos.position - managerPos.position).magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            childPos.position = hit.point;
        }
    }
    void FallCameraEndressUpdate()
    {
        Vector3 vector = Vector3.zero;
        float yPos = 0;
        yPos = mainCameraParameter.YPoint + GameManager.Instance.cameraYPoint;
        for (int i = 0; i < mainCameraParameter.CameraFixedPos.Length; i++)
        {
            switch (mainCameraParameter.CameraFixedPos[i])
            {
                case CameraFixed.XFixed:
                    vector += new Vector3(lookPosition.position.x, 0 + yPos, 0);
                    break;
                case CameraFixed.YFixed:
                    vector += new Vector3(0, lookPosition.position.y, GameManager.Instance.cameraZPoint-1200);
                    break;
                case CameraFixed.ZFixed:
                    vector += new Vector3(0, 0 + yPos, lookPosition.position.z);
                    break;
            }
        }
        if (mainCameraParameter.LeapFlag)
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = Vector3.Lerp(new Vector3((vector + mainCameraParameter.PlusTargetPos).x, managerPos.position.y, managerPos.position.z), vector + mainCameraParameter.PlusTargetPos, mainCameraParameter.TrackingSpeed);
            }
        }
        else
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = vector + mainCameraParameter.PlusTargetPos;
            }
        }

        //カメラとの距離
        childPos.localPosition = new Vector3(0, 0, -mainCameraParameter.Distance);
        //カメラ角度
        managerPos.localEulerAngles = lookAngles;
        lookAngles = Vector2.Lerp(lookAngles, mainCameraParameter.LookAngles, 0.03f);
        //カメラの位置
        cameraPos.localPosition = mainCameraParameter.OffsetPos;
        if (mainCameraParameter.RotationFlag)
        {
            rotationPos.localEulerAngles = lookPosition.rotation.eulerAngles;
        }
        //壁めり込み判定
        RaycastHit hit;
        if (Physics.Raycast(managerPos.position, childPos.position - managerPos.position, out hit, (childPos.position - managerPos.position).magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            childPos.position = hit.point;
        }
    }
    /// <summary>
    /// タイトル用カメラ挙動
    /// </summary>
    void TitleCameraUpdate()
    {
        if (mainCameraParameter.LeapFlag)
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = Vector3.Lerp(managerPos.position, lookPosition.position + mainCameraParameter.PlusTargetPos, mainCameraParameter.TrackingSpeed * Time.deltaTime);
            }
        }
        else
        {
            //注視対象が設定されている場合追いかける
            if (lookPosition != null)
            {
                rotationPos.position = lookPosition.position + mainCameraParameter.PlusTargetPos;
            }
        }

        //カメラとの距離
        childPos.localPosition = new Vector3(0, 0, -mainCameraParameter.Distance);
        //カメラ角度
        managerPos.localEulerAngles = lookAngles;
        //カメラの位置
        cameraPos.localPosition = mainCameraParameter.OffsetPos;
        rotationPos.localEulerAngles = lookPosition.rotation.eulerAngles;
        //壁めり込み判定
        RaycastHit hit;
        if (Physics.Raycast(managerPos.position, childPos.position - managerPos.position, out hit, (childPos.position - managerPos.position).magnitude, 1 << LayerMask.NameToLayer("Wall")))
        {
            childPos.position = hit.point;
        }
        //カメラの回転メソッド
        CameraRotationTitleAround();
    }
    /// <summary>
    /// プレイヤーを探して格納
    /// </summary>
    void lookPlayerSearch()
    {
        //プレイヤーの取得
        if (PlayerManager.Instance.PlayerObject == null || lookPosition != null)
        {
            return;
        }
        lookPosition = PlayerManager.Instance.PlayerObject.transform;
    }

    //モード変更して必要なところを初期化（パラメータが変更されるとき使用）
    public void InitCameraParameterChange(CameraParameter parameter)
    {
        mainCameraParameter = parameter;
        nowCameraMode = mainCameraParameter.CameraMode;
    }
    /// <summary>
    /// カメラの向く対象を決める
    /// 他から呼び出し
    /// </summary>
    public void CameraLookAt(Transform transform)
    {
        lookPosition = transform;
    }
    //カメラパラメータ変更（初期化なし）
    public void ChangeCameraParamater(CameraParameter parameter)
    {
        mainCameraParameter = parameter;
    }
    /// <summary>
    /// カメラを回転させる
    /// </summary>
    private void CameraRotation()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        lookAngles += new Vector2(-inputY, inputX) * mainCameraParameter.CameraSpeed * Time.deltaTime;
        if (lookAngles.y > 360)
        {
            lookAngles.y -= 360;
        }
        if (lookAngles.y < -360)
        {
            lookAngles.y += 360;
        }
        if (lookAngles.x > mainCameraParameter.MaxRotation)
        {
            lookAngles.x = mainCameraParameter.MaxRotation;
        }
        if (lookAngles.x < -mainCameraParameter.MaxRotation)
        {
            lookAngles.x = -mainCameraParameter.MaxRotation;
        }
    }
    //タイトルのカメラ回転
    private void CameraRotationTitleAround()
    {
        float inputX = 0.03f;
        float inputY = 0f;
        lookAngles += new Vector2(-inputY, inputX) * mainCameraParameter.CameraSpeed * Time.deltaTime;
        if (lookAngles.y > 360)
        {
            lookAngles.y -= 360;
        }
        if (lookAngles.y < -360)
        {
            lookAngles.y += 360;
        }
        if (lookAngles.x > mainCameraParameter.MaxRotation)
        {
            lookAngles.x = mainCameraParameter.MaxRotation;
        }
        if (lookAngles.x < -mainCameraParameter.MaxRotation)
        {
            lookAngles.x = -mainCameraParameter.MaxRotation;
        }
    }
}
