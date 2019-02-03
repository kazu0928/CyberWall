using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoveAndCamera : MonoBehaviour,IEventGimic
{
    public enum StartEnd
    {
        Start,
        End,
        Fall,
        FallStart,
        FallEnd,
    }
    [SerializeField]
    private CameraParameter changeCameraParameter;
    [SerializeField]
    private StageMode changeStage;
    [SerializeField]
    private StartEnd startOrEnd;
    public void OnPlayEffect(Collider other = null)
    {
        return;
    }

    public void OnPlayEvent()
    {
        MainCameraManager.Instance.InitCameraParameterChange(changeCameraParameter);
        PlayerManager.Instance.ChangeStageMode(changeStage);
        PlayerManager.Instance.ChangeGravityModeNomal(GravityMode.Down);
        if(startOrEnd==StartEnd.End)
        {
            PlayerManager.Instance.SaveSpeedChange(10);
        }
        if(startOrEnd==StartEnd.Start)
        {
            PlayerManager.Instance.SaveSpeedReflection();
            GameManager.Instance.RandomNextCreateMode(changeStage);
        }
        if(startOrEnd == StartEnd.FallStart)
        {
            PlayerManager.Instance.SaveSpeedReflection();
            GameManager.Instance.PlusYPoint(-1005);
            GameManager.Instance.PlusZPoint(-500);
            GameManager.Instance.RandomNextCreateMode(StageMode.Fall);
        }
        if(startOrEnd == StartEnd.Fall)
        {
            PlayerManager.Instance.SaveSpeedReflection();
        }
        if(startOrEnd == StartEnd.FallEnd)
        {
            PlayerManager.Instance.SaveSpeedChange(0);
            GameManager.Instance.cameraYPoint -= 1005;
            GameManager.Instance.cameraZPoint -= 500;
        }
        Destroy(gameObject);
    }

    public void OnPlaySound()
    {
        return;
    }
}
