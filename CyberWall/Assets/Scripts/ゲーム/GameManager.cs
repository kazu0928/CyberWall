//
//ゲームの流れを操るクラス
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    public float yPoint; public float zPoint;
    public float cameraYPoint; public float cameraZPoint;
    public float cameraRotaX;public float cameraRotaY;

    private int modeLength;
    private int boxStageLength;
    private int tubeStageLength;
    private int fallStageLength;
    private int debugLength;

    private StageMode nowStageMode;
    private GameObject[] pastStage = { null, null, null, null };//消す
    private int pastNumber;
    private GameObject startStage;
    private bool startFlag;
    private StageMode nextStageMode;
    private StageMode next2StageMode;//使ってない

    [SerializeField]
    private bool DebugMode = false;

    void Start()
    {
        Input.gyro.enabled = true;
        pastNumber = 0;
        startFlag = false;
        yPoint = 0; zPoint = 0;
        cameraYPoint = 0; cameraZPoint = 0;
        int modeN;
        //デバッグモード
        if (DebugMode)
        {
            debugLength = StageList.Instance.DebugStage.Length;
            modeN = Random.Range(0, debugLength);
            pastStage[pastNumber] = Instantiate(StageList.Instance.DebugStage[modeN], new Vector3(0, -100, 0), Quaternion.identity);
            pastNumber++;

            return;
        }

        //mode数を取得
        modeLength = System.Enum.GetValues(typeof(StageMode)).Length;
        boxStageLength = StageList.Instance.BoxStage.Length;
        tubeStageLength = StageList.Instance.TubeStage.Length;
        fallStageLength = StageList.Instance.FallStage.Length;
        //次どのモードか
        modeN = Random.Range(1, 2);
        //次どの配列か
        switch (modeN)
        {
            case 0:
                nextStageMode = StageMode.Nomal;
                modeN = Random.Range(0, boxStageLength);
                pastStage[pastNumber] = Instantiate(StageList.Instance.BoxStage[modeN], new Vector3(0, -100, 0), Quaternion.identity);
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
        cameraRotaX = Random.Range(-10, 10);
        cameraRotaY = Random.Range(-10, 10);
        //次どの配列か
        switch (modeN)
        {
            case 0:
                nextStageMode = StageMode.Nomal;
                modeN = Random.Range(0, boxStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.BoxStage[modeN], new Vector3(0, yPoint - 100, zPoint), Quaternion.identity);
                CreateEnergyNormal(pastStage[pastNumber]);
                break;
            case 1:
                nextStageMode = StageMode.Tube;
                modeN = Random.Range(0, tubeStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.TubeStage[modeN], new Vector3(0, yPoint - 100, zPoint), Quaternion.identity);
                CreateEnergyTube(pastStage[pastNumber]);
                break;
            case 2:
                nextStageMode = StageMode.Fall;
                modeN = Random.Range(0, fallStageLength);
                yPoint -= 100;
                zPoint += 1200;
                cameraYPoint -= 100;
                cameraZPoint += 1200;
                pastStage[pastNumber] = Instantiate(StageList.Instance.FallStage[modeN], new Vector3(0, yPoint - 100, zPoint), Quaternion.identity);
                CreateEnergyFall(pastStage[pastNumber]);
                break;
        }
        //削除
        if (pastNumber == 3)
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
        for (int i = 0; i < large; i++)
        {
            GameObject item;
            item = Instantiate(StageList.Instance.EnergyItem);
            item.transform.parent = o.transform;
            float pos = Random.Range(-Front, Back);
            float posLocal = Random.Range(-boxEdge, boxEdge);
            int gravMode = Random.Range(1, 5);
            switch (gravMode)
            {
                //上
                case 1:
                    item.transform.localPosition = new Vector3(posLocal, boxEdge, pos);
                    break;
                //下
                case 2:
                    item.transform.localPosition = new Vector3(posLocal, -boxEdge, pos);
                    break;
                //右
                case 3:
                    item.transform.localPosition = new Vector3(boxEdge, posLocal, pos);
                    break;
                case 4:
                    item.transform.localPosition = new Vector3(-boxEdge, posLocal, pos);
                    break;
            }
        }
        //ここからギミック生成
        for (int i = 0; i < 7; i++)
        {
            GameObject gim;
            int r = Random.Range(0, StageList.Instance.BoxGimicList.Length);
            int r2 = Random.Range(0, StageList.Instance.BoxGimicList[r].GimicSet.Length);
            gim = Instantiate(StageList.Instance.BoxGimicList[r].GimicSet[r2]);
            gim.transform.parent = o.transform;
            gim.transform.localPosition = new Vector3(0, 0, 460 - (i * 150));
        }
    }
    const float tubeEdge = 5;
    private void CreateEnergyTube(GameObject o)
    {
        //アイテム生成
        float large = Random.Range(10, 15);
        for (int i = 0; i < large; i++)
        {
            GameObject item;
            item = Instantiate(StageList.Instance.EnergyItem);
            item.transform.parent = o.transform;
            float pos = Random.Range(-Front, Back);
            int x = Random.Range(-180, 180); int y = Random.Range(-180, 180);
            Vector2 xy = new Vector2(x, y).normalized * tubeEdge;
            item.transform.localPosition = new Vector3(xy.x, xy.y, pos);
        }
        //ここからギミック生成
        for (int i = 0; i < 7; i++)
        {
            GameObject gim;
            int r = Random.Range(0, StageList.Instance.TubeGimicList.Length);
            int r2 = Random.Range(0, StageList.Instance.TubeGimicList[r].GimicSet.Length);
            gim = Instantiate(StageList.Instance.TubeGimicList[r].GimicSet[r2]);
            gim.transform.parent = o.transform;
            gim.transform.localPosition = new Vector3(0, 0, 460 - (i * 150));
        }
    }
    private void CreateEnergyFall(GameObject o)
    {
        float large = Random.Range(19, 30);
        for (int i = 0; i < large; i++)
        {
            GameObject item;
            item = Instantiate(StageList.Instance.EnergyItem,new Vector3(0,300,0), StageList.Instance.EnergyItem.transform.rotation);
            item.transform.parent = o.transform;
            float pos = Random.Range(-1000, 0);
            float posLocal = Random.Range(-boxEdge, boxEdge);
            //int gravMode = Random.Range(1, 5);
            //switch (gravMode)
            //{
            //    //上
            //    case 0:
            //        item.transform.localPosition = new Vector3(boxEdge, pos, posLocal);
            //        break;
            //    //下
            //    case 1:
            //        item.transform.localPosition = new Vector3(-boxEdge, pos, posLocal);
            //        break;
            //    //右
            //    case 2:
            //        item.transform.localPosition = new Vector3(posLocal, pos, boxEdge);
            //        break;
            //    case 3:
            //        item.transform.localPosition = new Vector3(posLocal, pos, -boxEdge);
            //        break;
            //}
            item.transform.localPosition = new Vector3(posLocal, pos, posLocal);
        }
        //ここからギミック生成
        for (int i = 0; i < 7; i++)
        {
            GameObject gim;
            int r = Random.Range(0, StageList.Instance.FallGimicList.Length);
            int r2 = Random.Range(0, StageList.Instance.FallGimicList[r].GimicSet.Length);
            gim = Instantiate(StageList.Instance.FallGimicList[r].GimicSet[r2]);
            gim.transform.parent = o.transform;
            gim.transform.localPosition = new Vector3(0, -960 + (i * 150),0 );
        }
    }
}
