using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;

public class mycircuits : MonoBehaviour
    {
        public Text compText;
        public Text outText;
        public VoltageSource v1 = new VoltageSource("V1", "in", "0", 1);
        public Resistor r1 = new Resistor("r1", "in", "out", 1.0e3);
        public Resistor r2 = new Resistor("r2", "out", "0", 1.0e3);
        public Circuit ckt;
        public DC dcSweep;
        // Start is called before the first frame update
        void Start()
        {
            outText.text = "Sim Output:";
            printComps();
            ckt = new Circuit(v1, r1, r2);



            dcSweep = new DC("SIM 1", "V1", 0.0, 10.0, 2.0);
            dcSweep.ExportSimulationData += (sender, args) =>
            {
                var input = args.GetVoltage("in");
                var output = args.GetVoltage("out");
                outText.text += "\nVin: {0}   |   Vout: {1}".FormatString(input, output);
            };
        }


        public void printComps()
        {

            compText.text = "Components:";
            compText.text += "\nVoltage Source: " + v1.Parameters.DcValue.Value;

            compText.text += "\nResistor 1: " + r1.Parameters.Resistance.Value;
            compText.text += "\nResistor 2: " + r2.Parameters.Resistance.Value;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }