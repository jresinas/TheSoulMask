using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClipboardController : MonoBehaviour, IPointerClickHandler {
    //[SerializeField] CardController card;

    public void OnPointerClick(PointerEventData eventData) {
        OpenCard();
    }

    void OpenCard() {
        GameManager.instance.card.gameObject.SetActive(true);
        GameManager.instance.card.OpenCard();
    }
}
