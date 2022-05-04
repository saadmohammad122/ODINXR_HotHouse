using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp;
using SpiceSharp.Components;
using SpiceSharp.Simulations;

using UnityEngine.UI;
using SpiceSharp.Validation;
using System;
namespace Circuits
{
    public class Multimeter : MonoBehaviour
    {
        public bool Power; // Off is False, On is true
        public string PositiveNodeLocation;
        public string NegativeNodeLocation;
        private string Function;
        private string[] InternalResistorNodes;
        private short ResistorNodeCount;
        
        void Start()
        {
            InternalResistorNodes = new string[1];
            ResistorNodeCount = 0;
         
        }

        // Update is called once per frame
        void Update()
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            var Properties = this.GetComponent<Properties>();
            
           

        }
        /*
         * On Collision Enter
         *   
         *          AddtoIRNDictionary(PositiveOrNegative, nodelocation)
         *          ResistorNodeCount += 1
         *          if (ResistorNodeCount == 2) 
         *              CreateResistor()
         *              
         * ONCollisionExit
         * 
         *          ResistorNodeCount -= 1;
         *          if (ResistorNodeCount == 1)
         *              RemoveResistor()
         *          ListOfComponents[uniquename][PositiveOrNegative].Clear();
         *          
         * CreateResistor()
         *      maincircuit.add(new Resistor("uniqueName", LoC[uni...][P], ..., 10)
         * 
         * RemoveResistor ()
         *      maincircuit.remove(uniqueName)
         *      
         */

        public void AddToIRNDictionary(string NodeName, string NodeLocation)
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            var Properties = this.GetComponent<Properties>();
            string UniqueName = Properties.UniqueName;
           

            if (!CircuitScript.ListOfComponents.ContainsKey(UniqueName))
            {
                CircuitScript.ListOfComponents.Add(UniqueName, new Dictionary<string, string>());
                CircuitScript.ListOfComponents[UniqueName].Add("Positive", null);
                CircuitScript.ListOfComponents[UniqueName].Add("Negative", null);

            }
            print("See if it contains at this point:  " + CircuitScript.ListOfComponents.ContainsKey(UniqueName));
            print("See if it contains at this point:  " + CircuitScript.ListOfComponents[UniqueName].ContainsKey("Positive"));
            print("See if it contains at this point:  " + CircuitScript.ListOfComponents[UniqueName].ContainsKey("Negative"));

            print("Properties Name:  " + UniqueName + " Unique Count:  " + ResistorNodeCount);
         
            
            CircuitScript.ListOfComponents[UniqueName][NodeName] = NodeLocation;
            ResistorNodeCount += 1;
            if (ResistorNodeCount == 2)
                CreateResistor();
                
        }
        public void RemoveFromIRNDictionary(string NodeName)
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            var Properties = this.GetComponent<Properties>();
            string UniqueName = Properties.UniqueName;
            ResistorNodeCount -= 1;
            print("This is ResistorNodeCount:   " + ResistorNodeCount);
            if (ResistorNodeCount == 1)
                RemoveResistor();

            CircuitScript.ListOfComponents[UniqueName].Remove(NodeName);
         

        }


        public void DecrementResistorNodeCount ()
        {
            ResistorNodeCount -= 1;
        }
        public short GetResistorNodeCount()
        {
            return ResistorNodeCount;
        }

        

        public void GetDCVoltage()
        {
            var Circuit =  this.GetComponentInParent<CircuitCreator>();
            //Have to write code to reference the voltage source's values here
            // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
            var dc = new DC("DC1", "V1", 0, 12.0, 2);
            // Catch exported data
            dc.ExportSimulationData += (sender, args) =>
            {
                double input = args.GetVoltage(PositiveNodeLocation);
                double output = args.GetVoltage(NegativeNodeLocation);
                PrintVoltage(input - output);
            };
            dc.Run(Circuit.mainCircuit);
        }

        private void PrintVoltage(double Voltage)
        {
            print("This is the voltage between " + PositiveNodeLocation + " and " +
                NegativeNodeLocation + " :  ");
            print(Voltage.ToString("0.00") + " Volts");

        }

       public void GetCurrent()
        {
            var Circuit = this.GetComponentInParent<CircuitCreator>();
            //Have to write code to reference the voltage source's values here
            // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
            var dc = new DC("DC1", "V1", 0, 12.0, 2);
            // Catch exported data
            // Ask Tessa how current ammeter should work.
            // Do we need to break circuit? 
            var inputExport = new RealVoltageExport(dc, PositiveNodeLocation);
            var outputExport = new RealVoltageExport(dc, NegativeNodeLocation);
            var currentExport = new RealPropertyExport(dc, "V1", "i");

            dc.ExportSimulationData += (sender, args) =>
            {
                double input = args.GetVoltage(PositiveNodeLocation);
                double output = args.GetVoltage(NegativeNodeLocation);

                PrintVoltage(input - output);
            };
            dc.Run(Circuit.mainCircuit);
        }

        private void CreateResistor()
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            var Properties = this.GetComponent<Properties>();
            string UniqueName = Properties.UniqueName;
            Resistor NewResistor = new Resistor(UniqueName, CircuitScript.ListOfComponents[UniqueName]["Positive"],
                CircuitScript.ListOfComponents[UniqueName]["Negative"], 10);
            print("Created new resistor of name:   " + UniqueName);
            CircuitScript.NumOfComponents += 1;
            CircuitScript.mainCircuit.Add(NewResistor);
        }

        private void RemoveResistor()
        {
            var CircuitScript = this.GetComponentInParent<CircuitCreator>();
            var Properties = this.GetComponent<Properties>();
            string UniqueName = Properties.UniqueName;
            CircuitScript.mainCircuit.Remove(UniqueName);
            CircuitScript.NumOfComponents -= 1;
        }
        

    }
}
