using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ObjectController component;
    public Camera FPCam;
    public Transform hand;

    private float objectSize;
    int rotationAngle;
    public RaycastHit hit;

    public PlayerMenu menu;

    private readonly int maxDistance = 100;


    // Start is called before the first frame update
    void Start()
    {
        component = null;
    }

    // Update is called once per frame
    void Update()
    {
        GrabObject();

        if (Input.GetMouseButtonDown(1) && component != null)
        {
            component = ReleaseObject(component);
        }

        // Assigning the "Q" button on a keyboard to rotate components
        if (Input.GetKeyDown(KeyCode.Q) && component != null)
        {
            Rotate();
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
        Ray mousePointer = FPCam.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        if (component == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(mousePointer, out hit, maxDistance, layerMask))
                {
                    // Check to see if hit component
                    if (hit.transform.gameObject.GetComponent<ObjectController>() != null)
                    {
                        component = hit.transform.gameObject.GetComponent<ObjectController>();
                        HitComponent(component);
                    }

                    // Check to see if clicked button
                    else
                    {
                        MenuController mc = gameObject.GetComponent<MenuController>();
                        mc.checkButton(hit);
                    }
                }
            }
        }
        else
        {
            component.transform.position = hand.transform.position + FPCam.transform.forward * objectSize;
            var placementAngle = transform.eulerAngles;
            placementAngle.x = 0f;
            placementAngle.y = hand.transform.eulerAngles.y - rotationAngle;
            placementAngle.z = 90f;
            component.transform.localEulerAngles = placementAngle;
        }
    }

    private void HitComponent(ObjectController component)
    {
        objectSize = component.GetComponent<Renderer>().bounds.size.magnitude;
        component.isHeld = true;
        DeactivateObject(component);

        // Turn these off so that the component does not move once in the players hand
        component.GetComponent<Rigidbody>().useGravity = false;
        component.GetComponent<Rigidbody>().isKinematic = true;
    }

    private ObjectController ReleaseObject(ObjectController component)
    {
        component.GetComponent<Rigidbody>().useGravity = true;
        component.GetComponent<Rigidbody>().isKinematic = false;
        component.isHeld = false;
        ActivateOdject(component);
        //component.transform.parent = null;

        return null;
    }

    public void Rotate()
    {
        rotationAngle = (rotationAngle + 90) % 180;
        var angle = component.transform.eulerAngles;
        angle.y = rotationAngle;
        component.transform.eulerAngles = angle;
    }

    public void DeactivateObject(ObjectController component)
    {
        component.GetComponent<CollisionDetecion>().enabled = false;
    }

    public void ActivateOdject(ObjectController component)
    {
        component.GetComponent<CollisionDetecion>().enabled = true;
    }
}