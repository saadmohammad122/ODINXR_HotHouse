using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndComponent : MonoBehaviour
    {


        public Collider collider;


        // Start is called before the first frame update
        void Start()
        {
            //var NodeScript = this.GetComponent<>();

        }



        private void OnCollisionEnter(Collision collision)
        {

            print("ONCollision Enter:   " + collision.gameObject.name);
            print("ONCollision Enter Collider Name:   " + collider.name);
            print("Parent Name:   " + collider.transform.parent.name);




        }

        private void OnTriggerEnter(Collider other)
        {


            print("On Trigger Enter: " + other.gameObject.name);
            print("On Trigger Enter Collider Name:   " + collider.name);
            print("Parent Name:   " + collider.transform.parent.Find("Out").name);

        }
        //  Create Central script in Resistor
        //  Create script for nodes
        //      Nodes will send name and hole name to Central Script
        //  Central Script sends data to Spice Sharp Component Dictionary



        /*
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
        */


        // Update is called once per frame
        void Update()
        {

        }
    }