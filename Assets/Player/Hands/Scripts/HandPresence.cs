using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{


    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    private GameObject spawnedHandModel;
    public GameObject handModelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        List<InputDevice> devices = new List<InputDevice>();    // List of VR devices connected to you
        InputDevices.GetDevices(devices);
        foreach (var item in devices)
        {
            print(item.name + item.characteristics);
        }
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right;   
        // the device controllers of the right controller are stored above
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)      // if multiple VR devices, select the first one
        {
            targetDevice = devices[0];
        

        }
        spawnedHandModel = Instantiate(handModelPrefab, transform);




    }

    // Update is called once per frame
    void Update()
    {
        // Take the target device and find the value from the primary Button
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

        if (primaryButtonValue) // this is a boolean, either the button is 0 (not pressed) or 1 (pressed)
        {
            Debug.Log("Pressing Primary Button");
        }

        targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue); // get the trigger float value
        if (triggerValue > 0.1f)
        {
            Debug.Log("Trigger pressed " + triggerValue);

        }
        targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary TouchPad " + primary2DAxisValue);



    }
}
