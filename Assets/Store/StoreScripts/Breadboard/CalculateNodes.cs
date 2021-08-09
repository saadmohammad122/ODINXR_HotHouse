using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateNodes
{
    public CalculateNodes()
    {

    }
    public void MiddleLocation(Vector3 position, float lowX, float lowZ, float avgCellSize, bool rotated)
    {
        float x_location = Mathf.Abs(position.x - lowX);
        float z_location = Mathf.Abs(position.z - lowZ);

        // IMPORTANT TO NOTE: Position is for the center of the resistor and favors the lower side of the BB
        int x_node = Mathf.RoundToInt(x_location / avgCellSize);
        Debug.Log("x_node: " + x_node);

        int z_node = Mathf.RoundToInt(z_location / avgCellSize);
        Debug.Log("z_node: " + z_node);
    }
}
