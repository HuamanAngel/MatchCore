using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemacticCustom : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineVirtualCamera cmv;
    private CinemachineTrackedDolly cmtd;
    private float theValue = 0.0f;
    public float increment = 0.1f;
    void Start()
    {
        cmv = GetComponent<CinemachineVirtualCamera>();
        cmtd = cmv.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    // Update is called once per frame
    void Update()
    {
        cmtd.m_PathPosition = theValue;
        theValue += increment;
    }
}
