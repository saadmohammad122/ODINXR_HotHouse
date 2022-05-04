using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{

    private int exitCount;

    // Start is called before the first frame update
    void Start()
    {

    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<NodeScript>().enabled == true)
        {
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                print(this.name + "Got to OnCollisionEnter!");
                //collision.collider.transform.position = new Vector3(this.gameObject.transform.position.x, .855f, this.gameObject.transform.position.z);
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .88f, this.gameObject.transform.position.z);
                //collision.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                
             
                ContactPoint contact = collision.contacts[0];
                string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;
                print(UniqueName + ":    "  + contact.thisCollider.name + " hit " + nodeLocation);
                
                var ComponentScript = this.GetComponent<ComponentScript>();
                var CircuitCreator = this.GetComponentInParent<CircuitCreator>();
                


                
                if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
                {
                    // Create an entry. We have to do this because we need to create a new dictionary within our dictionary
                 
                    ComponentScript.AddedToDictionary = true;
                    CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());

                   
                    // Set in/out. we don't know what it is and we don't need to

                    //ComponentScript.NodeList[UniqueName][contact.thisCollider.name] = nodeLocation;
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                    //print(ComponentScript.NodeList[UniqueName][contact.thisCollider.name]);
                    //print(ComponentScript.NodeList[UniqueName].Count);
                    // If the component is already in the dictionary
                }
                else
                {                    // Just add in/out
                                     //print(contact.thisCollider.name);

                    //ComponentScript.NodeList[UniqueName][contact.thisCollider.name] = nodeLocation;
                    CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
                    foreach (KeyValuePair<string, string> kvp in ComponentScript.NodeList[UniqueName])
                    {

                        Debug.Log("Key = " + kvp.Key + " Value = " + kvp.Value);
                    }


                }

                if ((CircuitCreator.ListOfComponents[UniqueName].Count == this.GetComponent<Properties>().numberOfInput))
                    ComponentScript.SendDataToCircuitCreator();

                

                    
            }
        }
    }

    
    private void OnCollisionExit(Collision other)
    {
        if (exitCount < 1)
        {

            if (other.gameObject.CompareTag("BreadBoardHoles"))
            {
                string nodeLocation = other.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                exitCount++;
                var CircuitScript = this.GetComponent<ComponentScript>();
                CircuitScript.ClearData = true; //change clear data to only clear this component***
                this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                CircuitScript.AddedToDictionary = false;
            }
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        print("NodeScript");
        ContactPoint contact = collision.contacts[0];
        string ContactName = contact.thisCollider.name;
        if (collision.gameObject.CompareTag("BreadBoardHoles") && (ContactName == "In" || ContactName == "Out"))
        {
            //collision.collider.transform.position = new Vector3(this.gameObject.transform.position.x, .855f, this.gameObject.transform.position.z);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1.14f, this.gameObject.transform.position.z);
            //collision.collider.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;
            print(UniqueName + ":    " + contact.thisCollider.name + " hit " + nodeLocation);

            var ComponentScript = this.GetComponent<ComponentScript>();
            var CircuitCreator = this.GetComponentInParent<CircuitCreator>();

            if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
            {
                // Create an entry. We have to do this because we need to create a new dictionary within our dictionary

                ComponentScript.AddedToDictionary = true;
                CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
        
            }
            else
            {
                CircuitCreator.ListOfComponents[UniqueName][contact.thisCollider.name] = nodeLocation;
            }

            if ((CircuitCreator.ListOfComponents[UniqueName].Count == this.GetComponent<Properties>().numberOfInput))
                ComponentScript.SendDataToCircuitCreator();


        }
    }




    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("BreadBoardHoles"))
        {
            var CircuitScript = this.GetComponent<ComponentScript>();
            CircuitScript.AddedToDictionary = false;
            CircuitScript.RemoveComponent();
            this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

    }
    // Update is called once per frame
    void Update()
    {

    }

}
