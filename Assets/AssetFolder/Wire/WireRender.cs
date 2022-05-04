using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRender : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public LineRenderer linerenderer;
    public float vertexCount = 12;
    public float Point2Yposition = 2;
    public Transform PinConnector1;
    public Transform PinConnector2;



    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if ((PinConnector1.transform.position != Point1.transform.position) || (PinConnector2.transform.position != Point3.transform.position))
        {
            //PinConnector1.transform.position = new Vector3(Point1.transform.position.x, Point1.transform.position.y - .1f, Point1.transform.position.z);
            //PinConnector2.transform.position = new Vector3(Point3.transform.position.x, Point3.transform.position.y - .1f, Point3.transform.position.z);
            Point1.transform.position = new Vector3(PinConnector1.transform.position.x, PinConnector1.transform.position.y + .031f, PinConnector1.transform.position.z);
            Point3.transform.position = new Vector3(PinConnector2.transform.position.x, PinConnector2.transform.position.y + .031f, PinConnector2.transform.position.z);

        }

        Point2.transform.position = new Vector3((Point1.transform.position.x + Point3.transform.position.x) / 2, Point2Yposition, (Point1.transform.position.z + Point3.transform.position.z) / 2);
        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(Point1.position, Point2.position, ratio);
            var tangent2 = Vector3.Lerp(Point2.position, Point3.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        linerenderer.positionCount = pointList.Count;
        linerenderer.SetPositions(pointList.ToArray());
    }
}
