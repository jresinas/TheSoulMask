using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DreamTextController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBox;

    public float DREAMTEXT_FADEIN_TIME = 1f;
    public float DREAMTEXT_FADEOUT_TIME = 1f;

    public void SetText(string text, float time) {
        Utils.instance.Timer(time, () => HidingText());
        textBox.text = text;
        ShowingText();
    }

    void ShowingText() {
        Utils.instance.FadeText(textBox, DREAMTEXT_FADEIN_TIME, false);
    }

    void HidingText() {
        Utils.instance.FadeText(textBox, DREAMTEXT_FADEOUT_TIME, true, () => HideText());
    }

    void HideText() {
        textBox.text = "";
        gameObject.SetActive(false);
    }
}
