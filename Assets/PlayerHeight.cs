using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    public GameObject VRRig;
    // Start is called before the first frame update
    public float height = .9f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (VRRig.transform.position.y != 1.8f)
        {
            VRRig.transform.position = new Vector3(VRRig.transform.position.x, height, VRRig.transform.position.z);
        }
    }
}
