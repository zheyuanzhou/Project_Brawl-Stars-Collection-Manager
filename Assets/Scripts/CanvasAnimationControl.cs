using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationControl : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //MARKER When You PRESS the selectButton on the FIRST SCREEN 
    public void SelectButton()
    {
        anim.SetTrigger("MoveSecond");
    }

    //MARKER WHen Player Select One of Character on the DISPLAY(SECOND) CANVAS, MVOE to the DETAIL(THIRD) SCREEN
    //MARKER HAS BEEN CALLED Inside SelectCharacter Function from CharacterSelection script
    public void SelectHero()
    {
        anim.SetTrigger("MoveThird");
    }

    //BACK to the DISPLAY(SECOND) SCREEN, ATTCHED TO BACKBUTTON ON THIRD CANVAS
    public void BackButtonToSelection()
    {
        anim.SetTrigger("BackSecond");
    }

    //BACK to the Menu(FIRST) SCREEN, ATTACH TO BACKBUTTON ON SECOND CANVAS
    public void BackButtonToMain()
    {
        anim.SetTrigger("BackFirst");
    }


}
