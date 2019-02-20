using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour ,IEventGimic
{
    public void OnPlayEffect(Collider other = null)
    {
    }

    public void OnPlayEvent()
    {
        PlusScore.Instance.plusScore += 50;
        Destroy(gameObject);
    }

    public void OnPlaySound()
    {
        SoundManager.Instance.PlayGetCoin();
    }
}
