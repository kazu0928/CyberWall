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

    //初期化
    void Start()
    {
        playerObj = Instantiate(parameter.PlayerObj, transform.position, transform.rotation);
        if(parameter.SpeedEffectObject != null)
        {
            speedEffectObject = Instantiate(parameter.SpeedEffectObject, new Vector3(10, 10, -50), parameter.SpeedEffectObject.transform.rotation);
            controller = new PlayerController(parameter, speedEffectObject);
            return;
        }
        //fx = Camera.main.gameObject.GetComponent<GlitchFx>();
        controller = new PlayerController(parameter);
    }
    private void Update()
    {
        controller.MoveLRInput();
    }
}
