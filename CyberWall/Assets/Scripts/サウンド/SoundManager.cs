using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMono<SoundManager>
{
    [SerializeField]
    private AudioClip[] breakWall;
    AudioSource source;
	void Start ()
    {
        source = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
	}
    public void PlayBreakWall()
    {
        for (int i = 0; i < breakWall.Length; i++)
        {
            source.PlayOneShot(breakWall[i]);
        }
    }
}
