using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMono<SoundManager>
{
    [SerializeField]
    private AudioClip[] breakWall;
    [SerializeField]
    private AudioClip[] getItemEnergy;
    [SerializeField]
    private AudioClip[] changeGrav;
    [SerializeField]
    private AudioClip[] changeGravInput;
    [SerializeField]
    private AudioClip startSound;
    [SerializeField]
    private AudioClip[] coinGet;
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
    public void PlayGetItemEnergy()
    {
        for (int i = 0; i < getItemEnergy.Length; i++)
        {
            source.PlayOneShot(getItemEnergy[i]);
        }
    }
    public void ChangeGravity()
    {
        for (int i = 0; i < changeGrav.Length; i++)
        {
            source.PlayOneShot(changeGrav[i]);
        }
    }
    public void ChangeGravityInput()
    {
        for (int i = 0; i < changeGravInput.Length; i++)
        {
            source.PlayOneShot(changeGravInput[i]);
        }
    }
    public void PlayStartSound()
    {
        source.PlayOneShot(startSound);
    }
    public void PlayGetCoin()
    {
        for (int i = 0; i < coinGet.Length; i++)
        {
            source.PlayOneShot(coinGet[i]);
        }
    }
}
