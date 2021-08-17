using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapLocation : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.SetInGrid();
            component.SetBoundaries(this.GetComponent<Collider>().bounds.min.x,
                                    this.GetComponent<Collider>().bounds.max.x,
                                    this.GetComponent<Collider>().bounds.min.z,
                                    this.GetComponent<Collider>().bounds.max.z,
                                    this.GetComponent<Collider>().bounds.min.y);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Resistor")
        {
            ObjectController component = other.gameObject.GetComponent<ObjectController>();
            component.ClearInGrid();
            component.ClearBoundaries();
        }
    }
}
