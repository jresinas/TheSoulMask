using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingController : MonoBehaviour {
    [SerializeField] Image good;
    [SerializeField] Image neutral;
    [SerializeField] Image evil;

    public float DELAY_BEFORE_ENDING = 1f;
    public float END_FADE_TIME = 2.5f;

    public void EndGame(int end) {
        Image selectedEnding = null;
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

        Utils.instance.Timer(DELAY_BEFORE_ENDING, () => ShowEnd(selectedEnding));
    }

    void ShowEnd(Image endImage) {
        endImage.gameObject.SetActive(true);
        Utils.instance.Fade(endImage, END_FADE_TIME, false);
    }
}
