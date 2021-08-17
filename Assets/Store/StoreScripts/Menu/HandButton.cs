using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class HandButton : XRBaseInteractable
{
    // When the button on the back on the wrist is pressed, invert the boolean in the menu 
    //  script, which allows the menu to apprear
    public void ButtonPress(GameObject Menu)
    {
        Menu.GetComponent<PlayerMenu>().buttonPressed = !Menu.GetComponent<PlayerMenu>().buttonPressed;
    }
}
