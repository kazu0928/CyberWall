using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ←※これを忘れずに入れる


public class EnergySlider : SingletonMono<EnergySlider>
{
    Slider _slider;
    float hp;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _slider.maxValue = 100;
        hp = 100;
    }
    private void Update()
    {
        _slider.value = hp;
        hp -= Time.deltaTime;
        if (hp > 100)
        {
            hp = 100;
        }
    }
    public void ChangePlusEnergyBar(float n)
    {
        hp += n;
    }
}
