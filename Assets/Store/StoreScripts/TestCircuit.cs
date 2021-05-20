using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiceSharp.Components;
using SpiceSharp.Simulations;
using SpiceSharp;

public class TestCircuit : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera fpsCam;
    public GameObject goCube;
    public GameObject AddResistor;

    public int index = 3;
    public string InNode = "in";
    public string OutNode = "out";
    public string PreviousOutput = "NULL"; 


    class MyComponent 
    {
        
        public SpiceSharp.Components.Component resistor { get; set; }
    }



    public Dictionary<string, SpiceSharp.Components.Component> cktArray = new Dictionary<string, SpiceSharp.Components.Component>();



    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetMouseButtonDown(0))
    {
        var ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var cktScript = this.GetComponent<mycircuits>();
                if (hit.collider.name == goCube.name)
                {
                    cktScript.outText.text = "Output: ";
                    cktScript.dcSweep.Run(cktScript.ckt);
                }

                else if (hit.collider.name == AddResistor.name)
                {
                    print("Hello! button pressed!");  
                    PreviousOutput = "r" + (index - 1);
                    cktArray.Add("r" + index, new Resistor("r" + index, PreviousOutput, OutNode + index, 1.0e4));
                    index += 1;
                    print("This is the index:   " + index);
                    cktScript.printComps();
                }
            }
        }
    }
    }
}
