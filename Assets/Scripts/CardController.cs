using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    //[SerializeField] ClipboardController clipboard;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] TextMeshProUGUI cardText;
    [SerializeField] OptionController leftOption;
    [SerializeField] OptionController rightOption;
    public float THRESHOLD = 400;
    public float MAX_MOVEMENT = 600;
    public float ZOOM_CARD_TIME = 0.25f;
    public float TIME_BEFORE_SLEEP = 1f;

    float offset;

    CardScriptableObject cardData;

    public void LoadCard(CardScriptableObject card) {
        cardData = card;
        cardText.text = card.text;
        leftOption.gameObject.SetActive(false);
        rightOption.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        offset = rectTransform.position.x - Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.position = new Vector2(Input.mousePosition.x + offset, rectTransform.position.y);
        if (rectTransform.localPosition.x > MAX_MOVEMENT) rectTransform.localPosition = new Vector2(MAX_MOVEMENT, rectTransform.localPosition.y);
        else if (rectTransform.localPosition.x < -MAX_MOVEMENT) rectTransform.localPosition = new Vector2(-MAX_MOVEMENT, rectTransform.localPosition.y);

        if (rectTransform.localPosition.x >= THRESHOLD) rightOption.SetHighlight(true);
        else rightOption.SetHighlight(false);
        if (rectTransform.localPosition.x <= -THRESHOLD) leftOption.SetHighlight(true);
        else leftOption.SetHighlight(false);
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log(rectTransform.position);
        if (rectTransform.localPosition.x >= THRESHOLD) SelectRight();
        else if (rectTransform.localPosition.x <= -THRESHOLD) SelectLeft();
        else ReturnStartPosition();

        leftOption.SetHighlight(false);
        rightOption.SetHighlight(false);
    }

    void SelectRight() {
        CloseCard();
        leftOption.Close();
        GameManager.instance.score += cardData.rightOptionValue;
        Utils.instance.Timer(TIME_BEFORE_SLEEP, () => GameManager.instance.EndDay(cardData.rightOption));
    }

    void SelectLeft() {
        CloseCard();
        rightOption.Close();
        GameManager.instance.score += cardData.leftOptionValue;
        Utils.instance.Timer(TIME_BEFORE_SLEEP, () => GameManager.instance.EndDay(cardData.leftOption));
    }

    void ReturnStartPosition() {
        rectTransform.localPosition = Vector3.zero;
    }

    public void OpenCard() {
        rectTransform.localPosition = Vector3.zero;
        Utils.instance.ZoomIn(rectTransform, ZOOM_CARD_TIME, () => CardOpened());
    }

    void CardOpened() {
        leftOption.gameObject.SetActive(true);
        rightOption.gameObject.SetActive(true);
        leftOption.LoadData(cardData.leftOptionText, cardData.altLeftOptionText);
        rightOption.LoadData(cardData.rightOptionText, cardData.altRightOptionText);
    }

    void CloseCard() {
        Utils.instance.ZoomOut(rectTransform, ZOOM_CARD_TIME, () => EndCloseCard());
        Utils.instance.Move(rectTransform, rectTransform.localPosition, Vector3.zero, ZOOM_CARD_TIME);
    }

    void EndCloseCard() {
        gameObject.SetActive(false);
    }
}
