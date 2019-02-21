using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {
    [SerializeField]
    private bool nineFlag=false;
	// Use this for initialization
	void Start () {
        if(nineFlag==true)
        {
            int r = Random.Range(0, 4);
            transform.rotation = Quaternion.Euler(0, 0, r*90);
            return;
        }
        int ra = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, 0, ra);
	}
}
