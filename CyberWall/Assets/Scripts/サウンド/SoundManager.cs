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
}
