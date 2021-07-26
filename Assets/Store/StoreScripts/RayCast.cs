using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Components;




public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera fpsCam;
    public GameObject StoreButton;
    public GameObject StoreUI;

    void Start()
    {

    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSecondsRealtime(10);
        StoreUI.SetActive(false);
        print("got here");
        
    }

    // Used Fixed Update for Ray Casting
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("i"))
        {
            print("'i' key was pressed");
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = fpsCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {

                    if (hit.collider.name == StoreButton.name)
                    {
                        if (StoreUI != null)
                        {
                            StoreUI.SetActive(true);
                        }
                    }
                    new WaitForSecondsRealtime(10);
                 //WaitFor();

                }
            }
        }
    }
}
