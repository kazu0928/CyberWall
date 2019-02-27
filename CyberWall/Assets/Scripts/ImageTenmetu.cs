using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTenmetu : MonoBehaviour {
    Image image;
    Text text;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.PingPong(Time.time, 1.5f));
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.PingPong(Time.time, 1.5f));
    }
}
