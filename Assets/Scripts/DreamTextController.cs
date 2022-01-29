using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DreamTextController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBox;

    public void ShowText(string text, float time) {
        textBox.text = text;
        Utils.instance.Timer(time, () => HideText());
    }

    public void HideText() {
        textBox.text = "";
        gameObject.SetActive(false);
    }
}
