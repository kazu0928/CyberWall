using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    enum XY
    {
        X,
        Y,
    }
    [SerializeField]
    private float speed;
    [SerializeField]
    private XY xY = XY.Y;
    private Material material;
    private Vector2 uv;

    private void Start()
    {
        if (GetComponent<LineRenderer>())
        {
            material = GetComponent<LineRenderer>().material;
        }
        else
        {
            material = GetComponent<MeshRenderer>().material;
        }
        uv = Vector2.zero;
    }

    private void Update()
    {
        if (XY.Y == xY)
        {
            uv.y += Time.deltaTime * speed;
        }
        if(XY.X == xY)
        {
            uv.x += Time.deltaTime * speed;
        }
        material.SetTextureOffset("_MainTex", uv);
    }
}
