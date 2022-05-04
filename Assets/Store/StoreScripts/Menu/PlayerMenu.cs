using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public Camera player;
    private Transform ComponentContainer;
    private WristMenu WristMenuScript;

    public GameObject LeftHand;
    private float rotationMin = 25;
    private float rotationMax = 170;

    public GameObject Resistor;
    private GameObject Component;

    public bool isPaused;
    public bool buttonPressed;
    private bool hasComponentInStore;

    void Start()
    {
        MainMenu.SetActive(false);
        isPaused = false;
        buttonPressed = false;
        hasComponentInStore = false;

        ComponentContainer = transform.Find("WristMenu").Find("ComponentContainer");
        if (ComponentContainer == null)
            Debug.Log("Could not find the ComponentContainer");

        WristMenuScript = transform.Find("WristMenu").GetComponent<WristMenu>();
        if (WristMenuScript == null)
            Debug.Log("Could not find WristMenu or the script within WristMenu");
    }

    void Update()
    {

        if (buttonPressed && isPaused == false)
        {
            Pause();
        }
        else if (!buttonPressed && isPaused == true)
        {
            Unpause();
        }

        if (isPaused == true)
            SetMenuPosition();

    }

    private void SetMenuPosition()
    {
        transform.position = LeftHand.transform.position + LeftHand.transform.forward;
        // took out rotation as that was messing with snap turn provider
        //transform.rotation = LeftHand.transform.localRotation;
    }

    // Checks the rotation of the left hand, returns true if it is flipped over
    /*    private bool CheckPaused()
        {
            float handRotation = LeftHand.transform.localEulerAngles.z;
            if (handRotation > rotationMin && handRotation < rotationMax)
                return true;

            return false;
        }*/

    public void Pause()
    {
        MainMenu.SetActive(true);
        isPaused = true;

        if (hasComponentInStore == false)
        {
            SpawnComponent();
        }
    }

    public void Unpause()
    {
        MainMenu.SetActive(false);
        isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public void SpawnComponent()
    {
        if (WristMenuScript == null)
            WristMenuScript = transform.Find("WristMenu").GetComponent<WristMenu>();

        Component = WristMenuScript.DisplayComponent(ComponentContainer);
        hasComponentInStore = true;
    }

    public void SetComponentInStore()
    {
        hasComponentInStore = true;
    }

    public void TakeComponentFromStore()
    {
        hasComponentInStore = false;
    }
}
