using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoveAndCamera : MonoBehaviour,IEventGimic
{
    [SerializeField]
    private CameraParameter changeCameraParameter;
    [SerializeField]
    private StageMode changeStage;
    public void OnPlayEffect(Collider other = null)
    {
        return;
    }

    public void OnPlayEvent()
    {
        MainCameraManager.Instance.InitCameraParameterChange(changeCameraParameter);
        PlayerManager.Instance.ChangeStageMode(changeStage);
    }

    public void OnPlaySound()
    {
        return;
    }
}
