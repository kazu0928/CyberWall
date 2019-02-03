using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    NomalTPS,       //通常のTPS視点、マウス移動あり、プレイヤーの向き追従なし
    FixedTPS,       //固定カメラ
    TitleCamera,    //タイトル用カメラワーク
    FixedEndressTPS,
    FixedFallEndress,
}