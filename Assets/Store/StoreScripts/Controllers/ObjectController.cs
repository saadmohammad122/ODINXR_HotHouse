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

        public Transform hover;
        Transform newHover;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (inGrid)
                snap();

            if (!inGrid && newHover != null)
            {
                Destroy(newHover.gameObject);
                hovering = false;
            }
        }

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
                offset.x = 0;
            }

            placementAngle.z = 90f;

            // My desire for position is to contain an offset within
            //  each snaplocation.  When a resistor enters the trigger,
            //  it then assigns an offset.  This is good bc the offsets
            //  can vary between parts so we can make them more specific.
            Vector3 position =
                new Vector3(
                    Mathf.RoundToInt(transform.position.x / size) * size + offset.x,
                    0.84f + offset.y,
                    Mathf.RoundToInt(transform.position.z / size) * size + offset.z);

            // The bounds for the grid are set here.  The bounds need to be inputted
            //  for each grid.  They go into the snapLocation section of the grid.

            // End x bounds
            if (position.x > highX)
                position.x = highX;
            else if (position.x < lowX)
                position.x = lowX;

            // End z bounds
            if (position.z > highZ)
                position.z = highZ;
            else if (position.z < lowZ)
                position.z = lowZ;


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

    }

