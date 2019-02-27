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
        if (startOrEnd == StartEnd.Start)
        {
            ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * -1, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
            playerParticle.transform.parent = PlayerObjectManager.Instance.PlayerObject.gameObject.transform;
        }
        if (startOrEnd == StartEnd.FallStart)
        {
            ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * -1, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
            playerParticle.transform.parent = PlayerObjectManager.Instance.PlayerObject.gameObject.transform;
        }

    }

    public void OnPlayEvent()
    {
        MainCameraManager.Instance.InitCameraParameterChange(changeCameraParameter);
        PlayerManager.Instance.ChangeStageMode(changeStage);
        PlayerManager.Instance.ChangeGravityModeNomal(GravityMode.Down);
        if (startOrEnd == StartEnd.End)
        {
            PlayerManager.Instance.SaveSpeedChange(10);
            SoundManager.Instance.ChangeGravity();
            EnergySlider.Instance.ChangePlusEnergyBar(10);
            PlusScore.Instance.PlusScoreDirect(500);
            int a = Random.Range(0, 2);
            PlayerManager.Instance.PlusUpSpeedAndMax(Random.Range(0, 0.09f), 0);

            PlayerManager.Instance.PlusUpSpeedAndMax(0, Random.Range(0, 10.0f));
        }
        if (startOrEnd==StartEnd.Start)
        {
            PlayerManager.Instance.SaveSpeedReflection();
            GameManager.Instance.RandomNextCreateMode(changeStage);
            PlayerManager.Instance.fallPrev = false;
        }
        if (startOrEnd == StartEnd.FallStart)
        {
            PlayerManager.Instance.SaveSpeedReflection();
            GameManager.Instance.PlusYPoint(-1005);
            GameManager.Instance.PlusZPoint(-500);
            GameManager.Instance.RandomNextCreateMode(StageMode.Fall);
            PlayerManager.Instance.fallPrev = true;
        }
        if(startOrEnd == StartEnd.Fall)
        {
            PlayerManager.Instance.SaveSpeedReflection();
            PlayerManager.Instance.fallPrev = false;
        }
        if (startOrEnd == StartEnd.FallEnd)
        {
            PlayerManager.Instance.SaveSpeedChange(0);
            GameManager.Instance.cameraYPoint -= 1005;
            GameManager.Instance.cameraZPoint -= 500;
            SoundManager.Instance.ChangeGravity();
            EnergySlider.Instance.ChangePlusEnergyBar(10);
            PlusScore.Instance.PlusScoreDirect(500);
            int a = Random.Range(0, 2);
            PlayerManager.Instance.PlusUpSpeedAndMax(Random.Range(0.01f, 0.05f), 0);
            PlayerManager.Instance.PlusUpSpeedAndMax(0, Random.Range(0.5f, 6f));
            PlayerManager.Instance.fallPrev = false ;
        }
        Destroy(gameObject);
    }

    public void OnPlaySound()
    {
        return;
    }
}
