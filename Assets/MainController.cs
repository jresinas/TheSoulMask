using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image mainScreen;

    public float MENU_FADE_TIME = 0.5f;

    void Start() {
        AudioManager.Initialize();
        AudioManager.SetProgress(0);
    }

    public void OnPointerClick(PointerEventData eventData) {
        Fade(mainScreen, MENU_FADE_TIME, true, () => SceneManager.LoadScene(1));
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
}
