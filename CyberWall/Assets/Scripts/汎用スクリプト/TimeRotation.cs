using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRotation : MonoBehaviour
{
    [SerializeField]
    private float speed = 30;
	void Update ()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + Time.deltaTime * speed);
	}
}
