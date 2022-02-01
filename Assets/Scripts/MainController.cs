using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class MainController : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image mainScreen;
    //[SerializeField] Image instructions;
    [SerializeField] Image useHeadphones;
    [SerializeField] TextMeshProUGUI instructions;

    public float MENU_FADEOUT_TIME = 0.5f;
    public float INSTRUCTIONS_DELAYTIME = 1f;
    public float INSTRUCTIONS_FADEIN_TIME = 0.5f;
    public float INSTRUCTIONS_TIME = 8f;
    public float INSTRUCTIONS_FADEOUT_TIME = 0.5f;
    public float HEADPHONES_DELAYTIME = 1f;
    public float HEADPHONES_FADEIN_TIME = 0.5f;
    public float HEADPHONES_TIME = 4f;
    public float HEADPHONES_FADEOUT_TIME = 0.5f;

    int step = 0;
    Coroutine currentStep = null; 


    void Start() {
        AudioManager.Initialize();
        AudioManager.SetProgress(0);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (currentStep != null) StopCoroutine(currentStep);
        switch (step) {
            case 0:
                step = 1;
                HidingMainScreen();
                break;
            case 2:
                step = 3;
                HidingInstructions();
                break; 
            case 4:
                step = 5;
                HidingUseHeadphones();
                break;
        }
    }

    void HidingMainScreen() {
        Fade(mainScreen, MENU_FADEOUT_TIME, true, () => Timer(INSTRUCTIONS_DELAYTIME, () => ShowingInstructions()));
    }

    void ShowingInstructions() {
        instructions.gameObject.SetActive(true);
        FadeText(instructions, INSTRUCTIONS_FADEIN_TIME, false, () => ShowInstructions());
    }

    void ShowInstructions() {
        step = 2;
        currentStep = Timer(INSTRUCTIONS_TIME, () => HidingInstructions());
    }

    void HidingInstructions() {
        FadeText(instructions, INSTRUCTIONS_FADEOUT_TIME, true, () => HideInstructions());
    }

    void HideInstructions() {
        instructions.gameObject.SetActive(false);
        Timer(HEADPHONES_DELAYTIME, () => ShowingUseHeadphones());
    }

    void ShowingUseHeadphones() {
        useHeadphones.gameObject.SetActive(true);
        Fade(useHeadphones, HEADPHONES_FADEIN_TIME, false, () => ShowUseHeadphones());
    }

    void ShowUseHeadphones() {
        step = 4;
        currentStep = Timer(HEADPHONES_TIME, () => HidingUseHeadphones());
    }

    void HidingUseHeadphones() {
        Fade(useHeadphones, HEADPHONES_FADEOUT_TIME, true, () => HideUseHeadphones());
    }

    void HideUseHeadphones() {
        useHeadphones.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
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

    public Coroutine Timer(float time, Action callback = null) {
        return StartCoroutine(TimerCoroutine(time, callback));
    }

    IEnumerator TimerCoroutine(float time, Action callback) {
        yield return new WaitForSecondsRealtime(time);
        if (callback != null) callback();
    }
}
