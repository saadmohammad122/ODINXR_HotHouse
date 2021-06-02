using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ObjectController component;
    public Camera FPCam;
    public Transform hand;

    private float objectSize;

    // Start is called before the first frame update
    void Start()
    {
        component = null;
    }

    // Update is called once per frame
    void Update()
    {
        GrabObject();

        if (Input.GetMouseButtonDown(1))
        {
            component = ReleaseObject(component);
        }
    }

    private void GrabObject()
    {
        /* ISSUE: The player is unable to grab resistors within the grid
         *        because the raycast stops as the box collider.
         *        
         * SOLUTION: The use of layers. We have created a custom layer for
         *           the circuit grid under layer 6.  If we set the layer
         *           mask to represent layer 6 and then invert it to work
         *           for everything but layer 6, we can "look through it" */

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        RaycastHit hit;
        Ray mousePointer = FPCam.ScreenPointToRay(Input.mousePosition);
        int maxDistance = 100;

        if (component == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Old way of using the raycast
                //if (Physics.Raycast(mousePointer, out hit))

                // New way of using raycast
                // We are also setting the maxDistance the raycast will go, which is nice
                if (Physics.Raycast(mousePointer, out hit, maxDistance, layerMask))
                {
                    if (hit.transform.gameObject.GetComponent<ObjectController>() != null)
                    {
                        component = hit.transform.gameObject.GetComponent<ObjectController>();
                        HitComponent(component);
                    }
                }
            }
        }
        else
        {
            component.transform.position = hand.transform.position + FPCam.transform.forward * objectSize;
            var placementAngle = transform.eulerAngles;
            placementAngle.x = 0f;
            placementAngle.y = hand.transform.eulerAngles.y - 90f;
            placementAngle.z = 90f;
            component.transform.localEulerAngles = placementAngle;
        }
    }

    private void HitComponent(ObjectController component)
    {
        objectSize = component.GetComponent<Renderer>().bounds.size.magnitude;
        component.isHeld = true;

        // To ensure that the component rotates w/ the player, make the player
        //  its parent so player rotation will affect component rotation
        //component.transform.parent = FPCam.transform;

        // Turn these off so that the component does not move once in the players hand
        component.GetComponent<Rigidbody>().useGravity = false;
        component.GetComponent<Rigidbody>().isKinematic = true;
    }

    private ObjectController ReleaseObject(ObjectController component)
    {
        component.GetComponent<Rigidbody>().useGravity = true;
        component.GetComponent<Rigidbody>().isKinematic = false;
        component.isHeld = false;
        //component.transform.parent = null;

        return null;
    }
}
