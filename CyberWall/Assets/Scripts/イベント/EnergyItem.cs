using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyItem : MonoBehaviour,IEventGimic
{
    public void OnPlayEffect(Collider other = null)
    {
        ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.GetItem).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * 2, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
        playerParticle.transform.parent = PlayerObjectManager.Instance.PlayerObject.gameObject.transform;
        return;
    }

    public void OnPlayEvent()
    {
        EnergySlider.Instance.ChangePlusEnergyBar(10);
        Destroy(gameObject);
    }

    public void OnPlaySound()
    {
        SoundManager.Instance.PlayGetItemEnergy();
        return;
    }
}
