using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circuits
{
    public class Multimeter : MonoBehaviour
    {
        // Start is called before the first frame update
        public string PositiveNodeLocation;
        public string NegativeNodeLocation;


        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GetDCVoltage()
        {
            // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
            /*var dc = new DC("DC1", "V1", 0, 12.0, 2);
            // Catch exported data
            dc.ExportSimulationData += (sender, args) =>
            {
                var input = args.GetVoltage(PositiveNodeLocation);
                var output = args.GetVoltage(NegativeNodeLocation);
                
            };

            dc.Run(mainCircuit);
            */
        }

        private void PrintVoltage(float Voltage)
        {
            print("This is the voltage between " + PositiveNodeLocation + " and " +
                NegativeNodeLocation + " :  ");
            print(Voltage.ToString("0.00") + " Volts");

        }

        
    }
}
