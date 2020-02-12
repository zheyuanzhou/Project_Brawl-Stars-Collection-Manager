using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite cardSprite;
    
    public bool locked;

    [TextArea(1, 10)]
    public string cardHistory;

    public int cardHealth;
    public int cardAttack;

    [TextArea(1,8)]
    public string cardDescription;

    //public int experience;

    public int currentExperience;//MARKER Current EXP INSTEAD OF character's TOTAL EXP

    public int upgradeCost;

    public int cardLevel;


}
