using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    WallBreakRed,
    PlayerDamage,
}

public class EffectList : SingletonMono<EffectList>
{
    [SerializeField]
    private ParticleSystem wallBreakRedEffect;
    [SerializeField]
    private ParticleSystem playerDamageEffect;
    public ParticleSystem GetEffect(EffectType type)
    {
        switch(type)
        {
            case EffectType.WallBreakRed:
                return wallBreakRedEffect;
            case EffectType.PlayerDamage:
                return playerDamageEffect;
        }
        return null;
    }
}
