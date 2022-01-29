using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/New Card", order = 51)]
public class CardScriptableObject : ScriptableObject {
    public CardScriptableObject leftOption;
    public CardScriptableObject rightOption;
    public string leftOptionText;
    public string rightOptionText;
    public string altLeftOptionText;
    public string altRightOptionText;
    public string dreamText;
    public int leftOptionValue;
    public int rightOptionValue;
    public Sprite picture;
    public Sprite altPicture;
    public int master;
    public int slave;
    public Sprite seal;
}
