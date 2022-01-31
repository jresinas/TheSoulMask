using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image mainScreen;
    [SerializeField] Image useHeadphones;

    public float MENU_FADEOUT_TIME = 0.5f;
    public float USE_HEADPHONES_DELAYTIME = 1f;
    public float USE_HEADPHONES_TIME = 5f;
    public float HEADPHONES_FADEIN_TIME = 0.5f;
    public float HEADPHONES_FADEOUT_TIME = 0.5f;

    void Start() {
        AudioManager.Initialize();
        AudioManager.SetProgress(0);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Fade(mainScreen, MENU_FADEOUT_TIME, true, () => Timer(USE_HEADPHONES_DELAYTIME, () => ShowUseHeadphones()));
    }

    void ShowUseHeadphones() {
        useHeadphones.gameObject.SetActive(true);
        Fade(useHeadphones, HEADPHONES_FADEIN_TIME, false, () => UseHeadPhones());
    }

    void UseHeadPhones() {
        Timer(USE_HEADPHONES_TIME, () => Fade(useHeadphones, HEADPHONES_FADEOUT_TIME, true, () => SceneManager.LoadScene(1)));
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

    public void Timer(float time, Action callback = null) {
        StartCoroutine(TimerCoroutine(time, callback));
    }

    IEnumerator TimerCoroutine(float time, Action callback) {
        yield return new WaitForSecondsRealtime(time);
        if (callback != null) callback();
    }
}
