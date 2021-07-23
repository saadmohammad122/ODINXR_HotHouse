using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    public bool inGrid;
    public bool isHeld;
    public bool hovering;
    public Vector3 offset;
    private float size = 0.03f;

    // For calculating the bounds of the placement of the object
    // Set in SnapLocation.cs
    public float lowX;
    public float highX;
    public float lowZ;
    public float highZ;
    public float lowY;

    public Transform hover;
    Transform newHover;

    // Update is called once per frame
    void Update()
    {
        if (inGrid && (isHeld || hovering))
            snap();

        if (!inGrid && newHover != null)
        {
            Destroy(newHover.gameObject);
            hovering = false;
        }
    }

<<<<<<< HEAD
        // For calculating the bounds of the placement of the object
        // Set in SnapLocation.cs
        public float lowX;
        public float highX;
        public float lowZ;
        public float highZ;
        public float lowY;
=======
    private void snap()
    {
        // "vec" gives us the desired rotation.
        //  Can go either vertical or horizontal
        var placementAngle = transform.eulerAngles;
        placementAngle.x = 0f;
        placementAngle.y = Mathf.Round(transform.eulerAngles.y / 90) * 90;

        // Slightly different offsets needed for the rotations, so set
        //  appropriate offset if piece is rotated
        if (placementAngle.y % 180 == 0)
        {
            //offset.x = 0; Commented out for horizontal rotation offset must not be 0
        }
>>>>>>> d9d8c2338fba4f94a40222d62ede4de024e050ce

        placementAngle.z = 90f;

<<<<<<< HEAD
        // Update is called once per frame
        void Update()
        {
            if (inGrid && (isHeld || hovering))
                snap();

            if (!inGrid && newHover != null)
=======
        // My desire for position is to contain an offset within
        //  each snaplocation.  When a resistor enters the trigger,
        //  it then assigns an offset.  This is good bc the offsets
        //  can vary between parts so we can make them more specific.
        Vector3 position = BreadboardPosition();

        

        if (!isHeld)
        {
            if (hovering)
>>>>>>> d9d8c2338fba4f94a40222d62ede4de024e050ce
            {
                Destroy(newHover.gameObject);
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
        else
        {
            Destroy(newHover.gameObject);
            newHover = Instantiate(hover, position, Quaternion.Euler(placementAngle));
        }
    }

<<<<<<< HEAD
            // My desire for position is to contain an offset within
            //  each snaplocation.  When a resistor enters the trigger,
            //  it then assigns an offset.  This is good bc the offsets
            //  can vary between parts so we can make them more specific.
<<<<<<< HEAD
            Vector3 position = BreadboardPosition();
            
=======
            Vector3 position =
                new Vector3(
                    Mathf.RoundToInt(transform.position.x / size) * size + offset.x,
                    0.84f,
=======
    private Vector3 BreadboardPosition()
    {
        Vector3 position =
                new Vector3(
                    Mathf.RoundToInt(transform.position.x / size) * size + offset.x,
                    lowY + offset.y,
>>>>>>> d9d8c2338fba4f94a40222d62ede4de024e050ce
                    Mathf.RoundToInt(transform.position.z / size) * size + offset.z);

        // The bounds for the grid are set here.  The bounds need to be inputted
        //  for each grid.  They go into the snapLocation section of the grid.

        // End x bounds
        if (position.x > highX)
            position.x = highX;
        else if (position.x < lowX)
            position.x = lowX;

<<<<<<< HEAD
            // End z bounds
            if (position.z > highZ)
                position.z = highZ;
            else if (position.z < lowZ)
                position.z = lowZ;


>>>>>>> parent of 39afb30 (Finalized)
            if (!isHeld)
            {
                if (hovering)
                {
                    Destroy(newHover.gameObject);
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
            else
            {
                Destroy(newHover.gameObject);
                newHover = Instantiate(hover, position, Quaternion.Euler(placementAngle));
            }
        }

        private Vector3 BreadboardPosition()
        {
        Vector3 position =
                new Vector3(
                    Mathf.RoundToInt(transform.position.x / size) * size + offset.x,
                    lowY + offset.y,
                    Mathf.RoundToInt(transform.position.z / size) * size + offset.z);

        // The bounds for the grid are set here.  The bounds need to be inputted
        //  for each grid.  They go into the snapLocation section of the grid.

        // End x bounds
        if (position.x > highX)
            position.x = highX;
        else if (position.x < lowX)
            position.x = lowX;
=======
        // End z bounds
        if (position.z > highZ)
            position.z = highZ;
        else if (position.z < lowZ)
            position.z = lowZ;

        return position;
    }
}
>>>>>>> d9d8c2338fba4f94a40222d62ede4de024e050ce

        // End z bounds
        if (position.z > highZ)
            position.z = highZ;
        else if (position.z < lowZ)
            position.z = lowZ;

        return position;
        }
    }
