using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    WallBreakRed,
    PlayerDamage,
    GetItem,
}

public class EffectList : SingletonMono<EffectList>
{
    [SerializeField]
    private ParticleSystem wallBreakRedEffect;
    [SerializeField]
    private ParticleSystem playerDamageEffect;
    [SerializeField]
    private ParticleSystem itemGetEffect;
    public ParticleSystem GetEffect(EffectType type)
    {
        switch (type)
        {
            case EffectType.WallBreakRed:
                return wallBreakRedEffect;
            case EffectType.PlayerDamage:
                return playerDamageEffect;
            case EffectType.GetItem:
                return itemGetEffect;
        }
        return null;
    }
}
