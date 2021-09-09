using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{

    private int ExitCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //print(this.name + " got to OnCollisionEnter!");
        if (this.GetComponent<NodeScript>().enabled == true)
        {
            //print(this.name + " almost");
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                //print(this.name + " adding");
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .88f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                ContactPoint contact = collision.contacts[0];
                string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;
                //print(UniqueName + ":    "  + contact.thisCollider.name + " hit " + nodeLocation);
                
                var ComponentScript = this.GetComponent<ComponentScript>();
                var CircuitCreator = this.GetComponentInParent<CircuitCreator>();
               
                if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
                {
                    // Create an entry. We have to do this because we need to create a new dictionary within our dictionary
                    ComponentScript.AddedToDictionary = true;
                    CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());

                    // Set in/out. we don't know what it is and we don't need to
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                }

                // If the component is already in the dictionary
                else
                {   // Just add in/out
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                }
                ExitCount = 0;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ExitCount < 1)
        {

            if (other.gameObject.CompareTag("BreadBoardHoles"))
            {
                string nodeLocation = other.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                //print(this.gameObject.GetComponent<Properties>().UniqueName + " exiting " /* + nodeLocation*/);
                ExitCount++;
                var CircuitScript = this.GetComponent<ComponentScript>();
                CircuitScript.ClearData = true; //change clear data to only clear this component***
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                CircuitScript.AddedToDictionary = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
