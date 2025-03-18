using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    void Start()
    {
        //Camera.main.aspect = 10f / 10f;

        // 화면 해상도에 맞는 aspect 비율로 설정
        float screenRatio = (float)Screen.width / (float)Screen.height;
        Camera.main.aspect = screenRatio;
    }
}
