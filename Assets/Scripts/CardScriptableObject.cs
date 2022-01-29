using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/New Card", order = 51)]
public class CardScriptableObject : ScriptableObject {
    public CardScriptableObject leftOption;
    public CardScriptableObject rightOption;
    public string text;
    public string leftOptionText;
    public string rightOptionText;
    public string altLeftOptionText;
    public string altRightOptionText;
    public string dreamText;
    public int leftOptionValue;
    public int rightOptionValue;
    public bool transform;
}
