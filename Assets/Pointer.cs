using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Pointer : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer rend;
    Vector3[] points;
    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        } 
            rend = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckButton();
    }


    void CheckButton()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primeButton);
        if (primeButton)
        {
            Debug.Log("We hit the prime button");
            
        }
    }
}
