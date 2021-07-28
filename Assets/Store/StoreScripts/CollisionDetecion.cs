using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollisionDetecion : MonoBehaviour
{

    private int exitCount;

    // Start is called before the first frame update
    void Start()
    {
       
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (this.GetComponent<CollisionDetecion>().enabled == true)
        {
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .855f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                ContactPoint contact = collision.contacts[0]; 
                print( contact.thisCollider.name + " hit " + nodeLocation);
                exitCount = 0;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (exitCount < 2)
        {

            if (other.gameObject.CompareTag("BreadBoardHoles"))
            {

                string nodeLocation = other.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                print(this.gameObject.name + " exiting " + nodeLocation);
                exitCount++;
            }
        }
    }
        // Update is called once per frame
        void Update()
        {
            
        }


}
