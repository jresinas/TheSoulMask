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

    float offset;

    CardScriptableObject cardData;

    public void LoadCard(CardScriptableObject card) {
        cardData = card;
        cardText.text = card.text;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        offset = rectTransform.position.x - Input.mousePosition.x;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log(Input.mousePosition.x);
        //transform.position = new Vector2(Mathf.Clamp(Input.mousePosition.x, -MAX_MOVEMENT, MAX_MOVEMENT), transform.position.y);
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
        Debug.Log("right");
        CloseCard();
        GameManager.instance.EndDay(cardData.rightOption);
    }

    void SelectLeft() {
        Debug.Log("left");
        CloseCard();
        GameManager.instance.EndDay(cardData.leftOption);
    }

    void ReturnStartPosition() {
        Debug.Log("startPosition");
        rectTransform.localPosition = Vector3.zero;
    }

    public void OpenCard() {
        rectTransform.localPosition = Vector3.zero;
        Utils.instance.ZoomIn(rectTransform, ZOOM_CARD_TIME, () => CardOpened());
        //Vector3 startPos = Camera.main.ScreenToWorldPoint(clipboard.transform.position);
        //Debug.Log(startPos);
        //Utils.instance.Move(rectTransform, startPos, Vector3.zero, 0.5f);
    }

    void CardOpened() {
        leftOption.gameObject.SetActive(true);
        rightOption.gameObject.SetActive(true);
        leftOption.LoadData(cardData.leftOptionText, cardData.altLeftOptionText);
        rightOption.LoadData(cardData.rightOptionText, cardData.altRightOptionText);
    }

    void CloseCard() {
        leftOption.gameObject.SetActive(false);
        rightOption.gameObject.SetActive(false);
        Utils.instance.ZoomOut(rectTransform, ZOOM_CARD_TIME, () => EndCloseCard());
        Utils.instance.Move(rectTransform, rectTransform.localPosition, Vector3.zero, ZOOM_CARD_TIME);
    }

    void EndCloseCard() {
        gameObject.SetActive(false);
    }
}
