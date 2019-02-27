using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimeCount : MonoBehaviour
{
    Text text;
    float timer = 0;
    int disPoint = 20;
    bool startFlag = false;
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine("CountStart");
        GameManager.Instance.plusDistanceCam = 0;
    }
    void Update()
    {
            timer += Time.deltaTime / 3.5f;
            GameManager.Instance.cameraRotYNoLeap -= (360 * Time.deltaTime) / 3.5f;
            GameManager.Instance.plusDistanceCam =Mathf.Clamp(  -Mathf.Sin((180 * timer * Mathf.Deg2Rad)) * disPoint,-50,0);
        if(PlayerObjectManager.Instance.PlayerObject==true&&startFlag==false)
        {
            PlayerManager.Instance.PlusUpSpeedAndMax(0, -PlayerManager.Instance.PlayerParam.MaxSpeed);
            startFlag = true;
        }
    }
    IEnumerator CountStart()
    {
        text.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        text.text = "2";
        yield return new WaitForSecondsRealtime(1);
        text.text = "1";
        yield return new WaitForSecondsRealtime(1);
        text.text = "START";
        PlayerManager.Instance.PlusUpSpeedAndMax(0, PlayerManager.Instance.PlayerParam.MaxSpeed);
        yield return new WaitForSecondsRealtime(0.5f);
        GameManager.Instance.plusDistanceCam = 0;
        GameManager.Instance.cameraRotYNoLeap = 0;
        Destroy(text.gameObject);
    }
}
