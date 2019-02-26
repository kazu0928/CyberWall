﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ←※これを忘れずに入れる


public class EnergySlider : SingletonMono<EnergySlider>
{
    Slider _slider;
    RawImage _gameOver;
    Text _text;
    float alpha;
    public float hp;
    public float minusHp;
    public int score;
    bool overFlag = false;
    Image panel;
    [SerializeField]
    private GameObject rankingText;
    void Start()
    {
        overFlag = false;
        // スライダーを取得する
        panel = GameObject.Find("Panel").GetComponent<Image>();
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _gameOver = GameObject.Find("GameOver").GetComponent<RawImage>();
        _text = GameObject.Find("Score").GetComponent<Text>();
        _slider.maxValue = 100;
        hp = 100;
        alpha = 0;
        score = 0;
    }
    private void Update()
    {
        _slider.value = hp;
        //色が赤くなる
        if(hp>0)
        {
            panel.color = new Color(panel.color.r, panel.color.b, panel.color.b, (1 - hp / 100) - 0.5f);
        }
        else
        {
            panel.color = new Color(panel.color.r, panel.color.b, panel.color.b, 0);
        }
        hp -= minusHp*Time.deltaTime;
        if (hp > 100)
        {
            hp = 100;
        }
        _text.text = ((int)score).ToString();
        if (hp<=0)
        {
            PlusScore.Instance.resetScorePlus();
            alpha += 1 * Time.deltaTime;
            _gameOver.color = new Color(255, 255, 255, alpha);
            if(PlayerObjectManager.Instance.PlayerObject!=null)
            {
                ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * 2, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
                Destroy(
                PlayerObjectManager.Instance.PlayerObject);
            }
            if (overFlag) return;
            if(alpha>1)
            {
                GameObject a =
                Instantiate(rankingText);
                a.transform.parent = transform;
                a.transform.localPosition=new Vector3(-529,0,0);
                overFlag = true;
            }
            return;
        }
        //score += PlayerManager.Instance.GetAccelSpeed() * Time.deltaTime;
    }
    public void ChangePlusEnergyBar(float n)
    {
        hp += n;
    }
}
