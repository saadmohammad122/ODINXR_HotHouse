using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;

public class ComponentScript : MonoBehaviour
{
    private int FrameCount = 0;
    public GameObject component;
    public bool DataHasBeenSent = false;
    public bool ClearData = false;
    public string ComponentName;
    public int ComponentValue;
    public Dictionary<string, Dictionary<string, string>> NodeList = new Dictionary<string, Dictionary<string, string>>();
    public GameObject ParentObject;
    public bool AddedToDictionary;
    
    private int WireClearData = 0;
    private short ResistorNodeCount;


    // Start is called before the first frame update
    void Start()
    {
       // ComponentName = this.GetComponent<Properties>().UniqueName;
        ComponentValue = this.GetComponent<Properties>().Value;
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        CircuitScript.NumOfComponents += 1;
        AddedToDictionary = false;

        ResistorNodeCount = 0;
    }


    /* 
     *  Important Note for Cole:
     * 
     *      I think we can take out the Update function in this file. Right now, the Update functions to check every 50 frames if the 
     *      current component has a completely filled dictionary. That means that all inputs have been added. Additionally, if 
     *      onCollisionExit is triggered, this update function checks and deletes the component from the ListOfComponents. 
     *      
     *      To make a simpler system, why don't we check if all inputs have been added in the Node Script stage? Every time OncollisionEnter
     *      is triggered, we can check if our dictionary is "full". If it is, we add it to the spice sharp circuit right there. If it isn't, we wait 
     *      until the next OnCollsionEnter. Same thing for OnCollisionExit, but when OCExit is triggered we delete the component from the spice sharp
     *      dictionary. This assumes that no component can be half plugged into the board. 
     *      
     */

    /*
    public int WireClearData = 0;
    
    WireClearData += 1; (every time wire OnCollisionExit happens) 
    if WCD == 1 and Clear Data (one node is in the board, the other is out)
        Remove from spice sharp
        Remove one node from dictionary
    if WCD == 2 and Clear Data
        Remove one node from dictionary
        WCD = 0
        ClearData = False


        
        

    */


    // Update is called once per frame

    /*
    void Update()
    {
        while (FrameCount > 50)
        {
           
            ComponentName = this.GetComponent<Properties>().UniqueName;

            FrameCount = 0;
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;
            if (ClearData)
            {
              
                RemoveComponent(ComponentName);
                ClearData = false;
                DataHasBeenSent = false;
            }


            // else if ((NodeList[UniqueName].Count == (this.GetComponent<Properties>().numberOfInput)) & !DataHasBeenSent)    // Every input has been accounted for in the NodeList
            
            else if (AddedToDictionary) // AddedToDictionary is a temporary filter to prevent components that 
                                        // that are not added to the board from triggering the below If statement.
            {

                if ((CircuitScript.ListOfComponents[UniqueName].Count == this.GetComponent<Properties>().numberOfInput) &
                        !DataHasBeenSent)
                {
                    print(UniqueName + " datahasbeensent:   " + DataHasBeenSent);

                    print("Got as far as adding to the dictionary 1");
                    SendDataToCircuitCreator();
                    DataHasBeenSent = true;
                }
            }
            
        }
        FrameCount++;
    } */

    

  
    public void AddWireNode(String NodeName, string NodeLocation)
    {
        AddToIRNDictionary(NodeName, NodeLocation);

        return;
    }

    private void AddToIRNDictionary(string NodeName, string NodeLocation)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        var Properties = this.GetComponent<Properties>();
        string UniqueName = Properties.UniqueName;
        if (!CircuitScript.ListOfComponents.ContainsKey(UniqueName))
        {
            CircuitScript.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
            if (Properties.Type == "VoltageSource")
            {
                CircuitScript.ListOfComponents[UniqueName].Add("Negative", null);
                CircuitScript.ListOfComponents[UniqueName].Add("Positive", null);
            }
            else
            {
                CircuitScript.ListOfComponents[UniqueName].Add("In", null);
                CircuitScript.ListOfComponents[UniqueName].Add("Out", null);
            }
        }
        
