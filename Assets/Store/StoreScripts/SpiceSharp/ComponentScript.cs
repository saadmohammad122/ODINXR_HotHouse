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
    public GameObject Component;
    public bool DataHasBeenSent = false;
    public bool ClearData = false;
    public string ComponentName;
    public int ComponentValue;
    public Dictionary<string, Dictionary<string, string>> NodeList = new Dictionary<string, Dictionary<string, string>>();

    public bool AddedToDictionary;
   

    // Start is called before the first frame update
    void Start()
    {
       // ComponentName = this.GetComponent<Properties>().UniqueName;
        ComponentValue = this.GetComponent<Properties>().Value;
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        CircuitScript.NumOfComponents += 1;
        AddedToDictionary = false;
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







    // Update is called once per frame
    void Update()
    {
        while (FrameCount > 50)
        {
            /* We need to write the Component Name here, because Unity calls Component Script first, 
             * and then properties script. This means that the Unique name is not 
             * generated in time for Component Script to grab it.    
            */
            ComponentName = this.GetComponent<Properties>().UniqueName;

            FrameCount = 0;
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            //print(CircuitScript.mainCircuit.Count);
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
               
                if ((CircuitScript.ListOfComponents[UniqueName].Count == this.GetComponent<Properties>().NumberOfInput) &
                        !DataHasBeenSent)
                {
                    //print(UniqueName + " datahasbeensent:   " + DataHasBeenSent);
                    //print("Got as far as adding to the dictionary 1");
                    SendDataToCircuitCreator();
                    DataHasBeenSent = true;
                }
            }
            
        }
        FrameCount++;
    }



    private void SendDataToCircuitCreator()
    {
        //var Attributes = this.GetComponentInParent<Properties>();
        var Attributes = this.GetComponent<Properties>();
        string ComponentType = Attributes.Type;
        //print("In send data to Circuit Creator :  " + ComponentType);

        int ComponentValue = Attributes.Value;
        //print("In send data to Circuit Creator value :  " + ComponentValue);

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

    private void CreateVoltageSource(int ComponentValue)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;

        /*print("Component Name:   " + ComponentName + "\n" + "In:      " + CircuitScript.ListOfComponents[UniqueName]["Positive"] + "\n" +
            "Out:     " + CircuitScript.ListOfComponents[UniqueName]["Negative"]);*/

        VoltageSource NewVoltageSource = new VoltageSource("VoltageSource", CircuitScript.ListOfComponents[UniqueName]["Positive"],
        CircuitScript.ListOfComponents[UniqueName]["Negative"], Convert.ToDouble(ComponentValue));


        CircuitScript.mainCircuit.Add(NewVoltageSource);
        //print("This is the circuit Count:    " + CircuitScript.mainCircuit.Count);

        CircuitScript.NumOfComponents += 1;
    }

    private void CreateResistor(int ComponentValue)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        string UniqueName = this.gameObject.GetComponent<Properties>().UniqueName;

        /*print("Component Name:" + ComponentName + "\n" + "In:" + CircuitScript.ListOfComponents[UniqueName]["In"] + "\n" +
            "Out:" + CircuitScript.ListOfComponents[UniqueName]["Out"]);*/

        Resistor NewResistor = new Resistor(ComponentName, CircuitScript.ListOfComponents[UniqueName]["In"], 
        CircuitScript.ListOfComponents[UniqueName]["Out"], ComponentValue);
        
        
        CircuitScript.mainCircuit.Add(NewResistor);
        //print("This is the circuit Count:    " + CircuitScript.mainCircuit.Count);

        CircuitScript.NumOfComponents += 1;
    }

    private void RemoveComponent(string ComponentName)
    {
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        CircuitScript.mainCircuit.Remove(ComponentName);
        CircuitScript.ListOfComponents.Remove(ComponentName);
        return;

    }
}



