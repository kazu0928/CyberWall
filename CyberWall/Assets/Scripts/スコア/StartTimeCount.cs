using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimeCount : MonoBehaviour
{
    Text text;
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine("CountStart");
    }

    IEnumerator CountStart()
    {
        Time.timeScale = 0;
        text.text = "3";
        yield return new WaitForSecondsRealtime(1);
        text.text = "2";
        yield return new WaitForSecondsRealtime(1);
        text.text = "1";
        yield return new WaitForSecondsRealtime(1);
        text.text = "START";
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(text.gameObject);
    }
}
