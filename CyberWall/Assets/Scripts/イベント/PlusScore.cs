using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusScore : SingletonMono<PlusScore>
{
    public int plusScore;
    [SerializeField]
    private GameObject plus;
    Text text;
	void Start ()
    {
        plusScore = 0;
        text = GetComponent<Text>();
	}
	void Update ()
    {
        if (plusScore > 0)
        {
            text.text = "+" + plusScore;
            return;
        }
        text.text = "";
	}
    public void resetScorePlus()
    {
        if (plusScore == 0)
        {
            return;
        }
        GameObject a = 
        Instantiate(plus, transform.position,Quaternion.identity);
        a.transform.parent = transform.parent;
        a.GetComponent<RectTransform>().position = text.rectTransform.position;
        a.GetComponent<PlusScoreUpdate>().plusScore = plusScore;    
        plusScore = 0;
    }
}
