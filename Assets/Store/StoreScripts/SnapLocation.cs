using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{
    // The offset seems to be about .03f in each direction,
    //  this should be checked for each scenario b/c we may be able
    //  to use a constant offset.
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = true;
            component.offset = offset;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = false;
            component.offset = new Vector3(0, 0, 0);
        }
    }
}
