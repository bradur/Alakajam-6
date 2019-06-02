// Date   : 02.06.2019 14:52
// Project: 6th Alakajam!
// Author : bradur

using UnityEngine;
using System.Collections;
using Cinemachine;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera.Follow = player.transform;
    }
}
