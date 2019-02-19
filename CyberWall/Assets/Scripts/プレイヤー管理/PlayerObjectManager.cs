//
// プレイヤーの子オブジェクトの取得クラス
// シングルトンオブジェクト
// 2019/01/29
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectManager
{
    //TODO:必要なオブジェクトがあったときに随時追加

    //プレイヤーオブジェクト
    private GameObject playerObject;
    //傾きオブジェクト
    private GameObject tiltObject;
    //プレイヤーモデル
    private GameObject playerModel;
    //リジッドボディ
    private Rigidbody rigidbody;

    //Singleton化
    private static PlayerObjectManager instance = new PlayerObjectManager();
    public static PlayerObjectManager Instance
    {
        get { return instance; }
    }
    /// <summary>
    /// コンストラクタ
    /// 子オブジェクト取得
    /// </summary>
    private PlayerObjectManager()
    {
        playerObject = PlayerManager.Instance.PlayerObject;
        tiltObject = playerObject.transform.Find("Tilt").gameObject;
        playerModel = tiltObject.transform.Find("PlayerModel").gameObject;
        rigidbody = playerObject.GetComponent<Rigidbody>();
    }
    //getter
    public GameObject PlayerObject
    {
        get { return playerObject; }
    }
    public GameObject TiltObject
    {
        get { return tiltObject; }
    }
    public GameObject PlayerModel
    {
        get { return playerModel; }
    }
    public Rigidbody rb
    {
        get { return rigidbody; }
    }
    public void init()
    {
        instance = new PlayerObjectManager();
    }
}
