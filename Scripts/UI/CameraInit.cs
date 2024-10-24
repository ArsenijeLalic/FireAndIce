using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInit : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Awake()
    {
        virtualCamera.Follow = GameObject.Find("MousePointer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
