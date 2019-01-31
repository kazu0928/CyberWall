using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonMono<PlayerManager>
{
    //プレイヤーオブジェクト
    private GameObject playerObj = null;
    public GameObject PlayerObject
    {
        get { return playerObj; }
    }
    //
    [SerializeField]
    private PlayerParameter parameter;
    private PlayerController controller;
    private GameObject speedEffectObject;
    //画面効果
    private GlitchFx fx;
    //現在のステージが何か
    [SerializeField]
    private StageMode nowStageMode;

    //初期化
    void Start()
    {
        playerObj = Instantiate(parameter.PlayerObj, transform.position, transform.rotation).gameObject;
        fx = Camera.main.gameObject.GetComponent<GlitchFx>();
        //プレイヤーにイベント用のスクリプトをアタッチ
        playerObj.AddComponent<PlayerOnEvent>();
        if(parameter.SpeedEffectObject != null)
        {
            speedEffectObject = Instantiate(parameter.SpeedEffectObject, new Vector3(100, 100, -500), parameter.SpeedEffectObject.transform.rotation);
            controller = new PlayerController(parameter, speedEffectObject);
            return;
        }
        controller = new PlayerController(parameter);
    }
    private void Update()
    {
        if (nowStageMode == StageMode.Tube)
        {
            controller.MoveTube();
        }
        else if(nowStageMode == StageMode.Nomal)
        {
            controller.Move();
        }
    }
    //デバッグ用
    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(230,155,144,0.3f);
        Gizmos.DrawSphere(transform.position, 1);
        if (playerObj == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(playerObj.transform.position + playerObj.transform.up * 3 + -playerObj.transform.up * parameter.RayRangeGround, parameter.RadGround);
    }
    //--------------------
    //スピード変更（加算式）
    public void PlusSpeedChange(float speed)
    {
        controller.PlusSpeedChange(speed);
    }
    //ダメージエフェクト
    public void StartDamage()
    {
        StartCoroutine("damage");
    }
    public void ChangeStageMode(StageMode mode)
    {
        nowStageMode = mode;
    }
    IEnumerator damage()
    {
        fx.enabled = true;
        yield return new WaitForSeconds(1);
        fx.enabled = false;
    }
}
