using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
    {

    // Start is called before the first frame update
    void Start()
        {
        var componentScript = this.GetComponent<ComponentScript>();
        }



        private void OnCollisionEnter(Collision collision)
        {
        
            if (this.name == "Colliders")
            {
      
                var componentScript = this.GetComponentInParent<ComponentScript>();
                string nodeLocation = this.GetComponent<HoleScript>().UniqueName;
                print("Enter_name:  " + name);
                print("Enter_nodeLocation:  " + nodeLocation);
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                componentScript.NodeList.Add(name, nodeLocation);

                if (!componentScript.NodeList.ContainsKey(name))
                {
                    print("Got to on collision enter.");
                    componentScript.NodeList.Add(name, nodeLocation);

                }
                //Check if doubles on this if statement otherwise delete
            }

 
        }
       

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<HoleScript>() != null)
            {
                FreeFall = false;
                var componentScript = this.GetComponentInParent<ComponentScript>();
                Rigidbody physicalBody = componentScript.component.GetComponent<Rigidbody>();


                string nodeLocation = other.GetComponent<HoleScript>().UniqueName;
                ///string name = this.name;


                if (!componentScript.NodeList.ContainsKey(name))
                {
                    ///print("Got to freezing the body");

                    //RigidbodyConstraints physicalBodyConstraints = componentScript.component.GetComponent<RigidbodyConstraints>();

                    componentScript.NodeList.Add(name, nodeLocation);
                    ///physicalBody.useGravity = false;

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

            }*/




       //OnCollisionExit

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<HoleScript>() != null)
            {
                print("Got to on trigger exit");
                var componentScript = this.GetComponentInParent<ComponentScript>();
                componentScript.ClearData = true;
                ///isColliding = false; This is an if fuction in component script
                string nodeLocation = other.GetComponent<HoleScript>().UniqueName;
                //string name = collider.name;
                string name = this.name;
                print("EXIT_name:  " + name);
                print("EXIT_nodeLocation:  " + nodeLocation);
                GetComponent<Rigidbody>().isKinematic = false;
                ///concider deleting component from here
            }

        }
            //Might have to be in Update()?

        

        // Update is called once per frame
        void Update()
        {
            

        /*if (FreeFall == false)
            {
                var componentScript = this.GetComponentInParent<ComponentScript>();
                Rigidbody physicalBody = componentScript.component.GetComponent<Rigidbody>();
                physicalBody.useGravity = false;
                physicalBody.velocity = Vector3.zero;
                physicalBody.angularVelocity = Vector3.zero;
                //physicalBodyConstraints = RigidbodyConstraints.FreezePositionY;
                physicalBody.constraints = RigidbodyConstraints.FreezeAll;

            }*/
        }
    }
