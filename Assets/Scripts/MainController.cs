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
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] Image useHeadphones;

    public float MENU_FADEIN_TIME = 0.5f;
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
        ShowingMainScreen();
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

    void ShowingMainScreen() {
        Utils.instance.Fade(mainScreen, MENU_FADEIN_TIME, false);
    }

    void HidingMainScreen() {
        Utils.instance.Fade(mainScreen, MENU_FADEOUT_TIME, true, () => HideMainScreen());
    }

    void HideMainScreen() {
        Utils.instance.Timer(INSTRUCTIONS_DELAYTIME, () => ShowingInstructions());
    }

    void ShowingInstructions() {
        instructions.gameObject.SetActive(true);
        Utils.instance.FadeText(instructions, INSTRUCTIONS_FADEIN_TIME, false, () => ShowInstructions());
    }

    void ShowInstructions() {
        step = 2;
        currentStep = Utils.instance.Timer(INSTRUCTIONS_TIME, () => HidingInstructions());
    }

    void HidingInstructions() {
        Utils.instance.FadeText(instructions, INSTRUCTIONS_FADEOUT_TIME, true, () => HideInstructions());
    }

    void HideInstructions() {
        instructions.gameObject.SetActive(false);
        Utils.instance.Timer(HEADPHONES_DELAYTIME, () => ShowingUseHeadphones());
    }

    void ShowingUseHeadphones() {
        useHeadphones.gameObject.SetActive(true);
        Utils.instance.Fade(useHeadphones, HEADPHONES_FADEIN_TIME, false, () => ShowUseHeadphones());
    }

    void ShowUseHeadphones() {
        step = 4;
        currentStep = Utils.instance.Timer(HEADPHONES_TIME, () => HidingUseHeadphones());
    }

    void HidingUseHeadphones() {
        Utils.instance.Fade(useHeadphones, HEADPHONES_FADEOUT_TIME, true, () => HideUseHeadphones());
    }

    void HideUseHeadphones() {
        useHeadphones.gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
