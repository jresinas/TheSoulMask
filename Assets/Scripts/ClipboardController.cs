using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClipboardController : MonoBehaviour, IPointerClickHandler {
    [SerializeField] SpriteRenderer seal;

    public void LoadData(CardScriptableObject card) {
        if (card.seal != null) seal.sprite = card.seal;
        else seal.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData) {
        OpenCard();
    }

    void OpenCard() {
        GameManager.instance.card.gameObject.SetActive(true);
        GameManager.instance.card.OpenCard();
    }
}
