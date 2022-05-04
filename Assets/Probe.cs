using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circuits
{

    public class Probe : MonoBehaviour
    {

        public string LeadLocation;
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
           
            if (collision.gameObject.CompareTag("BreadBoardHoles"))
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1.14f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                print("ProbeScript");
                //this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                //this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                var Multimeter = this.GetComponentInParent<Multimeter>();
                LeadLocation = nodeLocation;
                if (transform.parent.name == "Positive")
                {
                    Multimeter.PositiveNodeLocation = nodeLocation;
                }
                else
                {
                    Multimeter.NegativeNodeLocation = nodeLocation;

                }
                Multimeter.AddToIRNDictionary(this.transform.parent.name, nodeLocation);

            }
            if (collision.gameObject.CompareTag("Resistor"))
            {
                print("I hit the resistor");
            }

        }

        private void OnCollisionExit(Collision other)
        {
                if (other.gameObject.CompareTag("BreadBoardHoles"))
                {
                    var Multimeter = this.GetComponentInParent<Multimeter>();
                    LeadLocation = "null";
                    if (transform.parent.name == "Positive")
                    {
                        Multimeter.PositiveNodeLocation = "null";
                    }
                    else
                    {
                        Multimeter.NegativeNodeLocation = "null";

                    }
                Multimeter.RemoveFromIRNDictionary(this.transform.parent.name);
                    this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
        }

        }

        


    }
