using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRedWall : MonoBehaviour,IEventGimic
{
    void IEventGimic.OnPlayEffect(Collider other = null)
    {
        if(other==null)
        {
            return;
        }
        // あたった場所
        Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
        ParticleSystem particle = Instantiate(EffectList.Instance.GetEffect(EffectType.WallBreakRed).gameObject, hitPos, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
        ParticleSystem playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, PlayerObjectManager.Instance.TiltObject.transform.position + PlayerObjectManager.Instance.TiltObject.transform.up * 2, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
        playerParticle.transform.parent = other.gameObject.transform;
        Destroy(other.gameObject);
        particle.Play();
    }
    void IEventGimic.OnPlayEvent()
    {
        PlayerManager.Instance.PlusSpeedChange(-90);
        PlayerManager.Instance.StartDamage();
    }
    void IEventGimic.OnPlaySound()
    {
        SoundManager.Instance.PlayBreakWall();
    }
}
