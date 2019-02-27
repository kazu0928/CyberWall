using System.Collections;
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
    Image panel2;
    Image panel3;
    [SerializeField]
    private GameObject rankingText;

    RawImage RKey;
    RawImage LKey;
    RawImage LR;
    RawImage WS;
    RawImage JumpKey;
    void Start()
    {
        overFlag = false;
        // スライダーを取得する
        panel = GameObject.Find("Panel").GetComponent<Image>();
        panel2 = panel.transform.GetChild(0).GetComponent<Image>();
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        _gameOver = GameObject.Find("GameOver").GetComponent<RawImage>();
        _text = GameObject.Find("Score").GetComponent<Text>();
        _slider.maxValue = 100;
        hp = 100;
        alpha = 0;
        score = 0;

        //キー画像取得
        RKey = GameObject.Find("RKey").GetComponent<RawImage>();
        LKey = GameObject.Find("LKey").GetComponent<RawImage>();
        JumpKey = GameObject.Find("BButton").GetComponent<RawImage>();
        LR = GameObject.Find("XKey").GetComponent<RawImage>();
        WS = LR.transform.GetChild(0).GetComponent<RawImage>();
    }
    private void Update()
    {
        _slider.value = hp;
        //色が赤くなる
        if(hp>0&&hp<40)
        {
            panel.color = new Color(panel.color.r, panel.color.b, panel.color.b, Mathf.Lerp(panel.color.a, (1 - hp / 60), 0.01f*Time.deltaTime*140));
        }
        else if(hp>=40)
        {
            panel.color = new Color(panel.color.r, panel.color.b, panel.color.b, 0);
        }
        if (hp > 0 && hp < 20)
        {
            panel2.color = new Color(panel2.color.r, panel2.color.b, panel2.color.b, Mathf.Lerp(panel2.color.a, (1 - hp /60), 0.01f * Time.deltaTime * 140));
        }
        else if (hp >= 20)
        {
            panel2.color = new Color(panel.color.r, panel.color.b, panel.color.b, 0);
        }
        //else
        //{
        //    panel.color = new Color(panel.color.r, panel.color.b, panel.color.b, 0);
        //}
        hp -= minusHp*Time.deltaTime*(PlayerManager.Instance.GetMaxSpeed()/PlayerManager.Instance.PlayerParam.MaxSpeed);
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
        if(PlayerManager.Instance.NowStageMode==StageMode.Nomal)
        {
            if(PlayerManager.Instance.fallPrev == true)
            {
                JumpKey.color = new Color(JumpKey.color.r, JumpKey.color.g, JumpKey.color.b, 1);
                RKey.color = new Color(RKey.color.r, RKey.color.g, RKey.color.b, 0);
                LKey.color = new Color(LKey.color.r, LKey.color.g, LKey.color.b, 0);
                LR.color = new Color(LR.color.r, LR.color.g, LR.color.b, 1);
                WS.color = new Color(WS.color.r, WS.color.g, WS.color.b, 0);
            }
            else
            {
                JumpKey.color = new Color(JumpKey.color.r, JumpKey.color.g, JumpKey.color.b, 1);
                RKey.color = new Color(RKey.color.r, RKey.color.g, RKey.color.b, 1);
                LKey.color = new Color(LKey.color.r, LKey.color.g, LKey.color.b, 1);
                LR.color = new Color(LR.color.r, LR.color.g, LR.color.b, 1);
                WS.color = new Color(WS.color.r, WS.color.g, WS.color.b, 0);
            }
        }
        else if(PlayerManager.Instance.NowStageMode == StageMode.Tube)
        {
            JumpKey.color = new Color(JumpKey.color.r, JumpKey.color.g, JumpKey.color.b, 0);
            RKey.color = new Color(RKey.color.r, RKey.color.g, RKey.color.b, 0);
            LKey.color = new Color(LKey.color.r, LKey.color.g, LKey.color.b, 0);
            LR.color = new Color(LR.color.r, LR.color.g, LR.color.b, 1);
            WS.color = new Color(WS.color.r, WS.color.g, WS.color.b, 0); 
        }
        else if (PlayerManager.Instance.NowStageMode == StageMode.Fall)
        {
            JumpKey.color = new Color(JumpKey.color.r, JumpKey.color.g, JumpKey.color.b, 0);
            RKey.color = new Color(RKey.color.r, RKey.color.g, RKey.color.b, 0);
            LKey.color = new Color(LKey.color.r, LKey.color.g, LKey.color.b, 0);
            LR.color = new Color(LR.color.r, LR.color.g, LR.color.b, 1);
            WS.color = new Color(WS.color.r, WS.color.g, WS.color.b, 1);
        }

    }
    public void ChangePlusEnergyBar(float n)
    {
        hp += n;
    }
}
