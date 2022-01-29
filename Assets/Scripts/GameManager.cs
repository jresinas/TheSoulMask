using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    public CardController card;
    public ClipboardController clipboard;
    public DreamTextController dreamText;
    [SerializeField] CardScriptableObject startingCard;

    CardScriptableObject currentCard;

    public float CLOSE_EYES_TIME = 0.5f;
    public float OPEN_EYES_TIME = 0.5f;
    public float SLEEP_TIME = 1.5f;
    public float DREAMTEXT_DELAYTIME = 0.25f;
    public float DREAMTEXT_TIME = 1f;

    void Awake() {
        instance = this;
    }

    void Start() {
        card.LoadCard(startingCard);
        currentCard = startingCard;
    }

    public void EndDay(CardScriptableObject cardData) {
        Utils.instance.CloseEyes(CLOSE_EYES_TIME, () => Sleeping(cardData));
    }

    void Sleeping(CardScriptableObject cardData) {
        dreamText.gameObject.SetActive(true);
        Utils.instance.Timer(DREAMTEXT_DELAYTIME, () => dreamText.ShowText(cardData.dreamText, DREAMTEXT_TIME));
        Utils.instance.Timer(SLEEP_TIME, () => Wake(cardData));
        card.LoadCard(cardData);
    }

    void Wake(CardScriptableObject cardData) {
        Utils.instance.OpenEyes(OPEN_EYES_TIME, () => StartDay(cardData));
    }

    void StartDay(CardScriptableObject cardData) {
        currentCard = cardData;
    }
}
