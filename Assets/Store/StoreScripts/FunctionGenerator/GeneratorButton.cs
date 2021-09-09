using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GeneratorButton : XRBaseInteractable
{
    public void ButtonPress(GameObject UI)
    {
        UI.GetComponent<FunctionGenerator>().ButtonPress();
    }
}
