using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;

public class CircuitCreator : MonoBehaviour
{
    public int numOfComponents = 0;
    private int FrameCount = 0;
    //public Dictionary<string, Dictionary <string, string> > ListOfComponents = new Dictionary<string, Dictionary<string, string>>();
    
    // Gave ComponentScript the ability to create SpiceSharp Components with the breadboard positioning, and then hand that component to CircuitCreator
    public Dictionary<string, SpiceSharp.Components.Component> ListOfComponents = new Dictionary<string, SpiceSharp.Components.Component>();

    private int CircuitCount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        CircuitCount = ListOfComponents.Count;
    }

    // Update is called once per frame
    void Update()
    {
        while (FrameCount > 50)
        {
            FrameCount = 0;

            if (CircuitCount != numOfComponents)   // when we should update spicesharp
            {
                // rerun spicesharp


                CircuitCount = ListOfComponents.Count;
                
            }
            /*
            foreach (var p in ListOfComponents)
            {
                print(p);
            }*/
        }
        FrameCount++;
    }


    private void UpdateSpiceSharp ()
    {
        int index = 0;
        foreach (var item in ListOfComponents)
        {
            print(item.Key);
            
            
        }
        
        // Spice sharp will have a List of components, that is made up of "Spicesharp.Components".
        //      ie: List: (Resistor r1, Resistor r2, VoltageSource v1, ...) 

        // Idea 1: 
        //  Two Dictionaries in Circuit Creator, one is updated by the front end components and the other is updated periodocially. 
        //      Initalize Dict1 and Dict2 to be equal. 
        //      Here, if Dict1 (updated by FEC) is changed, there is a period of time where Dict2 is not updated. 
        //      Find which index Dict2 is different than Dict1, and push that change to Spicesharp so that you don't have to update the whole list. 

        // Idea 2: 
        //  If there is any change to the circuit at all, clear the old circuit and create a new circuit for spice sharp to operate on. 



    }





}

