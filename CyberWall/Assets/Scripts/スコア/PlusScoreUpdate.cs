using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusScoreUpdate : MonoBehaviour
{
    private Text scoreText;
    RectTransform rectTransform;
    RectTransform scoreRect;
    public int plusScore=5000;
	void Start ()
    {
        GetComponent<Text>().text ="+" + plusScore;
        rectTransform = GetComponent<RectTransform>();
        scoreText = transform.parent.transform.Find("Score").GetComponent<Text>();
        scoreRect = scoreText.rectTransform;
	}
	
	void Update ()
    {
        rectTransform.position += (scoreRect.position - rectTransform.position).normalized * Time.deltaTime * 2000;
        if((scoreRect.position - rectTransform.position).magnitude<20)
        {
            EnergySlider.Instance.score+=plusScore;
            Destroy(gameObject);
        }
	}
}
