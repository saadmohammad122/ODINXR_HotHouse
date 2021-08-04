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
        public Dictionary<string, string> NodeList = new Dictionary<string, string>();

        // Start is called before the first frame update
        void Start()
        {
            ComponentName = this.GetComponent<Properties>().UniqueName;
            ComponentValue = this.GetComponent<Properties>().Value;
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            CircuitScript.numOfComponents += 1;
        }
        
        
        // Update is called once per frame
        void Update()
        {
            while (FrameCount > 50)
            {
                FrameCount = 0;
                var CircuitScript = this.GetComponentInParent<CircuitCreator>();
                if (ClearData)
                {
                //removed component
                    RemoveComponent(ComponentName);
                    NodeList.Clear();
                    ClearData = false;
                }

                else if ((NodeList.Count == (this.GetComponent<Properties>().numberOfInput)) & !DataHasBeenSent)    // Every input has been accounted for in the NodeList
            {
                    SendDataToCircuitCreator();
                    //CircuitScript.ListOfComponents.Add(ComponentName, NodeList);
                    //CircuitScript.numOfComponents += 1;
                    DataHasBeenSent = true;


                }
            }
            FrameCount++;
        }



        private void SendDataToCircuitCreator()
        {
            var Attributes = this.GetComponentInParent<Properties>();
            string ComponentType = Attributes.Type;
            int ComponentValue = Attributes.Value;

            switch (ComponentType)
            {
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
            print(NodeList["In"] + NodeList["Out"]);
            Resistor NewResistor = new Resistor(ComponentName, "in", "out", ComponentValue); //NodeList["In"], NodeList["Out"]
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            CircuitScript.mainCircuit.Add(NewResistor);
            
        }

        private void RemoveComponent(string ComponentName)
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            CircuitScript.mainCircuit.Remove(ComponentName);
            
    }
    }


