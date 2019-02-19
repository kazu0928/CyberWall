using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRotation : MonoBehaviour
{
    public enum dir
    {
        Z,
        X,
        Y,
    }
    [SerializeField]
    private float speed = 30;
    [SerializeField]
    private dir d = dir.Z;
	void Update ()
    {
        if (d == dir.Z)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + Time.deltaTime * speed);
        }
        else if(d==dir.X)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Time.deltaTime * speed, transform.rotation.eulerAngles.z);
        }
    }
}
