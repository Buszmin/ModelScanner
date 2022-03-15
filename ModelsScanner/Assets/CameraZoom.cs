using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public void Zoom(float zoomValue)
    {
        transform.position = new Vector3(0, 0, -zoomValue);
    }
}
