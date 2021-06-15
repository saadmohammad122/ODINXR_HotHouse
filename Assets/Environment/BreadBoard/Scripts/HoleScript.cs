using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public string UniqueName;
    Collider ColliderBox; 
    // Start is called before the first frame update
    void Start()
    {
        UniqueName = this.name + transform.parent.name + transform.parent.parent.name + transform.parent.parent.parent.name;
        

        ColliderBox = GetComponent<BoxCollider>();
        ColliderBox.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
