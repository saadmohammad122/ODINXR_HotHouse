using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag + " In Grid");
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.inGrid = true;
            component.lowX = this.GetComponent<Collider>().bounds.min.x;
            component.highX = this.GetComponent<Collider>().bounds.max.x;
            component.lowZ = this.GetComponent<Collider>().bounds.min.z;
            component.highZ = this.GetComponent<Collider>().bounds.max.z;
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
