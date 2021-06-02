using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{

    public float lowX;
    public float highX;
    public float lowZ;
    public float highZ;
    public float midHighZ;
    public float midLowZ;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = true;
            component.lowX = lowX;
            component.highX = highX;
            component.lowZ = lowZ;
            component.highZ = highZ;
            component.midHighZ = midHighZ;
            component.midLowZ = midLowZ;
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
            component.midHighZ = 0;
            component.midLowZ = 0;
        }
    }
}
