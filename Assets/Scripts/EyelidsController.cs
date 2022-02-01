using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyelidsController : MonoBehaviour {
    [SerializeField] RectTransform topLid;
    [SerializeField] RectTransform bottomLid;

    public void CloseEyes(float time, Action callback = null) {
        Vector3 topLidStart = new Vector3(0, 2480);
        Vector3 topLidEnd = new Vector3(0, 1240);
        Vector3 bottomLidStart = new Vector3(0, -2480);
        Vector3 bottomLidEnd = new Vector3(0, -1240);
        Utils.instance.Move(topLid, topLidStart, topLidEnd, time, callback);
        Utils.instance.Move(bottomLid, bottomLidStart, bottomLidEnd, time);
    }

    public void OpenEyes(float time, Action callback = null) {
        Vector3 topLidStart = new Vector3(0, 1240);
        Vector3 topLidEnd = new Vector3(0, 2480);
        Vector3 bottomLidStart = new Vector3(0, -1240);
        Vector3 bottomLidEnd = new Vector3(0, -2480);
        Utils.instance.Move(topLid, topLidStart, topLidEnd, time, callback);
        Utils.instance.Move(bottomLid, bottomLidStart, bottomLidEnd, time);
    }
}
