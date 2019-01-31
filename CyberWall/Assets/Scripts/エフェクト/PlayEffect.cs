using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO インターフェース化
public class PlayEffect : MonoBehaviour
{
    [SerializeField]
    private EffectType effectType;
    private ParticleSystem particle;
    private ParticleSystem playerParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (effectType)
            {
                case EffectType.WallBreakRed:
                    Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
                    particle = Instantiate(EffectList.Instance.GetEffect(effectType).gameObject, hitPos, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
                    playerParticle = Instantiate(EffectList.Instance.GetEffect(EffectType.PlayerDamage).gameObject, other.transform.GetChild(0).transform.position + other.transform.up * 2, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
                    playerParticle.transform.parent = other.gameObject.transform;
                    Destroy(gameObject);
                    particle.Play();
                    break;
            }
                
        }
    }
}
