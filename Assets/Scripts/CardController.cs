using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    //[SerializeField] ClipboardController clipboard;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image cardPicture;
    [SerializeField] OptionController leftOption;
    [SerializeField] OptionController rightOption;

    public float THRESHOLD = 400;
    public float MAX_MOVEMENT = 600;
    public float ZOOM_CARD_TIME = 0.25f;
    public float TIME_BEFORE_SLEEP = 1f;
    public float BLOCK_DRAG_TIME = 2f;

    public float TIME_BEFORE_TRANSFORM = 1.5f;
    public float BLINK_CLOSE_EYES_TIME = 0.5f;
    public float BLINK_OPEN_EYES_TIME = 0.5f;

    public bool block;
    float offset;

    CardScriptableObject cardData;

    public void LoadCard(CardScriptableObject card) {
        block = true;
        cardData = card;
        cardPicture.sprite = cardData.picture;
        leftOption.gameObject.SetActive(false);
        rightOption.gameObject.SetActive(false);
        leftOption.SetHighlight(false);
        rightOption.SetHighlight(false);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (block) eventData.pointerDrag = null;
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
        if (rectTransform.localPosition.x >= THRESHOLD) SelectRight();
        else if (rectTransform.localPosition.x <= -THRESHOLD) SelectLeft();
        else ReturnStartPosition();
    }

    void SelectRight() {
        CloseCard();
        leftOption.Close();
        GameManager.instance.score += cardData.rightOptionValue;
        if (cardData.master != 0) GameManager.instance.masterSlave.Add(cardData.master, CardOption.right);
        SelectCard(cardData.rightOption);
    }

    void SelectLeft() {
        CloseCard();
        rightOption.Close();
        GameManager.instance.score += cardData.leftOptionValue;
        if (cardData.master != 0) GameManager.instance.masterSlave.Add(cardData.master, CardOption.left);
        SelectCard(cardData.leftOption);
    }

    void SelectCard(CardScriptableObject card) {
        AudioManager.ClickSound();
        CardScriptableObject selectedCard = card;
        if (cardData.slave != 0 && GameManager.instance.masterSlave.ContainsKey(cardData.slave)) {
            if (GameManager.instance.masterSlave[cardData.slave] == CardOption.left) selectedCard = cardData.leftOption;
            else if (GameManager.instance.masterSlave[cardData.slave] == CardOption.right) selectedCard = cardData.rightOption;
        }
        Utils.instance.Timer(TIME_BEFORE_SLEEP, () => GameManager.instance.EndDay(selectedCard));
    }

    void ReturnStartPosition() {
        rectTransform.localPosition = Vector3.zero;
    }

    public void OpenCard() {
        GameManager.instance.blackPanel.SetActive(true);
        rectTransform.localPosition = Vector3.zero;
        Utils.instance.ZoomIn(rectTransform, ZOOM_CARD_TIME, () => CardOpened());
    }

    void CardOpened() {
        leftOption.gameObject.SetActive(true);
        rightOption.gameObject.SetActive(true);
        leftOption.LoadData(cardData.leftOptionText, cardData.altLeftOptionText);
        rightOption.LoadData(cardData.rightOptionText, cardData.altRightOptionText);
        Utils.instance.Timer(BLOCK_DRAG_TIME, () => { block = false; });
        if (cardData.altPicture != null && GameManager.instance.dayViolentCount == 1) Utils.instance.Timer(TIME_BEFORE_TRANSFORM, () => StartTransform());
    }

    void StartTransform() {
        GameManager.instance.eyeLids.CloseEyes(BLINK_CLOSE_EYES_TIME, () => Transform());
    }

    void Transform() {
        GameManager.instance.redPanel.SetActive(true);
        cardPicture.sprite = cardData.altPicture;
        GameManager.instance.eyeLids.OpenEyes(BLINK_OPEN_EYES_TIME);
    }

    void CloseCard() {
        Utils.instance.ZoomOut(rectTransform, ZOOM_CARD_TIME, () => EndCloseCard());
        Utils.instance.Move(rectTransform, rectTransform.localPosition, Vector3.zero, ZOOM_CARD_TIME);
    }

    void EndCloseCard() {
        gameObject.SetActive(false);
    }
}
