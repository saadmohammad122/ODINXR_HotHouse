using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetecion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

     private void OnCollisionEnter(Collision collision)
    {
        //this.GetComponent<Rigidbody>().isKinematic = true;
        //this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //this.GetComponent<Rigidbody>().useGravity = false;

        if (this.GetComponent<CollisionDetecion>().enabled == true)
        {
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                Debug.Log("Detected?");
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                print("nodeLocation:  " + nodeLocation);
            }
        }
    }
        // Update is called once per frame
        void Update()
        {
            
        }   
}
