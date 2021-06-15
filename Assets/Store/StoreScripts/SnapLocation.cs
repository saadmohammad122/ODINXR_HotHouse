using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{

    public float lowX;
    public float highX;
    public float lowZ;
    public float highZ;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Stay");
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = true;
            component.lowX = lowX;
            component.highX = highX;
            component.lowZ = lowZ;
            component.highZ = highZ;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = false;
            component.lowX = 0;
            component.highX = 0;
            component.lowZ = 0;
            component.highZ = 0;
        }
    }
}
