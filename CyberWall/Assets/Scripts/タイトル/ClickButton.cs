using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour {
    public Button target;
    public Image fadeOut;
    float alfa;    //A値を操作するための変数
    float red, green, blue;    //RGBを操作するための変数
    bool fadeFlag;

    private void Start()
    {
        fadeFlag = false;
        alfa = 0;
        red = fadeOut.color.r;
        green = fadeOut.color.g;
        blue = fadeOut.color.b;
        target.onClick.AddListener(() => StartCoroutine("fade"));
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ExecuteEvents.Execute
            (
                target: target.gameObject,
                eventData: new PointerEventData(EventSystem.current),
                functor: ExecuteEvents.pointerClickHandler
            );
        }
        if(fadeFlag==true)
        {
            fadeOut.color = new Color(red, green, blue, alfa);
            alfa += 1*Time.deltaTime;
        }
    }
    IEnumerator fade()
    {
        if (fadeFlag == false)
        {
            fadeFlag = true;
            SoundManager.Instance.PlayStartSound();
        }
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("テスト用シーン");
    }
}