        CircuitScript.ListOfComponents[UniqueName][NodeName] = NodeLocation;
        print("This is Unique Name!  " + UniqueName );
        ResistorNodeCount += 1;
        if (ResistorNodeCount == 2)
            SendDataToCircuitCreator();
    }


    public void RemoveWireNode(string NodeName)
    {
        RemoveFromIRNDictionary(NodeName);
    }

    public void RemoveFromIRNDictionary(string NodeName)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        var Properties = this.GetComponent<Properties>();
        string UniqueName = Properties.UniqueName;
        ResistorNodeCount -= 1;
        print("Unique Name :  " + UniqueName + " Nodename   : " + NodeName + "       resistor Node Count: " + ResistorNodeCount );
        if (ResistorNodeCount == 1)
        {
            if (Properties.Type == "Resistor")
                RemoveResistor();
            else if (Properties.Type == "VoltageSource")
                RemoveVoltageSource();
            else
                RemoveComponent();

        }
        print(CircuitScript.mainCircuit.Count);
        CircuitScript.ListOfComponents[UniqueName][NodeName] = null;
        foreach (KeyValuePair<string, string> kvp in CircuitScript.ListOfComponents[UniqueName])
        {

            Debug.Log("Key = " + kvp.Key + " Value = " + kvp.Value);
        }

    }


    public void DecrementResistorNodeCount()
    {
        ResistorNodeCount -= 1;
    }
    public void IncrementResistorNodeCount()
    {
        ResistorNodeCount += 1;
    }
    public short GetResistorNodeCount()
    {
        return ResistorNodeCount;
    }


    public void SendDataToCircuitCreator()
    {
        //var Attributes = this.GetComponentInParent<Properties>();

        var Attributes = this.GetComponent<Properties>();
        string ComponentType = Attributes.Type;

        int ComponentValue = Attributes.Value;
        print("Got to SendDatatoCircuitCreator! for:  " + Attributes.UniqueName);
        switch (ComponentType)
        {
            case "VoltageSource":
                CreateVoltageSource(ComponentValue);
                break;
            case "Resistor":
                CreateResistor(ComponentValue);
                break;
            // Include other case's for what is considered a component Type;
            default:
                break;

        }

        return;

    }

    private void CreateResistor(int ComponentValue)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;

        print("Component Name:   " + UniqueName + "\n" + "In:      " + CircuitScript.ListOfComponents[UniqueName]["In"] + "\n" + 
            "Out:     " + CircuitScript.ListOfComponents[UniqueName]["Out"]);

        Resistor NewResistor = new Resistor(UniqueName, CircuitScript.ListOfComponents[UniqueName]["In"], 
        CircuitScript.ListOfComponents[UniqueName]["Out"], ComponentValue);
        
        
        CircuitScript.mainCircuit.Add(NewResistor);
        print("This is the circuit Count:    " + CircuitScript.mainCircuit.Count);
        print("Created new resistor of name:   " + UniqueName);

        CircuitScript.NumOfComponents += 1;
    }

    private void CreateVoltageSource(int ComponentValue)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;

       /* print("Component Name:   " + ComponentName + "\n" + "In:      " + CircuitScript.ListOfComponents[UniqueName]["Positive"] + "\n" +
            "Out:     " + CircuitScript.ListOfComponents[UniqueName]["Negative"]);*/

        VoltageSource NewVoltageSource = new VoltageSource(UniqueName, CircuitScript.ListOfComponents[UniqueName]["Positive"],
        CircuitScript.ListOfComponents[UniqueName]["Negative"], Convert.ToDouble(ComponentValue));


        CircuitScript.mainCircuit.Add(NewVoltageSource);
        //print("This is the circuit Count:    " + CircuitScript.mainCircuit.Count);

        CircuitScript.NumOfComponents += 1;
    }

    public void RemoveComponent()
    {
        var Attributes = this.GetComponent<Properties>();
        string UniqueName = Attributes.UniqueName;
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        if (CircuitScript.ListOfComponents.ContainsKey(UniqueName))
        {
            CircuitScript.mainCircuit.Remove(UniqueName);
            CircuitScript.ListOfComponents.Remove(UniqueName);
        } 
        
        return;

    }

    private void RemoveVoltageSource()
    {
        var Attributes = this.GetComponent<Properties>();
        string UniqueName = Attributes.UniqueName;
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        if (CircuitScript.ListOfComponents.ContainsKey(UniqueName))
            CircuitScript.mainCircuit.Remove(UniqueName);
        
        return;
    }

    private void RemoveResistor()
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        var Properties = this.GetComponent<Properties>();
        string UniqueName = Properties.UniqueName;
        CircuitScript.mainCircuit.Remove(UniqueName);
        print("thjis is RemoveResistor count:  " + CircuitScript.mainCircuit.Count);
    }



    public void EnablePhysics ()
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        print("Got to enable physics for:  " + this.name);
        print(this.gameObject.GetComponent<Rigidbody>().isKinematic == false);
        this.transform.SetParent(ParentObject.transform);


    }

}



