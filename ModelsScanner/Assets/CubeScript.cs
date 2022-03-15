using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeScript : MonoBehaviour
{
    //Drag and rotate
    bool firstFrameOfDragOnUi; //fix error when u can rotate when using zoom slider
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    Transform parent;

    //sliders rotation (not used in final version)
    Vector3 sliderRotationVector;
    bool rotate;

    private void Start()
    {
        parent = transform.parent;
    }

    private void Update()
    {
        //Drag and rotate
        EventSystem eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        if(Input.GetMouseButtonDown(0))
        {
            if (eventSys.IsPointerOverGameObject())
            {
                firstFrameOfDragOnUi = true;
            }
            else
            {
                firstFrameOfDragOnUi = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (firstFrameOfDragOnUi == false)
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                Vector3 mPosDeltaNormal = mPosDelta.normalized;
                Vector3 axis = Vector3.Cross(-Vector3.forward , mPosDeltaNormal);
                parent.rotation = parent.rotation * Quaternion.AngleAxis(mPosDelta.magnitude * 0.35f,axis);
                Quaternion tempRot = transform.rotation;
                parent.rotation = Quaternion.identity;
                transform.rotation = tempRot;
            }
        }

        mPrevPos = Input.mousePosition;

        //sliders rotation (not used in final version)
        if (rotate)
        {
            transform.localEulerAngles = sliderRotationVector;
            rotate = false;
        }
    }

    //sliders rotation (not used in final version)
    public void RotateY(float rotation)
    {
        sliderRotationVector.y = rotation;
        rotate = true;
    }

    public void RotateX(float rotation)
    {
        sliderRotationVector.x = rotation;
        rotate = true;
    }

    public void RotateZ(float rotation)
    {
        sliderRotationVector.z = rotation;
        rotate = true;
    }
}
