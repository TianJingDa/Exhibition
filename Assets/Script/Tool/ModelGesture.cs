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
            transform.Translate(Vector3.right * current.deltaPosition.x / Screen.width * swipeSensibility, Space.World);
            transform.Translate(Vector3.forward * current.deltaPosition.y / Screen.height * swipeSensibility, Space.World);
        }

        // Twist
        if (current.type == EasyTouch.EvtType.On_Twist)
        {
            transform.Rotate(-Vector3.up * current.twistAngle, Space.World);
        }

        // Pinch
        if (current.type == EasyTouch.EvtType.On_Pinch)
        {
            Vector3 scale = transform.localScale + Vector3.one * current.deltaPinch * Time.deltaTime * pinchSensibility;
            if (scale.x > maxScale * initScale.x
            || scale.y > maxScale * initScale.y
            || scale.z > maxScale * initScale.z)
            {
                transform.localScale = maxScale * initScale;
                return;
            }
            if (scale.x < minScale * initScale.x
            || scale.y < minScale * initScale.y
            || scale.z < minScale * initScale.z)
            {
                transform.localScale = minScale * initScale;
                return;
            }
            transform.localScale += Vector3.one * current.deltaPinch * Time.deltaTime * pinchSensibility;
        }
    }
}
