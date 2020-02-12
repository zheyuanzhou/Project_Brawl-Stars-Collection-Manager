using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public int currentIndex;//Player Selection index
     
    public Card[] cards;

    [Header("Cards UI")]
    public Text cardNameText;
    public Image cardSpriteImage;
    public Text cardHisText;
    public Text cardHealthText;
    public Text cardAttText;
    public Text cardDesText;
    public Text cardLvText; 
     
    [SerializeField] private int[] levelNeededeExp = new int[] { 10, 30, 60, 110, 180, 280, 480, 780, 1280, 2280};

    [Header("CharacterInfo Slots")]
    public GameObject[] slots;

    //MARKER SELECT BUTTON ON THE THIRD SCREEN, JUST used for showing active/inactive(Color) Hero
    public Image SelectButton;
    public Image histroyImage;//CHARACTER HISTORY WINDOW when Mouse POINT ENTER or EXIT

    public Text titleText;//"HERO SELECTION XX/18" ON THE TOP OF THE SECOND SCREEN
    public Text pageText;//"X/18" PAGE UI on the THIRD SCREEN (TOP RIGHT CORNER)

    private int healthIncrement = 100;//TODO Customize increment curver later
    private int attackIncrement = 250;//TODO Customize increment curver later

    [SerializeField] private bool CanUpgradeHero;
    public Text healthIncrementText;
    public Text attackIncrementText;

    [Header("VFX")]
    public ParticleSystem heroLevelupEffect;
    public ParticleSystem healthLevelUpEffect;
    public ParticleSystem attackLevelupEffect;

    public Animator tipAnim;

    private void Start()
    {
        currentIndex = 0;
        histroyImage.gameObject.SetActive(false);//DEFAULT histroy Image is turn off(SetActive to be false)

        ShowPageUI();
        DisplayInfo();
        DisplayLevelBar();

        DisplayCharacters();
    }

    private void Update()
    {
        //DisplayLevelBar();TODO REMOVE LATER
        CanUpgrade();//TODO REMOVE LATER
    }

    //MARKER CALL on BUTTON EVENT LISTENER
    public void NextButton()
    {
        currentIndex++;
        if(currentIndex >= cards.Length)
        {
            currentIndex = 0;
        }

        UpdateLevel();
        DisplayInfo();
        ShowPageUI();
    }

    //MARKER CALL on BUTTON EVENT LISTENER
    public void BackButton()
    {
        currentIndex--;
        if(currentIndex < 0)
        {
            currentIndex = cards.Length - 1;
        }

        UpdateLevel();
        DisplayInfo();//MARKER Each time When you PRESS NEXT or BACK button, DIAPLY CARD making sure current Card is WHAT YOU WANT
        ShowPageUI();
    }

    //PRESS UPGRADE button
    public void UpgradeButton()//When We press the Upgrade button
    {
        if(CanUpgrade())
        {
            if (GameManager.instance.money >= cards[currentIndex].upgradeCost)
                {
                    GameManager.instance.money -= cards[currentIndex].upgradeCost;
                    FindObjectOfType<UIManager>().UpdateMoneyUI();

                    DisplayInfo();

                    PlayParticles();
                    StartCoroutine(UpgradeHealthEffect());
                    StartCoroutine(UpgradeAttackEffect());
                    StartCoroutine(UpgradeIncrementDisplay());

                cards[currentIndex].cardLevel++;
                UpdateLevel();
                    
                    Debug.Log("Upgrade success");
                }
                else
                {
                    Debug.Log("Not enough money");
                }
        }
        else
        {
            tipAnim.SetTrigger("isActive");
            Debug.Log("You did not own this Character");
        }
    }

    private bool CanUpgrade()//MARKER Decide whether we can upgrade this hero or not
    {
        if (cards[currentIndex].locked == false)
        {
            CanUpgradeHero = true;
        }
        else
        {
            CanUpgradeHero = false;
        }

        return CanUpgradeHero;
    }

    #region Display All of characters and SELECTED Hero Detail INFO
    //MARKER Display Character DETAIL INFO on THIRD SCREEN
    public void DisplayInfo()
    {
        if (cards[currentIndex].locked == false)
        {

            SelectButton.color = new Color(1, 1, 1, 1);//NORAML COLOR
            cardSpriteImage.sprite = cards[currentIndex].cardSprite;
            cardSpriteImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            cardNameText.text = cards[currentIndex].cardName;
            cardHisText.text = cards[currentIndex].cardHistory;
            cardHealthText.text = cards[currentIndex].cardHealth.ToString();
            cardAttText.text = cards[currentIndex].cardAttack.ToString();
            cardDesText.text = cards[currentIndex].cardDescription;
        }
        else
        {

            SelectButton.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);//DARK COLOR
            cardSpriteImage.sprite = cards[currentIndex].cardSprite;
            cardSpriteImage.GetComponent<Image>().color = new Color(0.15f, 0.15f, 0.15f, 1f);

            cardNameText.text = "???";
            cardHisText.text = "???";
            cardHealthText.text = "???";
            cardAttText.text = "???";
            cardDesText.text = "???";
        }
    }

    private void DisplayLevelBar()
    {
        for (int j = 0; j < slots.Length; j++)
        {
            for (int i = 0; i < levelNeededeExp.Length; i++)
            {
                if (cards[j].locked == true)
                {
                    //[DISPLAY EXP Bar and LEVEL TEXT]
                    slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "XX/XXX";//MARKER Use One line
                    slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = "X";//LV on the CORNER Display

                    cards[j].cardLevel = 0;//MARKER NEW Features

                    slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = 0.0f;
                }
                else
                {
                    if ((cards[j].currentExperience >= levelNeededeExp[i]) && (cards[j].currentExperience <= levelNeededeExp[i + 1]))//MARKER The CURRENT_EXPERIENCE is NOT the TOTAL EXPERIENCE of the Character
                    {
                        if (j != GameManager.instance.playerID)//NEW
                        {
                            slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = cards[j].currentExperience + "/" + levelNeededeExp[i + 1];//MARKER CORRECT
                            slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = (i + 2).ToString();//LV

                            cards[j].cardLevel = i + 2;//MARKER NEW Features

                            slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = (float)cards[j].currentExperience / levelNeededeExp[i + 1];
                        }
                        else
                        {
                            slots[j].transform.Find("Experience Bar").transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = GameManager.instance.playerExp + "/" + levelNeededeExp[i + 1];//MARKER CORRECT
                            slots[j].transform.Find("Level Image").transform.GetChild(0).GetComponent<Text>().text = (i + 2).ToString();//LV

                            cards[j].cardLevel = i + 2;//MARKER NEW Features

                            slots[j].transform.Find("Experience Bar").transform.GetChild(0).transform.GetComponent<Image>().fillAmount = (float)GameManager.instance.playerExp / levelNeededeExp[i + 1];
                        }

                    }
                }
            }
        }
    }
    #endregion

    //MARKER Display All of Characters on SECOND SCREEN [LOCKED or UNLOCKED]
    private void DisplayCharacters()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = cards[i].cardSprite;

            if (!cards[i].locked)
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 1); ;
            }
        }
    }

    //MARKER Display TITLE & PAGE UI ON SELECTION(SECOND) SCREEN
    public void ShowPageUI()
    {
        titleText.text = "Hero Selection " + GetUnlockedCount() + "/" + cards.Length.ToString();
        pageText.text = (currentIndex + 1).ToString() + "/" + cards.Length.ToString();
    }

    public void UpdateLevel()//显示Hero Level。在NextButton，BackButton和第二Canvas各个英雄点击事件,UpgradeButton中
    {
        cardLvText.text = "Level " + cards[currentIndex].cardLevel.ToString();
    }

    //MARKER THIS HELPER METHOD ONLY used for CALCULATE How many cards UNCLOCKED in this game for ShowPageUI FUNCTION
    private int GetUnlockedCount()
    {
        int unlockedCount = 0;
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].locked == false)
            {
                unlockedCount++;
            }
        }
        return unlockedCount;
    }

    #region Only For Mouse Interaction with EXCLAMATION POINT(!!!!) Icon
    public void MouseEnterHisImage()
    {
        histroyImage.gameObject.SetActive(true);
    }

    public void MouseExitHisImage()
    {
        histroyImage.gameObject.SetActive(false);
    }
    #endregion

    #region VISUAL EFFECT (INCLUDES ROLLING Number Effect & PARTICLES system)
    //MARKER CALL this function when you PRESS UPGRADE BUTTON & CAN UPGRADE
    private void PlayParticles()
    {
        heroLevelupEffect.Play();
        healthLevelUpEffect.Play();
        attackLevelupEffect.Play();
    }

    //MARKER Rolling Number Effect. CALL THESE FUCNTION when you PRESS UPGRADE BUTTON & CAN UPGRADE 
    IEnumerator UpgradeHealthEffect()
    {
        int currentHealth = cards[currentIndex].cardHealth;

        while (cards[currentIndex].cardHealth < currentHealth + healthIncrement)
        {
            cards[currentIndex].cardHealth += 5;
            yield return new WaitForSeconds(1/20);
        }
    }

    IEnumerator UpgradeAttackEffect()//滚动升级数字
    {
        int currentAttack = cards[currentIndex].cardAttack;

        while(cards[currentIndex].cardAttack < currentAttack + attackIncrement)
        {
            cards[currentIndex].cardAttack += 5;
            yield return new WaitForSeconds(1/50);
        }
    }

    IEnumerator UpgradeIncrementDisplay() {
        healthIncrementText.text = "+" + healthIncrement.ToString();
        attackIncrementText.text = "+" + attackIncrement.ToString();
        yield return new WaitForSeconds(1.0f);
        healthIncrementText.text = "";
        attackIncrementText.text = "";
    }
    #endregion

}
