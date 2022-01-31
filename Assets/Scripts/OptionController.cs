using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionController : MonoBehaviour {
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] TMP_FontAsset normalFont;
    [SerializeField] TMP_FontAsset highlightFont;
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
            //textBox.font = highlightFont;
            //textBox.fontStyle = FontStyles.Bold;
            //textBox.fontSize = 42;
            textBox.color = new Color32(85,19,20,255);
            if (altText != "") ShowText(altText);
        } else {
            //textBox.font = normalFont;
            //textBox.fontStyle = FontStyles.Normal;
            //textBox.fontSize = 36;
            //textBox.color = Color.black;
            //ShowText(normalText);
            textBox.color = new Color32(0,0,0,255);
            if (altText != "") ShowText(normalText);
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
