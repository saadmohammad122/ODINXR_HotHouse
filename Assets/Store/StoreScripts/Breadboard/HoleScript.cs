using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    public string UniqueName;
    public Collider ColliderBox;



    // Start is called before the first frame update
    void Start()
    {
        UniqueName = transform.parent.name + transform.parent.parent.name;


        //ColliderBox = GetComponent<BoxCollider>();
        //ColliderBox.isTrigger = false;

    }
}
