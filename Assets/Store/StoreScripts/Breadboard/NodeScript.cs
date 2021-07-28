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

    private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<NodeScript>().enabled == true)
        {
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .855f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                ContactPoint contact = collision.contacts[0];
                print(contact.thisCollider.name + " hit " + nodeLocation);
                var ComponentScript = this.GetComponent<ComponentScript>();
                if (!ComponentScript.NodeList.ContainsKey(contact.thisCollider.name))
                {
                    ComponentScript.NodeList.Add(contact.thisCollider.name, nodeLocation);
                }
                exitCount = 0;

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
                print(this.gameObject.name + " exiting " /* + nodeLocation*/); 
                exitCount++;
                var CircuitScript = this.GetComponent<ComponentScript>();
                CircuitScript.ClearData = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
