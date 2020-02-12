using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private CardManager cardManager;

    public Text moneyMenuText, gemMenuText, moneyInfoText, gemInfoText;

    public Image playerImage;

    private void Start()
    {
        cardManager = FindObjectOfType<CardManager>();
        UpdateMoneyUI();
        UpdatePlayerImage();
    }

    public void UpdateMoneyUI()
    {
        moneyMenuText.text = GameManager.instance.money.ToString();
        gemMenuText.text = GameManager.instance.gem.ToString();
        moneyInfoText.text = GameManager.instance.money.ToString();
        gemInfoText.text = GameManager.instance.gem.ToString();
    }

    public void UpdatePlayerImage()
    {
        playerImage.sprite = GameManager.instance.playerSprites[GameManager.instance.playerID];
    }

    #region REMOVE from CHARACTER SELECTION script
    //MARKER PRESS one of Characters on the DISPLAY(SECOND) SCREEN
    public void SelectCharacter(int _id)
    {
        cardManager.currentIndex = _id;
        FindObjectOfType<CanvasAnimationControl>().SelectHero();//MARKER MAKE ANIMATION
        cardManager.DisplayInfo();
        cardManager.UpdateLevel();
        cardManager.ShowPageUI();
    }

    //MARKER PRESS Hero SELECTION_BUTTON on the THIRD SCREEN
    public void PressSelectonHeroButton()
    {
        if (cardManager.cards[cardManager.currentIndex].locked == false)
        {
            GameManager.instance.playerID = cardManager.currentIndex;
            GameManager.instance.playerSelection = (PlayerSelection)cardManager.currentIndex;
            FindObjectOfType<UIManager>().playerImage.sprite = cardManager.cards[cardManager.currentIndex].cardSprite;
            Debug.Log("PLayer Default Class is : " + GameManager.instance.playerSelection + ", And ITS ID is " + GameManager.instance.playerID);

            LoadCharacterData();
        }
        else
        {
            Debug.Log("SORRY, YOU DONT OWN THIS CHARACTER");
        }
    }
    #endregion

    //PRESS the START button on the MENU screen, MOVE to the second Scene(PLAY THE GAME)
    public void StartButton()
    {
        FindObjectOfType<SceneFader>().FadeTo("01_Game");//MARKER SceneManager.LoadScene("01_Game");
    }

    //MARKER BEFORE you play the game, Load your SELECTED HERO DATA
    private void LoadCharacterData()
    {
        GameManager.instance.playerExp = cardManager.cards[GameManager.instance.playerID].currentExperience;
        GameManager.instance.playerHealth = cardManager.cards[GameManager.instance.playerID].cardHealth;
        GameManager.instance.playerAttack = cardManager.cards[GameManager.instance.playerID].cardAttack;
        GameManager.instance.playerSprite = GameManager.instance.playerSprites[GameManager.instance.playerID];//MARKER UPDATE player Profile on the first screen
    }

}
