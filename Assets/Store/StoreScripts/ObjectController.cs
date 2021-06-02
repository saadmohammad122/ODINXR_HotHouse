using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    public bool inGrid;
    public bool isHeld;
    public bool hovering;
    public Vector3 offset = new Vector3(0, 0, 0);
    private float size = 0.3f;

    public Transform hover;
    private Transform newHover;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inGrid)
            snap();
    }

    private void snap()
    {
        // "vec" gives us the desired rotation.
        //  Can go either vertical or horizontal
        var placementAngle = transform.eulerAngles;
        placementAngle.x = 0f;
        placementAngle.y = Mathf.Round(transform.eulerAngles.y / 90) * 90;
        placementAngle.z = 90f;

        // My desire for position is to contain an offset within
        //  each snaplocation.  When a resistor enters the trigger,
        //  it then assigns an offset.  This is good bc the offsets
        //  can vary between parts so we can make them more specific.
        Vector3 position =
            new Vector3(
                Mathf.RoundToInt(transform.position.x / size) * size,
                0.85f,
                Mathf.RoundToInt(transform.position.z / size) * size);

        if (!isHeld)
        {
            if (hovering)
            {
                Destroy(newHover);
                hovering = false;
            }
            transform.eulerAngles = placementAngle;
            transform.position = position;

            // We disabled these in the playercontroller once we
            //  picked up the object so we shall enable them now
            //  that the player is no longer holding them
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        else if (!hovering)
        {
            newHover = Instantiate(hover, position, Quaternion.Euler(placementAngle));
            hovering = true;
        }
    }

}
