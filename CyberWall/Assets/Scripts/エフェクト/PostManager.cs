using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostManager : MonoBehaviour
{
    BloomModel.Settings settings;
    PostProcessingBehaviour behaviour;
    [SerializeField]
    float nomalIntensity = 0.3f;
    [SerializeField]
    float minusIntensity = 3;
    void Start ()
    {
        behaviour = Camera.main.GetComponent<PostProcessingBehaviour>();
        settings = behaviour.profile.bloom.settings;
        settings.bloom.intensity = nomalIntensity;
        behaviour.profile.bloom.settings = settings;
    }

    void Update ()
    {
        settings = behaviour.profile.bloom.settings;
        if (settings.bloom.intensity>nomalIntensity)
        {
            settings.bloom.intensity -= Time.deltaTime*minusIntensity;
        }
        behaviour.profile.bloom.settings = settings;
    }
}
