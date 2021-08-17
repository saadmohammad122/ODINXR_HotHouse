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
        // IMPORTANT TO NOTE: Position is for the center of the resistor and favors the lower side of the BB
        Debug.Log("Avg Cell Size: " + avgCellSize);
        Debug.Log("Starting Z Position: " + position.z);
        Debug.Log("Low Z Position: " + lowZ);

        float ZPosition = position.z;
        int ZCount = 0;
        do
        {
            ZCount++;
        } while (lowZ <= (ZPosition -= avgCellSize));

        Debug.Log("Z Count: " + ZCount);

        float XPosition = position.x;
        int XCount = 0;

        while (lowX <= (XPosition -= avgCellSize))
            XCount++;

        Debug.Log("X Count: " + XCount);
    }
}
