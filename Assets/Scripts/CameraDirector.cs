using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirector : MonoBehaviour
{
    public List<VCam> cameras;

    public void Watch(GameObject go)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i].IsGameObjectInView(go))
            {
                Debug.Log(go.name);
                cameras[i].CinemachineVirtualCamera.SetActive(true);
                cameras[i].CinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = go.transform;
            }
            else
            {
                cameras[i].CinemachineVirtualCamera.SetActive(false);
            }
        }
    }

    public void Watch(VCam camera, GameObject gameObject)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] == camera)
            {
                camera.CinemachineVirtualCamera.SetActive(true);
                camera.CinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
            }
            else
            {
                cameras[i].CinemachineVirtualCamera.SetActive(false);
            }
        }
    }
}
