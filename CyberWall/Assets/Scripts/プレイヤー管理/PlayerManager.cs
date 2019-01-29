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

    //初期化
    void Start()
    {
        playerObj = Instantiate(parameter.PlayerObj, transform.position, transform.rotation);
        controller = new PlayerController(parameter);
        //fx = Camera.main.gameObject.GetComponent<GlitchFx>();
    }
    private void Update()
    {
        controller.PlayerMoveLeftRight();
    }
}
