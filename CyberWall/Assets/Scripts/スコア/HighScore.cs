using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];
    private Text[] highScoreText= { null,null,null,null,null }; //ハイスコアを表示するText
    private int runkNumber;

    void Start ()
    {
        runkNumber = 5;
        for (int i = 0; i < 5; i++)
        {
            highScoreText[i] = transform.Find((i+1).ToString()).GetComponent<Text>();
        }
        for (int i = 0; i < 5; i++)
        {
            highScoreText[i].text = "";
        }
        GetRanking();
        SetRanking(EnergySlider.Instance.score);
        for (int i = 0; i < rankingValue.Length; i++)
        {
            highScoreText[i].text =  rankingValue[i].ToString();
        }
        StartCoroutine("tenmetu");
    }
    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    void SetRanking(int _value)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                runkNumber = i;
                break;
            }
        }
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                int change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
            }
        }
        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.DeleteAll();
        }
        else if(Input.anyKeyDown||Tap())
        {
            SceneManager.LoadScene("タイトル");
        }
    }
    private bool Tap()
    {
        // タッチされているかチェック
        if (Input.touchCount > 0)
        {
            // タッチ情報の取得
            Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    return true;
                }
        }
        return false;
    }

    IEnumerator tenmetu()
    {
        if(runkNumber>4)
        {
            yield break;
        }
        highScoreText[runkNumber].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        highScoreText[runkNumber].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine("tenmetu");

    }
}
