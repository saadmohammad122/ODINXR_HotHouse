using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{

    private int exitCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("WireScript");
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                print(this.name + " Got to OnCollisionEnter!");
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1.15f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                ContactPoint contact = collision.contacts[0];
                string UniqueName = this.GetComponentInParent<Properties>().UniqueName;
                print(UniqueName + ":    " + contact.thisCollider.name + " hit " + nodeLocation);

                var ComponentScript = this.GetComponentInParent<ComponentScript>();
                var CircuitCreator = this.GetComponentInParent<CircuitCreator>();
                print(contact.thisCollider.name);
                ComponentScript.AddWireNode(contact.thisCollider.name, nodeLocation);

                 
            /*if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
                {
                    ComponentScript.AddedToDictionary = true;
                    CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                    CircuitCreator.ListOfComponents[UniqueName].Add("in", null);
                    CircuitCreator.ListOfComponents[UniqueName].Add("out", null);

                    //CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                }*/
            //else
            //{

            //   CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;

            //}


        }
    }

    /*private void OnCollisionExit(Collision other)
    {
       

            if (other.gameObject.CompareTag("BreadBoardHoles"))
            {
                string nodeLocation = other.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                exitCount++;
                var ComponentScript = this.GetComponentInParent<ComponentScript>();
                ComponentScript.ClearData = true; //change clear data to only clear this component***
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                ComponentScript.AddedToDictionary = false;
            }
        
    }*/

    /* Reorganized Wire script so it would not use Clear Data, and thus not use the Update function
     * 
     * 
     */
    private void OnCollisionExit(Collision other)
    {
        
        if (other.gameObject.CompareTag("BreadBoardHoles"))
        {
            string nodeLocation = other.collider.gameObject.GetComponent<HoleScript>().UniqueName;
            if (other.gameObject.CompareTag("BreadBoardHoles"))
            {
                var ComponentScript = this.GetComponentInParent<ComponentScript>();

                ComponentScript.RemoveFromIRNDictionary(this.name);
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                
            }
        
    }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
