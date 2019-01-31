using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakRedWall : MonoBehaviour,IEventGimic
{
    [SerializeField]
    private float minusBreakSpeed = 90;
    public void OnPlayEffect(Collider other = null)
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
    public void OnPlayEvent()
    {
        PlayerManager.Instance.PlusSpeedChange(-minusBreakSpeed);
        PlayerManager.Instance.StartDamage();
    }
    public void OnPlaySound()
    {
        SoundManager.Instance.PlayBreakWall();
    }
}
