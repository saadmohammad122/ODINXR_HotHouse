  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;

public class ComponentScript : MonoBehaviour
    {
        public int numOfInputs = 2;
        private int FrameCount = 0;
        public GameObject component;
        public bool DataHasBeenSent = false;
        public bool ClearData = false;
        public string ComponentName;        // The unique name for each Component in the game 

        // public List<KeyValuePair<string, string>> NodeList;
        public Dictionary<string, string> NodeList = new Dictionary<string, string>();


        // Start is called before the first frame update
        void Start()
        {
            System.Random numberGenerator = new System.Random();

            component = this.gameObject;

            //if function for repeats may need to be considered
            ComponentName = this.name + numberGenerator.Next(1, 100000).ToString();

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
                    CircuitScript.ListOfComponents.Remove(this.name);
                    NodeList.Clear();
                    ClearData = false;
                }

                else if ((NodeList.Count == (numOfInputs)) & !DataHasBeenSent)    // Every input has been accounted for in the NodeList
                {
                    SendDataToCircuitCreator();

                    //CircuitScript.ListOfComponents.Add(ComponentName, NodeList);
                    //CircuitScript.numOfComponents += 1;
                    DataHasBeenSent = true;


                }
                /*foreach (var p in NodeList)
                {
                    print(p);
                }*/

            }
            FrameCount++;
        }



        private void SendDataToCircuitCreator()
        {
            var Attributes = this.GetComponentInParent<Properties>();
            string ComponentType = Attributes.Type;
            int ComponentValue = Attributes.Value;
            SpiceSharp.Components.Component newComponent;

            switch (ComponentType)
            {
                case "Resistor":
                    newComponent = CreateResistor(ComponentValue);
                    break;
                // Include other case's for what is considered a component Type;
                default:
                    newComponent = null;
                    break;

            }

            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            CircuitScript.ListOfComponents.Add(ComponentName, newComponent);
            CircuitScript.numOfComponents += 1;
            return;

        }

        //What is the purpose of this?
        private SpiceSharp.Components.Component CreateResistor(int ComponentValue)
        {
            Resistor NewResistor = new Resistor(ComponentName, NodeList["In"], NodeList["Out"], ComponentValue);
            return NewResistor;
        }


    }


