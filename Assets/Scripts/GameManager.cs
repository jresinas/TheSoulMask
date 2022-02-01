using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardOption {
    left, right
}

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public CardController card;
    public ClipboardController clipboard;
    public DreamTextController dreamText;
    public GameObject blackPanel;
    public GameObject redPanel;
    public EndingController ending;
    [SerializeField] CardScriptableObject startingCard;

    public int score = 0;
    int dayCount = 0;
    public int dayViolentCount = 0;
    public Dictionary<int, CardOption> masterSlave = new Dictionary<int, CardOption>();

    public float START_GAME_DELAYTIME = 2f;
    public float CLOSE_EYES_TIME = 0.5f;
    public float OPEN_EYES_TIME = 0.5f;
    public float SLEEP_TIME = 1.5f;
    public float DREAMTEXT_DELAYTIME = 0.25f;
    public float DREAMTEXT_TIME = 1f;

    void Awake() {
        instance = this;
        //AudioManager.Initialize();
    }

    void Start() {
        AudioManager.SetProgress(1);
        card.LoadCard(startingCard);
        clipboard.LoadData(startingCard);
        Utils.instance.Timer(START_GAME_DELAYTIME, () => Utils.instance.OpenEyes(OPEN_EYES_TIME));
    }

    public void EndDay(CardScriptableObject cardData) {
        dayCount++;

        if (cardData != null && !IsEncounter()) {
            if (cardData.altPicture != null) dayViolentCount++;
            AudioManager.SetStatus(cardData.altPicture ? 1 : 0);
            AudioManager.SetTime(1);
        }

        Utils.instance.CloseEyes(CLOSE_EYES_TIME, () => Sleeping(cardData));
    }

    void Sleeping(CardScriptableObject cardData) {

        if (cardData != null && !IsEncounter()) {
            dreamText.gameObject.SetActive(true);
            Utils.instance.Timer(DREAMTEXT_DELAYTIME, () => dreamText.ShowText(cardData.dreamText, DREAMTEXT_TIME));
            Utils.instance.Timer(SLEEP_TIME, () => Wake(cardData));
        } else if (cardData != null && IsEncounter()) {
            Wake(cardData);
        } else {
            EndGame();
        }
    }

    void Wake(CardScriptableObject cardData) {
        card.LoadCard(cardData);
        clipboard.LoadData(cardData);
        blackPanel.SetActive(false);
        Utils.instance.OpenEyes(OPEN_EYES_TIME, () => StartDay(cardData));
        if (!IsEncounter()) {
            AudioManager.SetTime(0);
            if (cardData.altPicture != null) {
                if (dayViolentCount == 1) redPanel.SetActive(false);
                else redPanel.SetActive(true);
                AudioManager.SetProgress(dayViolentCount);
            } else {
                redPanel.SetActive(false);
                AudioManager.SetProgress(Mathf.FloorToInt((dayCount - dayViolentCount)/2));
            }
        } else {
            redPanel.SetActive(true);
            AudioManager.SetProgress(4);
        }
    }

    void StartDay(CardScriptableObject cardData) {
    }

    void EndGame() {
        ending.EndGame(score);
    }

    bool IsEncounter() {
        return dayCount > 10;
    }
}
