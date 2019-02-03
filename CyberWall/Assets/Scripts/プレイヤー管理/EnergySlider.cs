using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ←※これを忘れずに入れる


public class EnergySlider : SingletonMono<EnergySlider>
{
    Slider _slider;
    RawImage _gameOver;
    float alpha;
    float hp;
    public float minusHp;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _gameOver = GameObject.Find("GameOver").GetComponent<RawImage>();
        _slider.maxValue = 100;
        hp = 100;
        alpha = 0;
    }
    private void Update()
    {
        _slider.value = hp;
        hp -= minusHp*Time.deltaTime;
        if (hp > 100)
        {
            hp = 100;
        }
        if(hp<=0)
        {
            alpha += 1 * Time.deltaTime;
            _gameOver.color = new Color(255, 255, 255, alpha);
        }
    }
    public void ChangePlusEnergyBar(float n)
    {
        hp += n;
    }
}
