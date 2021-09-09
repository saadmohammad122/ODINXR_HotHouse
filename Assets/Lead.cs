using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circuits
{

    public class Lead : MonoBehaviour
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
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, .8798f, this.gameObject.transform.position.z);
                this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                string nodeLocation = collision.collider.gameObject.GetComponent<HoleScript>().UniqueName;
                string UniqueName = this.GetComponentInParent<Properties>().UniqueName;
                ContactPoint contact = collision.contacts[0];
                
                    var ComponentScript = this.GetComponentInParent<ComponentScript>();
                    var CircuitCreator = this.GetComponentInParent<CircuitCreator>();

                    if (!CircuitCreator.ListOfComponents.ContainsKey(UniqueName))
                    {
                        //print("Adding itself to circuitCreator!");
                        ComponentScript.AddedToDictionary = true;
                        CircuitCreator.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                        CircuitCreator.ListOfComponents[UniqueName][this.transform.parent.name] = nodeLocation;
                    }
                    else
                    {
                        //print("Now we are adding ourselves to the list of components ");
                        CircuitCreator.ListOfComponents[UniqueName][this.transform.parent.name] = nodeLocation;
                    }
                /*foreach (KeyValuePair<string, string> kvp in CircuitCreator.ListOfComponents[UniqueName])
                {

                 Debug.Log("Key = " + kvp.Key + " Value = " + kvp.Value);
                }*/

            }

        }

        private void OnCollisionExit(Collision other)
        {
                if (other.gameObject.CompareTag("BreadBoardHoles"))
                {
                    this.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                    this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    var ComponentScript = this.GetComponentInParent<ComponentScript>();
                    ComponentScript.ClearData = true;
                    ComponentScript.AddedToDictionary = false;
            }
            }
        }


    }
