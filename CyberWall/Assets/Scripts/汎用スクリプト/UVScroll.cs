using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroll : MonoBehaviour
{
    [SerializeField]
    private float speed;
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
        uv.y += Time.deltaTime * speed;
        material.SetTextureOffset("_MainTex", uv);
    }
}
