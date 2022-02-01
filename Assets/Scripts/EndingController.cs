using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingController : MonoBehaviour, IPointerClickHandler {
    [SerializeField] Image good;
    [SerializeField] Image neutral;
    [SerializeField] Image evil;
    [SerializeField] TextMeshProUGUI creditsTitle;
    [SerializeField] TextMeshProUGUI credits;
    Image selectedEnding = null;
    int step = 0;

    public float DELAY_BEFORE_END = 1f;
    public float END_FADEIN_TIME = 2.5f;
    public float END_FADEOUT_TIME = 2.5f;
    public float CREDITS_DELAYTIME = 1f;
    public float CREDITS_FADEIN_TIME = 1f;
    public float CREDITS_FADEOUT_TIME = 1f;

    public void EndGame(int end) {
        selectedEnding = null;
        switch (end) {
            // Good
            case 0:
                selectedEnding = good;
                break;
            // Neutral
            case 1:
                selectedEnding = neutral;
                break;
            // Evil
            case 2:
                selectedEnding = evil;
                break;
        }

        Utils.instance.Timer(DELAY_BEFORE_END, () => ShowEnd(end));
    }

    void ShowEnd(int end) {
        selectedEnding.gameObject.SetActive(true);
        AudioManager.SetProgress(end + 7);
        Utils.instance.Fade(selectedEnding, END_FADEIN_TIME, false);
    }

    public void OnPointerClick(PointerEventData eventData) {
        switch (step) {
            case 0:
                step = 1;
                HidingEnd();
                break;
            case 2:
                step = 3;
                HidingCredits();
                break;
        }
        
    }

    void HidingEnd() {
        Utils.instance.Fade(selectedEnding, END_FADEOUT_TIME, true, () => HideEnd());
    }

    void HideEnd() {
        creditsTitle.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        Utils.instance.Timer(CREDITS_DELAYTIME, () => ShowingCredits());
    }

    void ShowingCredits() {
        AudioManager.SetProgress(10);
        Utils.instance.FadeText(creditsTitle, CREDITS_FADEIN_TIME, false, () => ShowCredits());
        Utils.instance.FadeText(credits, CREDITS_FADEIN_TIME, false, () => ShowCredits());
    }

    void ShowCredits() {
        step = 2;
    }

    void HidingCredits() {
        Utils.instance.FadeText(creditsTitle, CREDITS_FADEOUT_TIME, true, () => HideCredits());
        Utils.instance.FadeText(credits, CREDITS_FADEOUT_TIME, true, () => HideCredits());
    }

    void HideCredits() {
        SceneManager.LoadScene(0);
    }
}
