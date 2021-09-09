using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;
using UnityEngine.UI;
using SpiceSharp.Validation;
using System;


public class CircuitCreator : MonoBehaviour
 {
        public int NumOfComponents = 1;
        private int FrameCount = 0;
        public Dictionary<string, Dictionary <string, string> > ListOfComponents = new Dictionary<string, Dictionary<string, string>>();
        public Circuit mainCircuit;
        public Camera FPSCam;
        public Circuit TestCircuit;
        public System.Random numberGenerator;
        private Circuit ACCircuit;
        public GameObject VoltButton;
        public GameObject AmpsButton;
        public GameObject ResistButton;
        public GameObject VoltageSource;
        public double VoltageValue;
        public GameObject AmpWire;
        private string UniqueName;
        private Sine NewSinWave;

    // Start is called before the first frame update

    private void Awake()
    {
        numberGenerator = new System.Random();

    }

    void Start()
        {

            mainCircuit = new Circuit(new Resistor("GroundResistor", "NegativeLeftColliders", "0", 0));
    
            TestCircuit = new Circuit(new VoltageSource("V1", "PositiveLeftColliders", "0", 12.0), new Resistor("GroundWire", "NegativeLeftColliders", "0", 0),
            new Resistor("Wire1", "PositiveLeftColliders", "LeftRow1", 0), new Resistor("Resistor1", "LeftRow1", "LeftRow4", 100), 
            new Resistor("Resistor2", "LeftRow4", "LeftRow7", 100), new Resistor("AmpWire", "LeftRow7", "NegativeLeftColliders", 0));

            NewSinWave = new Sine(0, 2.0, 1000);

            ACCircuit = new Circuit(new VoltageSource("V1", "in", "0", new Pulse(0.0, 5.0, 0.01, 1e-3, 1e-3, 0.02, 0.04)),
            new Resistor("R1", "in", "out", 10.0e3),
            new Capacitor("C1", "out", "0", 1e-6));


       






    }

    // Update is called once per frame
    void Update()
    {
        VoltageValue = Convert.ToDouble(VoltageSource.GetComponent<ComponentScript>().ComponentValue);

        if (Input.GetMouseButtonDown(0))
        {
            var ray = FPSCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {

                    if (hit.collider.name == VoltButton.name)
                    {
                        
                        //print(mainCircuit.Count);
                        
                        //print(VoltageValue);
                        //print(VoltageValue.GetType());

                        // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
                        var dc = new DC("DC1", "VoltageSource", VoltageValue, VoltageValue, 1);
                        var inputExport = new RealVoltageExport(dc, "LeftRow1");
                        var outputExport = new RealVoltageExport(dc, "LeftRow7");

                        // Catch exported data
                        dc.ExportSimulationData += (sender, args) =>
                        {
                            var input = inputExport.Value;
                            var output = outputExport.Value;

                            //var input = args.GetVoltage("+"); 
                            //var output = args.GetVoltage("0");
                            print("\ninput :  " + input + "\n" + "output: " + output);
                        };

                        dc.Run(mainCircuit);
                        /*try
                        {
                            dc.Run(mainCircuit);
                        }
                        catch (Exception e)
                        {
                            var rules = mainCircuit.Validate();
                            if (rules.ViolationCount > 0)
                            {
                                // We have rules that were violated
                                foreach (var violation in rules.Violations)
                                {
                                    mainCircuit.
                                    print(violation.Rule);
                                }
                            }*/

                        //return new ValidationResult(false, $"Illegal characters or {e.Message}");


                    }

                    if (hit.collider.name == AmpsButton.name)
                    {

                        //print(mainCircuit.Count);

                        // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
                        var dc = new DC("DC1", "VoltageSource", VoltageValue, VoltageValue, 1);
                        var inputExport = new RealVoltageExport(dc, "PositiveLeftColliders");
                        var outputExport = new RealVoltageExport(dc, "NegativeLeftColliders");
                        var currentExport = new RealPropertyExport(dc, AmpWire.GetComponent<Properties>().UniqueName, "i");

                        // Catch exported data
                        dc.ExportSimulationData += (sender, args) =>
                        {
                            var input = inputExport.Value;
                            var output = outputExport.Value;
                            var current = currentExport.Value;

                            //var input = args.GetVoltage("+"); 
                            //var output = args.GetVoltage("0");
                            print("\ninput :  " + input + "\n" + "output: " + output + "\ncurrent" + current);
                        };

                        dc.Run(mainCircuit);
                        
                    }

                    /*if (hit.collider.name == ResistButton.name)
                    {
                        mainCircuit.Add(new Resistor("ResistResistor", "LeftRow7", "NegativeLeftColliders", 0));

                        // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
                        var dc = new DC("DC1", "VoltageSource", VoltageValue, VoltageValue, 1);
                        var inputExport = new RealVoltageExport(dc, "LeftRow1");
                        var outputExport = new RealVoltageExport(dc, "LeftRow7");
                        var resistanceExport = new RealPropertyExport(dc, "ResistResistor", "r");

                        // Catch exported data
                        dc.ExportSimulationData += (sender, args) =>
                        {
                            var input = inputExport.Value;
                            var output = outputExport.Value;
                            var resistance = resistanceExport.Value;

                            //var input = args.GetVoltage("+"); 
                            //var output = args.GetVoltage("0");
                            print("Resistance" + resistance);
                        };

                        dc.Run(mainCircuit);
                    }*/
                        
                        /*
                        var ac = new AC("AC-1", new DecadeSweep(1e-2, 1.0e3, 10));
                        // Make the export
                        var exportVoltage = new ComplexVoltageExport(ac, "out");

                        // Simulate
                        ac.ExportSimulationData += (sender, args) =>
                        {
                            var output = exportVoltage.Value;
                            var decibels = 10.0 * Math.Log10(output.Real * output.Real + output.Imaginary * output.Imaginary);
                            print("output :  " + output + "\n" + "Decibels: " + decibels);
                        };
                        ac.Run(ACCircuit);
                        */

                        /*
                        // Create the simulation
                        var tran = new Transient("Tran 1", 1e-3, 1);

                        // Make the exports
                        var inputExport = new RealVoltageExport(tran, "in");
                        var outputExport = new RealVoltageExport(tran, "out");

                        // Simulate
                        tran.ExportSimulationData += (sender, args) =>
                        {
                            var input = inputExport.Value;
                            var output = outputExport.Value;
                            print("\ninput :  " + input + "\n" + "output: " + output);

                        };
                        tran.Run(ACCircuit);
                        */

                        //}
                        new WaitForSecondsRealtime(10);
                }
            }
            
        }
    }
}

