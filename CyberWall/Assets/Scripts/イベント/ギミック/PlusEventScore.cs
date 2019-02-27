using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlusEventScore : MonoBehaviour, IEventGimic
{
    [SerializeField]
    private float intens=2.7f;
    public void OnPlayEffect(Collider other = null)
    {
        PostProcessingBehaviour behaviour = Camera.main.GetComponent<PostProcessingBehaviour>();
        BloomModel.Settings settings = Camera.main.GetComponent<PostProcessingBehaviour>().profile.bloom.settings;
         settings.bloom.intensity = intens;
        behaviour.profile.bloom.settings = settings;
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
            if (PlusScore.Instance.plusScore >= 1000)
            {
                int score = PlusScore.Instance.plusScore;
                PlusScore.Instance.resetScorePlus();
                PlusScore.Instance.plusScore = score;
            }
            else
            {
                PlusScore.Instance.plusScore += 100;
            }
        }
        return;
    }

    public void OnPlaySound()
    {
        SoundManager.Instance.PlayGetItemEnergy();
        return;
    }

}
