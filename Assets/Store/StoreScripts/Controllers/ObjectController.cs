using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    private bool inGrid, isHeld, hovering, rotated;
    [SerializeField] private Vector3 HorizontalOffset;
    [SerializeField] private Vector3 VerticalOffset;
    [SerializeField] private Vector3 offset;

    // For calculating the bounds of the placement of the object
    // Set in SnapLocation.cs
    private float lowX, highX;
    private float lowZ, highZ;
    public float lowY;
    private float avgCellSize;
    [SerializeField] private float xBorder;

    public Transform hover;
    Transform newHover;

    private CalculateNodes Calculator;

    private void Start()
    {
        Calculator = new CalculateNodes();
        rotated = false;

        // Components are picked up horizontally, so initialize the offset to horizontal
        offset = HorizontalOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGrid && (isHeld || hovering))
            Snap();

        if (!inGrid && newHover != null)
        {
            Destroy(newHover.gameObject);
            hovering = false;
        }
    }

    private void Snap()
    {
        // "vec" gives us the desired rotation.
        //  Can go either vertical or horizontal
        var placementAngle = transform.eulerAngles;
        placementAngle.x = 0f;
        placementAngle.y = Mathf.Round(transform.eulerAngles.y / 90) * 90;

        // Get correct offset based on the current rotation of the resistor when it is in the grid zone
        if (placementAngle.y % 180 == 0)
            offset = VerticalOffset;
        else
            offset = HorizontalOffset;


        placementAngle.z = 90f;

        // Obtain a grid position within the boundaries and add an offset to that position so
        //  the resistor directly aligns with the nodes
        Vector3 position = BreadboardPosition();

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

            Debug.Log($"Low X: {lowX}; High X: {highX}");
            Debug.Log("Avg Cell Size: " + avgCellSize);
            Calculator.MiddleLocation(transform.position, lowX, lowZ, avgCellSize, rotated);
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
                    (Mathf.RoundToInt((transform.position.x - offset.x) / .02977f) * .02977f) + offset.x,
                    lowY + offset.y,
                    (Mathf.RoundToInt((transform.position.z - offset.z) / .02977f) * .02977f) + offset.z);

        // End x bounds
        if (position.x > highX)
            position.x = highX - (avgCellSize / 2);
        else if (position.x < lowX)
            position.x = lowX + (avgCellSize / 2);

        // End z bounds
        if (position.z > highZ)
            position.z = highZ - (avgCellSize / 2);
        else if (position.z < lowZ)
            position.z = lowZ + (avgCellSize / 2);

        return position;
    }

    // I watched a video and they explained that in clean code, variables are set to private and not modified in other scripts but instead they would
    //  call public functions to modify these variables, so I wanted to apply clean code practices.
    public void SetInGrid()
    {
        inGrid = true;
    }

    public void ClearInGrid()
    {
        inGrid = false;
    }

    public void SetIsHeld()
    {
        isHeld = true;
    }

    public void ClearIsHeld()
    {
        isHeld = false;
    }

    public void SetBoundaries(float lowX, float highX, float lowZ, float highZ)
    {
        this.lowX = lowX;
        this.highX = highX;
        this.lowZ = lowZ;
        this.highZ = highZ;

        avgCellSize = (highX - lowX) / 60;
    }

    public void ClearBoundaries()
    {
        lowX = 0;
        highX = 0;
        lowZ = 0;
        highZ = 0;

        avgCellSize = 0;
    }

    public void Rotate()
    {
        // Switch the boolean orientation
        // Issue with the rotation orientation when approaching the board from a different side
        rotated = !rotated;

        var angle = transform.eulerAngles;
        if (rotated)
            angle.y = 0f;
        else
            angle.y = 90f;
        transform.eulerAngles = angle;

    }

    public bool GetRotation()
    {
        return rotated;
    }
}

