using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {
    public static Utils instance;

    [SerializeField] RectTransform topLid;
    [SerializeField] RectTransform bottomLid;

    float SLEEP_TIME = 1;
    float BLINK_SPEED = 0.5f;


    void Awake() {
        instance = this;    
    }

    
    void Update() {
        if (Input.GetKeyDown("b")) Blink();
        if (Input.GetKeyDown("s")) Sleep();
    }
    

    public void Timer(float time, Action callback = null) {
        StartCoroutine(TimerCoroutine(time, callback));
    }

    IEnumerator TimerCoroutine(float time, Action callback) {
        yield return new WaitForSecondsRealtime(time);
        if (callback != null) callback();
    }

    public void ZoomIn(RectTransform rectTransform, float time, Action callback = null) {
        //rectTransform.localScale = new Vector3(0.1f, 0.1f);
        StartCoroutine(Zoom(rectTransform, new Vector3(0.1f, 0.1f), new Vector3(1f, 1f), time, callback));
    }

    public void ZoomOut(RectTransform rectTransform, float time, Action callback = null) {
        //rectTransform.localScale = new Vector3(1f, 1f);
        StartCoroutine(Zoom(rectTransform, new Vector3(1f, 1f), new Vector3(0.1f, 0.1f), time, callback));
    }

    IEnumerator Zoom(RectTransform rectTransform, Vector2 startSize, Vector2 endSize, float time, Action callback) {
        float t = 0;
        Vector2 scale = startSize;
        while (t < time) {
            scale = Vector2.Lerp(startSize, endSize, t / time);
            rectTransform.localScale = scale;
            t += Time.deltaTime;
            yield return null;
        }
        rectTransform.localScale = endSize;
        if (callback != null) callback();
    }


    public void Move(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float time, Action callback = null) {
        StartCoroutine(MoveAnim(rectTransform, startPos, endPos, time, callback));
    }

    IEnumerator MoveAnim(RectTransform rectTransform, Vector3 startPos, Vector3 endPos, float time, Action callback) {
        float t = 0;
        Vector3 pos = startPos;
        while (t < time) {
            pos = Vector2.Lerp(startPos, endPos, t / time);
            rectTransform.localPosition = pos;
            t += Time.deltaTime;
            yield return null;
        }
        rectTransform.localPosition = endPos;
        if (callback != null) callback();
    }

    
    public void Sleep() {
        CloseEyes(BLINK_SPEED, () => Timer(SLEEP_TIME, () => OpenEyes(BLINK_SPEED)));
    }

    public void Blink() {
        //Move(topLid, topLidStart, topLidEnd, BLINK_SPEED, () => Move(topLid, topLidEnd, topLidStart, BLINK_SPEED));
        //Move(bottomLid, bottomLidStart, bottomLidEnd, BLINK_SPEED, () => Move(bottomLid, bottomLidEnd, bottomLidStart, BLINK_SPEED));
        CloseEyes(BLINK_SPEED, () => OpenEyes(BLINK_SPEED));
    }
    

    public void CloseEyes(float time, Action callback = null) {
        Vector3 topLidStart = new Vector3(0, 810);
        Vector3 topLidEnd = new Vector3(0, 270);
        Vector3 bottomLidStart = new Vector3(0, -810);
        Vector3 bottomLidEnd = new Vector3(0, -270);
        Move(topLid, topLidStart, topLidEnd, time, callback);
        Move(bottomLid, bottomLidStart, bottomLidEnd, time);
    }

    public void OpenEyes(float time, Action callback = null) {
        Vector3 topLidStart = new Vector3(0, 270);
        Vector3 topLidEnd = new Vector3(0, 810);
        Vector3 bottomLidStart = new Vector3(0, -270);
        Vector3 bottomLidEnd = new Vector3(0, -810);
        Move(topLid, topLidStart, topLidEnd, time, callback);
        Move(bottomLid, bottomLidStart, bottomLidEnd, time);
    }
}
