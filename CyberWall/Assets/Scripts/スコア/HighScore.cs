using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    int[] rankingValue = new int[5];
    private Text highScoreText; //ハイスコアを表示するText

    void Start ()
    {
        highScoreText = GetComponent<Text>();
        highScoreText.text = "";
        GetRanking();
        SetRanking(EnergySlider.Instance.score);
        highScoreText.text += "\n";
        for (int i = 0; i < rankingValue.Length; i++)
        {
            highScoreText.text +=  rankingValue[i].ToString()+"\n";
        }
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
        else if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("タイトル");
        }
    }
}
