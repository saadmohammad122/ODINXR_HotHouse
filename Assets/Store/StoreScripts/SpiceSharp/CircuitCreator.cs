using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;
using UnityEngine.UI;
using SpiceSharp.Validation;


public class CircuitCreator : MonoBehaviour
 {
        public int numOfComponents = 0;
        private int FrameCount = 0;
        //public Dictionary<string, Dictionary <string, string> > ListOfComponents = new Dictionary<string, Dictionary<string, string>>();

        // Gave ComponentScript the ability to create SpiceSharp Components with the breadboard positioning, and then hand that component to CircuitCreator
        //public Dictionary<string, SpiceSharp.Components.Component> ListOfComponents = new Dictionary<string, SpiceSharp.Components.Component>();
        public Circuit mainCircuit;
        public Camera fpsCam;
        public GameObject SweepButton;
        public Text test;
        public Circuit testCircuit;

    // Start is called before the first frame update
    void Start()
        {

            mainCircuit = new Circuit(new VoltageSource("V1", "In", "0", 1.0), (new Resistor("R1", "Out", "0", 0)));

            testCircuit = new Circuit(new VoltageSource("V1", "Row1", "0", 12.0), (new Resistor("Resistor046", "Row1", "Row4", 10000000)), (new Resistor("Ground", "Row4", "0", 0)));


        }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            print("'i' key was pressed");
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = fpsCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {

                    if (hit.collider.name == SweepButton.name)
                    {
                        // Create a DC simulation that sweeps V1 from -1V to 1V in steps of 100mV
                        var dc = new DC("DC1", "V1", -12.0, 12.0, 0.5);

                        // Catch exported data
                        dc.ExportSimulationData += (sender, args) =>
                        {
                            var input = args.GetVoltage("Row1"); 
                            var output = args.GetVoltage("Row4");
                            print(input - output);
                        };
                        
                        dc.Run(testCircuit);
                    }
                    new WaitForSecondsRealtime(10);
                }
            }
            
        }

        //public IEnumerable<IRuleViolation> Violations { get; }

        //private void UpdateSpiceSharp()
        //{


        // Spice sharp will have a List of components, that is made up of "Spicesharp.Components".
        //      ie: List: (Resistor r1, Resistor r2, VoltageSource v1, ...) 

        // Idea 1: 
        //  Two Dictionaries in Circuit Creator, one is updated by the front end components and the other is updated periodocially. 
        //      Initalize Dict1 and Dict2 to be equal. 
        //      Here, if Dict1 (updated by FEC) is changed, there is a period of time where Dict2 is not updated. 
        //      Find which index Dict2 is different than Dict1, and push that change to Spicesharp so that you don't have to update the whole list. 

        // Idea 2: 
        //  If there is any change to the circuit at all, clear the old circuit and create a new circuit for spice sharp to operate on. 



        //}

        /*private float Sweep(float Ckt)
        {
            var Ckt = new Circuit();
            var dc = new DC("DC 1", "V1", -1.0, 1.0, 0.2);

            // Create exports
            var inputExport = new RealVoltageExport(dc, "in");
            var outputExport = new RealVoltageExport(dc, "out");
            var currentExport = new RealPropertyExport(dc, "V1", "i");

            // Catch exported data
            dc.ExportSimulationData += (sender, args) =>
            {
                var input = inputExport.Value;
                var output = outputExport.Value;
                var current = currentExport.Value;
            };
            dc.Run(Ckt);
            return Ckt;
        }*/
    }
}

