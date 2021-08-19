using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDNodeScript : MonoBehaviour
{
    public string nodeName;

    // Start is called before the first frame update
    void Start()
    {
        nodeName = this.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "BreadBoardHoles")
        {
            print("this is node" + nodeName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
