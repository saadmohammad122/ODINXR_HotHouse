using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeScript : MonoBehaviour
{

    public Collider collider;
    public bool isColliding = false;
    // Start is called before the first frame update
    void Start()
    {
        var componentScript = this.GetComponent<ComponentScript>();
        
    }



    private void OnCollisionEnter(Collision collision)
    {

       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding == false)
        {
            var componentScript = this.GetComponentInParent<ComponentScript>();

            string nodeLocation = other.gameObject.name;
            string name = collider.name;
            print("Got to OnTriggerEnter!");
            // figure out how to prevent double entries in List. Maybe switch to Dictionary
            if (!componentScript.NodeList.ContainsKey(name))
            {
                componentScript.NodeList.Add(name, nodeLocation);
                componentScript.component.GetComponent<Rigidbody>().useGravity = false;
                componentScript.component.GetComponent<Rigidbody>().isKinematic = true;
                componentScript.component.GetComponent<Rigidbody>().velocity = Vector3.zero;
                componentScript.component.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                //Vector3 stopInPlace = new Vector3(componentScript.component.transform.position.x, (float)0.86, componentScript.component.transform.position.z);
               // componentScript.component.transform.position = stopInPlace; 

        }
        //  Above code will have to be reviewed after seeing the snap function
        // Vector3 stopInPlace = new Vector3(componentScript.component.transform.position.x, (float) 0.86, componentScript.component.transform.position.z);
        //  componentScript.component.transform.position = stopInPlace; 

        isColliding = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        return;
        print("Got to on trigger exit");
        var componentScript = this.GetComponentInParent<ComponentScript>();
        componentScript.ClearData = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
