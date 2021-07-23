using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Take the raycast and check if it hit the UI button, if it does
    //  then we can treat it appropriately and call the onClick()
    public void checkButton(RaycastHit hit)
    {
        if (hit.transform.gameObject.GetComponent<Button>() != null)
        {
            Button btn = hit.transform.gameObject.GetComponent<Button>();
            btn.onClick.Invoke();
        }
    }
}
