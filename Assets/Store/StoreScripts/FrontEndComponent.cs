using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    


    // Update is called once per frame
    void Update()
    {
        
    }
}
