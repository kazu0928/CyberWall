using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallWall : MonoBehaviour
{
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(PlayerObjectManager.Instance.PlayerObject==null)
        {
            return;
        }
        rb.AddForce(-PlayerObjectManager.Instance.PlayerObject.transform.up.normalized *
PlayerManager.Instance.PlayerParam.StartGravitySpeed - PlayerObjectManager.Instance.PlayerObject.transform.up.normalized *
Time.deltaTime * 60 * PlayerManager.Instance.PlayerParam.GravitySpeed, ForceMode.Acceleration);
    }
}
