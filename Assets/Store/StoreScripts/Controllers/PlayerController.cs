using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private WristMenu wristMenu;
    private Transform componentContainer;

    public void GrabObject(GameObject component)
    {        
        SetWristMenu();

        ObjectController controller = component.GetComponent<ObjectController>();
        if (controller != null)
        {
            // If the component is in the store, then move it elsewhere so that it does not deactivate with the store
            // Refresht the component into the store
            if (controller.CheckInStore() == true)
            {
                controller.transform.SetParent(transform.parent);
                wristMenu.DisplayComponent(componentContainer);
            }
            controller.SetIsHeld();
        }
    }

    public void ReleaseObject(GameObject component)
    {
        ObjectController controller = component.GetComponent<ObjectController>();
        if (controller != null)
            controller.ClearIsHeld();
    }

    private void SetWristMenu()
    {
        GameObject menu = GameObject.FindGameObjectWithTag("Menu");

        wristMenu = menu.transform.Find("WristMenu").GetComponent<WristMenu>();
        if (wristMenu == null)
            Debug.Log("Could not find wrist menu");

        componentContainer = wristMenu.transform.Find("ComponentContainer");
    }
}