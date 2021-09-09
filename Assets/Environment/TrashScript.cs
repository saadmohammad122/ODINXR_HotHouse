using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Circuits
{
    public class TrashScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<Properties>().Type == "Resistor")
            {
                DeleteObject(other.gameObject);
            }
        }

        private void DeleteObject(GameObject gameObject)
        {
            Destroy(gameObject);
        }
    }
}
