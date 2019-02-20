using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int ra = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, 0, ra);
	}
}
