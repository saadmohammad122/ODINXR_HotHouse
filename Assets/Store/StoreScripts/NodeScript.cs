using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeScript : MonoBehaviour
{

    public Collider collider;
    public bool isColliding = false;
    public bool FreeFall = true;
    public float time = .03f;
    // Start is called before the first frame update
    void Start()
    {
        var componentScript = this.GetComponent<ComponentScript>();
        
    }



    private void OnCollisionEnter(Collision collision)
    {

       

    }


    /*
     * Snap in Place: 
     *      1. Picking the object up
     *      2. Attaching Object to breadboard
     *      
     * 
     * 
     * 
     * 
     */





    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HoleScript>() != null)
        {
            FreeFall = false;
            var componentScript = this.GetComponentInParent<ComponentScript>();
            Rigidbody physicalBody = componentScript.component.GetComponent<Rigidbody>();


            string nodeLocation = other.GetComponent<HoleScript>().UniqueName;
            //string name = collider.name;
            string name = this.name;

            // Idea 2: put rigidbody on end colldiers 


            if (!componentScript.NodeList.ContainsKey(name))
            {
                print("Got to freezing the body");

                //RigidbodyConstraints physicalBodyConstraints = componentScript.component.GetComponent<RigidbodyConstraints>();

                componentScript.NodeList.Add(name, nodeLocation);
                physicalBody.useGravity = false;

                physicalBody.velocity = Vector3.zero;
                physicalBody.angularVelocity = Vector3.zero;
                //physicalBodyConstraints = RigidbodyConstraints.FreezePositionY;
                physicalBody.constraints = RigidbodyConstraints.FreezePositionY;
                physicalBody.isKinematic = true;
                //componentScript.component.GetComponent<Rigidbody>().velocity = Vector3.zero;
                //  componentScript.component.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                //Vector3 stopInPlace = new Vector3(componentScript.component.transform.position.x, (float)0.86, componentScript.component.transform.position.z);
                //Vector3 stopInPlace = new Vector3(componentScript.component.transform.position.x, 0.86f, componentScript.component.transform.position.z);

                //componentScript.component.transform.localPosition = stopInPlace; 

            }
            //  Above code will have to be reviewed after seeing the snap function
            // Vector3 stopInPlace = new Vector3(componentScript.component.transform.position.x, (float) 0.86, componentScript.component.transform.position.z);
            //  componentScript.component.transform.position = stopInPlace; 

        }




    }

    private void OnTriggerExit(Collider other)
    {
        return;
        if (other.GetComponent<HoleScript>() != null)
        {
            print("Got to on trigger exit");
            var componentScript = this.GetComponentInParent<ComponentScript>();
            componentScript.ClearData = true;
            isColliding = false;
            string nodeLocation = other.GetComponent<HoleScript>().UniqueName;
            //string name = collider.name;
            string name = this.name;

            print("EXIT_name:  " + name); // check names of what is being hit
            print("EXIT_nodeLocation:  " + nodeLocation);
        }


    }
    // Idea 1: Catch OntriggerExit the first time, and do nothing.
    //          Second time it goes off, report an exit to the board.
    //      Cons: wont stop it from speeding through the board. 
    //      Pros: 

    //  Idea 2: OnCollision Enter reacts instantaently,
    //          OnTriggerExit, thin cube above collsionbox that is changed to a trigger when OnCollisionEnter is used.
    //  

    // Update is called once per frame
    void Update()
    {
        if (FreeFall == false)
        {
            var componentScript = this.GetComponentInParent<ComponentScript>();
            Rigidbody physicalBody = componentScript.component.GetComponent<Rigidbody>();
            physicalBody.useGravity = false;
            physicalBody.velocity = Vector3.zero;
            physicalBody.angularVelocity = Vector3.zero;
            //physicalBodyConstraints = RigidbodyConstraints.FreezePositionY;
            physicalBody.constraints = RigidbodyConstraints.FreezeAll;

        }
    }
}
