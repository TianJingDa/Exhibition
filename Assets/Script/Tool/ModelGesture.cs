using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

[DisallowMultipleComponent]
public class ModelGesture : MonoBehaviour
{
    private float swipeSensibility = 30;
    private float pinchSensibility = 0.05f;
    private float maxScale = 3;
    private float minScale = 0.5f;
    private Vector3 initScale;
    private Vector3 horizontal;
    private Vector3 vertical;
    private StateID curStateID;

    void Start()
    {
        initScale = transform.localScale;
        curStateID = GameManager.Instance.GetCurStateID();
    }

    void Update ()
    {
        Gesture current = EasyTouch.current;

        if (curStateID == StateID.CompareState)
        {
            float startX = current.startPosition.x / Screen.width;
            if ((startX >= 0.5 && gameObject.tag == "MainModel") || (startX <= 0.5 && gameObject.tag == "ViceModel"))
                return;
        }

        // Swipe
        if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1)
        {
            horizontal = transform.InverseTransformDirection(Vector3.right);
            vertical = transform.InverseTransformDirection(Vector3.forward);
            transform.Translate(horizontal * current.deltaPosition.x / Screen.width * swipeSensibility);
            transform.Translate(vertical * current.deltaPosition.y / Screen.height * swipeSensibility);
        }

        // Twist
        if (current.type == EasyTouch.EvtType.On_Twist)
        {
            transform.Rotate(-Vector3.up * current.twistAngle, Space.World);
        }

        // Pinch
        if (current.type == EasyTouch.EvtType.On_Pinch)
        {
            if (current.deltaPinch > 0 && (transform.localScale.x > maxScale * initScale.x
            ||  transform.localScale.y > maxScale * initScale.y
            ||  transform.localScale.z > maxScale * initScale.z))
            {
                transform.localScale = maxScale * initScale;
                return;
            }
            if (current.deltaPinch < 0 && (transform.localScale.x < minScale * initScale.x
            || transform.localScale.y < minScale * initScale.y
            || transform.localScale.z < minScale * initScale.z))
            {
                transform.localScale = minScale * initScale;
                return;
            }
            transform.localScale += Vector3.one * current.deltaPinch * Time.deltaTime * pinchSensibility;
        }
    }
}
