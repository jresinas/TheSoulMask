using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBox;
    public bool highlight = false;
    string normalText = "";
    string altText = "";

    public void LoadData(string text, string text2 = "") {
        normalText = text;
        altText = text2;
        ShowText(text);
    }

    public void SetHighlight(bool status) {
        highlight = status;
        if (highlight) {
            textBox.color = Color.red;
            if (altText != "") ShowText(altText);
        } else {
            textBox.color = Color.black;
            ShowText(normalText);
        }
    }

    void ShowText(string txt) {
        textBox.text = txt;
    }

    //public void Hold(float time) {
    //    Utils.instance.Timer(time, () => Close());
    //}

    public void Close() {
        gameObject.SetActive(false);
    }
}
