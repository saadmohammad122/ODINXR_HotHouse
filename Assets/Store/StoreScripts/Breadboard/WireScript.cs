using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{

    private int ExitCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("Got here!");
        
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                //print(this.name + " Got to OnCollisionEnter!");
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .8798f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                ContactPoint contact = collision.contacts[0];
                string UniqueName = this.GetComponentInParent<Properties>().UniqueName;
                //print(UniqueName + ":    " + contact.thisCollider.name + " hit " + nodeLocation);

               var ComponentScript = this.GetComponentInParent<ComponentScript>();
               var CircuitCreator = this.GetComponentInParent<CircuitCreator>();

                if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
                {
                    //print("Adding itself to circuitCreator!");
                    ComponentScript.AddedToDictionary = true;
                    CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                }
                else
                {
                    //print("Now we are adding ourselves to the list of components ");
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
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
                //print(this.GetComponentInParent<Properties>().UniqueName + " exiting " /* + nodeLocation*/);
                ExitCount++;
                var ComponentScript = this.GetComponentInParent<ComponentScript>();
                ComponentScript.ClearData = true; //change clear data to only clear this component***
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ComponentScript.AddedToDictionary = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
