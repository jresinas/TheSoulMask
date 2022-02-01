using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Utils : MonoBehaviour {
    public static Utils instance;

    void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Coroutine Timer(float time, Action callback = null) {
        return StartCoroutine(TimerCoroutine(time, callback));
    }

    IEnumerator TimerCoroutine(float time, Action callback) {
        yield return new WaitForSecondsRealtime(time);
        if (callback != null) callback();
    }

    public void ZoomIn(RectTransform rectTransform, float time, Action callback = null) {
        StartCoroutine(Zoom(rectTransform, new Vector3(0.1f, 0.1f), new Vector3(1f, 1f), time, callback));
    }

    public void ZoomOut(RectTransform rectTransform, float time, Action callback = null) {
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

    public void Fade(Image image, float time, bool fadeOut = true, Action callback = null) {
        StartCoroutine(FadeOut(image, time, fadeOut, callback));
    }

    IEnumerator FadeOut(Image image, float time, bool fadeOut, Action callback = null) {
        Color color = image.color;
        for (float f = 0; f <= time; f += Time.deltaTime) {
            if (image.gameObject == null) break;
            if (fadeOut) color.a = Mathf.Lerp(1f, 0f, f / time);
            else color.a = Mathf.Lerp(0f, 1f, f / time);
            image.color = color;
            yield return null;
        }
        if (callback != null) callback();
    }

    public void FadeText(TextMeshProUGUI image, float time, bool fadeOut = true, Action callback = null) {
        StartCoroutine(FadeOutText(image, time, fadeOut, callback));
    }

    IEnumerator FadeOutText(TextMeshProUGUI image, float time, bool fadeOut, Action callback = null) {
        Color color = image.color;
        for (float f = 0; f <= time; f += Time.deltaTime) {
            if (image.gameObject == null) break;
            if (fadeOut) color.a = Mathf.Lerp(1f, 0f, f / time);
            else color.a = Mathf.Lerp(0f, 1f, f / time);
            image.color = color;
            yield return null;
        }
        if (callback != null) callback();
    }
}
