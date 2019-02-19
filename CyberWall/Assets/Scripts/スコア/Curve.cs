using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    VacuumShaders.CurvedWorld.CurvedWorld_Controller controller;
    private int one = 1;
    private int one2 = 1;
    // Use this for initialization
    void Start () {
        one = 1;
        controller = GetComponent<VacuumShaders.CurvedWorld.CurvedWorld_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs( controller._V_CW_Bend_Y)>1)
        {
            controller._V_CW_Bend_Y = 1 * Mathf.Sign(controller._V_CW_Bend_Y);
            one *= -1;
        }

        controller._V_CW_Bend_Y += one * Time.deltaTime*0.2f;
        if (Mathf.Abs(controller._V_CW_Bend_X) > 1)
        {
            controller._V_CW_Bend_X = 1 * Mathf.Sign(controller._V_CW_Bend_X);
            one2 *= -1;
        }
        controller._V_CW_Bend_X += one2 * Time.deltaTime * 0.4f;
    }
}
