using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSelection
{
    Shadow = 0, Fatter = 1, Mind = 2, Worm = 3, Harm = 4, Harm2 = 5, Knight = 6, Threesome = 7, Floger = 8,
    Main = 9, Crystal = 10, Wizard = 11, Fivesome = 12, Real = 13, Fighter = 14, Reader = 15, Last = 16, Boss = 17
}

//MARKER This script RUN FIRST EACH TIME
public class GameManager : MonoBehaviour
{
    public static GameManager instance;//Singleton Pattern
    private CardManager cardManager;

    public int money;
    public int gem;

    public PlayerSelection playerSelection;
    public int playerID;

    [Header("Character Sprites Resources")]
    public Sprite[] playerSprites;

    [Header("Player Property")]
    public int playerHealth;
    public int playerAttack; 
    public int playerExp;
    public Sprite playerSprite;

    private void Awake()
    {
        #region Singleton Pattern
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        #endregion

        cardManager = FindObjectOfType<CardManager>();

        money = 1000;
        gem = 200;
        FindObjectOfType<UIManager>().UpdateMoneyUI();//MARKER Have to UPDATE MONEY & GEM

        playerSelection = PlayerSelection.Shadow;//Default Character
        playerID = (int)playerSelection;
        FindObjectOfType<UIManager>().UpdatePlayerImage();//MARKER Have to UPDATE PLAYER IMAGE

        Debug.Log("Default Character is : " + playerSelection + ", ID is " + playerID);

        playerHealth = cardManager.cards[playerID].cardHealth;
        playerAttack = cardManager.cards[playerID].cardAttack;
        playerSprite = playerSprites[playerID];
        playerExp = cardManager.cards[playerID].currentExperience;
    }

    private void Start()
    {
        cardManager.cards[playerID].currentExperience = playerExp;
    }

}
