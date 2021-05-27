using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentScript : MonoBehaviour
{
    public int numOfInputs = 0;
    public int FrameCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        var NodeList = new List<KeyValuePair <string, string>> ();
    }

    // Update is called once per frame
    void Update()
    {
        while (FrameCount > 20)
        {
            FrameCount = 0;
            //  Call Function to add to NodeList
            //  Consider running this function via ontriggerenter, which triggers the function to populate NodeList
            //  Benefits    :   Low processing use
            //  Drawbacks   :   More overhead on collisions, must implement a snap in place to have the trigger work.
            //                      On trigger, if the right trigger, then populate NodeList
        }
        FrameCount++;
    }
}
