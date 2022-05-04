using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("lead");
        ContactPoint contact = collision.contacts[0];
        string ContactName = contact.thisCollider.name;
        if (collision.gameObject.CompareTag("BreadBoardHoles") && (ContactName == "Out"))
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1.14f, this.gameObject.transform.position.z);
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
            string UniqueName = this.gameObject.GetComponentInParent<Properties>().UniqueName;
            print(UniqueName + ":    " + contact.thisCollider.name + " hit " + nodeLocation);

            
            var ComponentScript = this.GetComponentInParent<ComponentScript>();
            var CircuitCreator = this.GetComponentInParent<CircuitCreator>();

            ComponentScript.AddWireNode(this.transform.parent.name, nodeLocation);
            /*
            if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
            {
                // Create an entry. We have to do this because we need to create a new dictionary within our dictionary

                ComponentScript.AddedToDictionary = true;
                CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                CircuitCreator.ListOfComponents[UniqueName][this.transform.parent.name] = nodeLocation;

            }
            else
            {
                CircuitCreator.ListOfComponents[UniqueName][this.transform.parent.name] = nodeLocation;
            }
            
            if ((CircuitCreator.ListOfComponents[UniqueName].Count == this.GetComponentInParent<Properties>().numberOfInput))
                ComponentScript.SendDataToCircuitCreator();
            */

        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("BreadBoardHoles"))
        {
            var ComponentScript = this.GetComponentInParent<ComponentScript>();
            ComponentScript.AddedToDictionary = false;
            //CircuitScript.RemoveComponent();
            ComponentScript.RemoveFromIRNDictionary(this.transform.parent.name);
            this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }

    }







}
