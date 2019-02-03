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
                break;
            case 1:
                nextStageMode = StageMode.Tube;
                modeN = Random.Range(0, tubeStageLength);
                pastStage[pastNumber] = Instantiate(StageList.Instance.TubeStage[modeN], new Vector3(0, -100, 0), Quaternion.identity);
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
                break;
            case 1:
                nextStageMode = StageMode.Tube;
                modeN = Random.Range(0, tubeStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.TubeStage[modeN], new Vector3(0, yPoint-100, zPoint), Quaternion.identity);
                break;
            case 2:
                nextStageMode = StageMode.Fall;
                modeN = Random.Range(0, fallStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
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
}
