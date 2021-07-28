using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Properties : MonoBehaviour
{

    
    public int Value;
    public int RandomInt;
    public string Type;
    public string UniqueName;
    public int numberOfInput;
    // Start is called before the first frame update
    void Start()
    {
        NameGenerator();
    }

    public void NameGenerator()
    {
        System.Random numberGenerator = new System.Random();
        var CircuitScript = this.GetComponentInParent<CircuitCreator>();
        UniqueName = this.name + CircuitScript.numOfComponents + numberGenerator.Next(1, 1000).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
