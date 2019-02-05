using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDeath : MonoBehaviour,IEventGimic
{
    public void OnPlayEffect(Collider other = null)
    {
        return;
    }

    public void OnPlayEvent()
    {
        if (PlayerObjectManager.Instance.PlayerObject != null)
        {
            EnergySlider.Instance.ChangePlusEnergyBar(-1000);
            ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * 2, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
            Destroy(
            PlayerObjectManager.Instance.PlayerObject);
        }
    }

    public void OnPlaySound()
    {
        return;
    }
}
