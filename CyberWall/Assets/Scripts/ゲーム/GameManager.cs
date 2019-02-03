//
//ゲームの流れを操るクラス
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    public float yPoint;public float zPoint;
    public float cameraYPoint;public float cameraZPoint;

    private int modeLength;
    private int boxStageLength;
    private int tubeStageLength;
    private int fallStageLength;

    private StageMode nowStageMode;
    private GameObject[] pastStage = { null,null,null,null };//消す
    private int pastNumber;
    private GameObject startStage;
    private bool startFlag;
    private StageMode nextStageMode;
    private StageMode next2StageMode;//使ってない

	void Start ()
    {
        pastNumber = 0;
        startFlag = false;
        yPoint = 0;zPoint = 0;
        cameraYPoint = 0; cameraZPoint = 0;
        //mode数を取得
        modeLength = System.Enum.GetValues(typeof(StageMode)).Length;
        boxStageLength = StageList.Instance.BoxStage.Length;
        tubeStageLength = StageList.Instance.TubeStage.Length;
        fallStageLength = StageList.Instance.FallStage.Length;
        //次どのモードか
        int modeN = Random.Range(0, 2);
        //次どの配列か
        switch (modeN)
        {
            case 0:
                nextStageMode = StageMode.Nomal;
                modeN = Random.Range(0, boxStageLength);
                pastStage[pastNumber] = Instantiate(StageList.Instance.BoxStage[modeN], new Vector3(0,-100,0), Quaternion.identity);
                CreateEnergyNormal(pastStage[pastNumber]);

                break;
            case 1:
                nextStageMode = StageMode.Tube;
                modeN = Random.Range(0, tubeStageLength);
                pastStage[pastNumber] = Instantiate(StageList.Instance.TubeStage[modeN], new Vector3(0, -100, 0), Quaternion.identity);
                CreateEnergyTube(pastStage[pastNumber]);

                break;
        }
        pastNumber++;
    }
    public void RandomNextCreateMode(StageMode mode)
    {
        nowStageMode = mode;
        //次どのモードか
        int modeN = Random.Range(0, modeLength);
        //次どの配列か
        switch(modeN)
        {
            case 0:
                nextStageMode = StageMode.Nomal;
                modeN = Random.Range(0, boxStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.BoxStage[modeN], new Vector3(0, yPoint-100, zPoint), Quaternion.identity);
                CreateEnergyNormal(pastStage[pastNumber]);
                break;
            case 1:
                nextStageMode = StageMode.Tube;
                modeN = Random.Range(0, tubeStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.TubeStage[modeN], new Vector3(0, yPoint-100, zPoint), Quaternion.identity);
                CreateEnergyTube(pastStage[pastNumber]);
                break;
            case 2:
                nextStageMode = StageMode.Fall;
                modeN = Random.Range(0, fallStageLength);
                yPoint -= 100;
                zPoint += 700;
                cameraYPoint -= 100;
                cameraZPoint += 700;
                pastStage[pastNumber] = Instantiate(StageList.Instance.FallStage[modeN], new Vector3(0, yPoint - 100, zPoint), Quaternion.identity);
                break;
        }
        //削除
        if(pastNumber==3)
        {
            Destroy(pastStage[0]);
            Destroy(pastStage[1]);
            pastStage[0] = pastStage[2];
            pastStage[1] = pastStage[3];
            pastStage[2] = null;
            pastStage[3] = null;
            pastNumber = 2;
        }
        else
        {
            pastNumber++;
        }
    }
    public void PlusYPoint(float a)
    {
        yPoint += a;
    }
    public void PlusZPoint(float a)
    {
        zPoint += a;
    }
    const float Front = 450;
    const float Back = 490;
    const float boxEdge = 9;
    private void CreateEnergyNormal(GameObject o)
    {
        float large = Random.Range(10, 15);
        for(int i=0;i<large;i++)
        {
            GameObject item;
            item = Instantiate(StageList.Instance.EnergyItem);
            item.transform.parent = o.transform;
            float pos = Random.Range(-Front, Back);
            float posLocal = Random.Range(-boxEdge, boxEdge);
            int gravMode = Random.Range(1, 5);
            switch(gravMode)
            {
                //上
                case 0:
                    item.transform.localPosition = new Vector3(posLocal, boxEdge, pos);
                    break;
                //下
                case 1:
                    item.transform.localPosition = new Vector3(posLocal, -boxEdge, pos);
                    break;
                //右
                case 2:
                    item.transform.localPosition = new Vector3(boxEdge, posLocal, pos);
                    break;
                case 3:
                    item.transform.localPosition = new Vector3(-boxEdge, posLocal, pos);
                    break;
            }
        }
    }
    const float tubeEdge = 4;
    private void CreateEnergyTube(GameObject o)
    {
        float large = Random.Range(10, 15);
        for (int i = 0; i < large; i++)
        {

            GameObject item;
            item = Instantiate(StageList.Instance.EnergyItem);
            item.transform.parent = o.transform;
            float pos = Random.Range(-Front, Back);
            int x = Random.Range(-180, 180); int y = Random.Range(-180, 180);
            Vector2 xy = new Vector2(x, y).normalized * tubeEdge;
            item.transform.localPosition = new Vector3(xy.x,xy.y,pos);
        }
    }
}
