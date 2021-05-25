using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndComponent : MonoBehaviour
{

    public Collider input;
/*
    public Collider input;
    public Collider output;
*/
    // Start is called before the first frame update
    void Start()
    {
       // print("Hey!");
       // Debug.Log("hey this works btw!!!");
    }



    private void OnCollisionEnter(Collision input)
    {
        print("Got to OnCollisionEnter");
        print(input.gameObject.transform.name);
        
        
        if (input.gameObject.name == "TestCube")
        {
            print("Hey!");
            print(input.gameObject.name);
            Debug.Log(input.gameObject.name);
        }
        if (input.gameObject.tag == "BreadBoardHoles")
        {
            print("Hey!");
            print(input.gameObject.name);
            Debug.Log(input.gameObject.name);
        }

    }
    private void OnCollisionStay(Collision col)
    {
        print("Got to OnCollisionStay");
        print(col.gameObject.name);


        if (col.gameObject.name == "TestCube")
        {
            print("Hey!");
            print(col.gameObject.name);
            Debug.Log(col.gameObject.name);
        }
        if (col.gameObject.tag == "BreadBoardHoles")
        {
            print("Hey!");
            print(col.gameObject.name);
            Debug.Log(col.gameObject.name);
        }

    }


    /*   private void OnCollisionEnter(Collision collision)
       {
           foreach(ContactPoint contact in collision.contacts)
           {
               print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
               Debug.DrawRay(contact.point, contact.normal, Color.white);
           }
       }
    */



    // Update is called once per frame
    void Update()
    {
        
    }
}
