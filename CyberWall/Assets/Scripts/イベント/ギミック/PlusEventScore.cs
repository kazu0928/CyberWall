using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusEventScore : MonoBehaviour, IEventGimic
{
    public void OnPlayEffect(Collider other = null)
    {
        return;
    }

    public void OnPlayEvent()
    {
        if (PlusScore.Instance.plusScore == 0)
        {
            PlusScore.Instance.plusScore = 100;
        }
        else
        {
            PlusScore.Instance.plusScore *= 2;
        }
        return;
    }

    public void OnPlaySound()
    {
        SoundManager.Instance.PlayGetItemEnergy();
        return;
    }

}
