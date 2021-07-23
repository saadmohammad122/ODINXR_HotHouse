using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenu : MonoBehaviour
    {
    public GameObject MainMenu;
    public GameObject Movement;
    public Camera player;

    public bool isPaused = false;

    void Start()
    {
        /* "SetActive()" enables or disables the component in the hierarchy
         *  This makes the whole component void in the gamespace
         *  To see the equivalent of this, go into the hierarchy, select
         *   something, and click the checkbox right next to it's name on
         *   the inspector panel */
        MainMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        // The "P" key is used to pull up or put away the menu
        // I originally wanted escape, but there were conflicts
        // between that and how Unity normally operates
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused == false)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        MainMenu.SetActive(true);
        isPaused = true;
        GameObject.Find("Student").GetComponent<FirstPersonAIO>().playerCanMove = false;

        setMenu();
    }

    public void Unpause()
    {
        MainMenu.SetActive(false);
        isPaused = false;
        GameObject.Find("Student").GetComponent<FirstPersonAIO>().playerCanMove = true;

    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    private void setMenu()
    {
        Vector3 position = player.transform.position + player.transform.forward;
        position.y = 1.45f;
        transform.position = position;

        Quaternion rotation = player.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
    }
}
