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
    [SerializeField] CardScriptableObject startingCard;
    //CardScriptableObject currentCard;
    public int score = 0;
    public Dictionary<int, CardOption> masterSlave = new Dictionary<int, CardOption>();


    public float CLOSE_EYES_TIME = 0.5f;
    public float OPEN_EYES_TIME = 0.5f;
    public float SLEEP_TIME = 1.5f;
    public float DREAMTEXT_DELAYTIME = 0.25f;
    public float DREAMTEXT_TIME = 1f;

    void Awake() {
        instance = this;
        AudioManager.Initialize();
    }

    void Start() {
        card.LoadCard(startingCard);
        clipboard.LoadData(startingCard);
        //currentCard = startingCard;
    }

    public void EndDay(CardScriptableObject cardData) {
        if (cardData != null) {
            AudioManager.SetStatus(cardData.altPicture ? 1 : 0);
            AudioManager.SetTime(1);
        }
        Utils.instance.CloseEyes(CLOSE_EYES_TIME, () => Sleeping(cardData));
    }

    void Sleeping(CardScriptableObject cardData) {
        dreamText.gameObject.SetActive(true);
 
        if (cardData != null) {
            Utils.instance.Timer(DREAMTEXT_DELAYTIME, () => dreamText.ShowText(cardData.dreamText, DREAMTEXT_TIME));
            Utils.instance.Timer(SLEEP_TIME, () => Wake(cardData));
        } else {
            EndGame();
        }
    }

    void Wake(CardScriptableObject cardData) {
        card.LoadCard(cardData);
        clipboard.LoadData(cardData);
        //currentCard = cardData;
        blackPanel.SetActive(false);
        redPanel.SetActive(false);
        Utils.instance.OpenEyes(OPEN_EYES_TIME, () => StartDay(cardData));
        AudioManager.SetTime(0);
    }

    void StartDay(CardScriptableObject cardData) {
    }

    void EndGame() {

    }
}
