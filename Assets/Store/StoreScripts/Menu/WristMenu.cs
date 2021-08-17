using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WristMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> Components;
    public List<GameObject> SpawnedComponents;
    private int ComponentIndex;
    private int ArraySize;

    private TextMeshProUGUI ComponentName;
    private Transform ComponentContainer;
    private GameObject CurrentComponent;

    private void Awake()
    {
        ComponentIndex = 0;
        ComponentName = transform.Find("ComponentDescription").Find("Name").GetComponent<TextMeshProUGUI>();
        if (ComponentName == null)
            Debug.Log("Could not find Component Name");

        ComponentContainer = transform.Find("ComponentContainer");
        if (ComponentContainer == null)
            Debug.Log("Could not find ComponentContainer");

        ArraySize = GetComponentCount();
        
        // Initialize the SpawnedComponents array with the capacity of the normal array
        for (int i = 0; i < ArraySize; i++)
            SpawnedComponents.Add(null);
    }

    public GameObject DisplayComponent(Transform Parent)
    {
        GameObject Component = null;

        // Check to see if we have any components inserted
        if (Components.Count > 0)
        {
            if (SpawnedComponents[ComponentIndex] != null)
            {
                Component = SetComponentActive();
            }
            else
            {
                Component = DisplayObject(Parent);
            }

            CurrentComponent = Component;

            // Change text to display proper component name
            DisplayName(Component);
        }
        else
        {
            Debug.Log("Error: No components inserted into the Component List");
        }

        return Component;
    }

    private GameObject DisplayObject(Transform Parent)
    {
        GameObject Component;

        // Place component in correct position
        Component = Instantiate(Components[ComponentIndex], Parent);
        Vector3 position = new Vector3(0, 0, -7.925f);
        Vector3 rotation = new Vector3(-90, 90, 90);
        Component.transform.localPosition = position;
        Component.transform.localEulerAngles = rotation;

        // Allow component to float over hand and not fall or move when hitting other objects
        Component.GetComponent<Rigidbody>().useGravity = false;
        Component.GetComponent<Rigidbody>().isKinematic = true;

        // Set the boolean to notify that it's in the store
        ObjectController controller = Component.GetComponent<ObjectController>();
        if (controller != null)
        {
            controller.SetStoreValue();
        }

        return Component;
    }

    private void DisplayName(GameObject Component)
    {
        Properties properties = GetProperties(Component);
        string type = properties.Type;
        float value = properties.Value;
        string units = GetUnits(type);
        ComponentName.text = value.ToString() + " " + units.ToString() + " " + type.ToString();
    }

    private Properties GetProperties(GameObject Component)
    {
        Properties properties = Component.GetComponent<Properties>();
        if (properties == null)
            Debug.Log("Component does not have Properties Script - WristMenu line 97.");

        return properties;
    }

    private string GetUnits(string type)
    {
        string units = "";

        if (type == "Resistor")
            units = "Ohms";
        else if (type == "Capacitor")
            units = "F";

        return units;
    }

    public void NextComponent()
    {
        if (ComponentIndex < ArraySize - 1)
        {
            // Deactivate the current component and place in array to access later
            SpawnedComponents[ComponentIndex] = CurrentComponent;
            CurrentComponent.SetActive(false);

            // Make the new component appear
            ComponentIndex++;
            DisplayComponent(ComponentContainer);
        }
    }

    public void PrevComponent()
    {
        if (ComponentIndex > 0)
        {
            // Deactive the current component and place in array to access later
            SpawnedComponents[ComponentIndex] = CurrentComponent;
            CurrentComponent.SetActive(false);

            // Make the new component appear
            ComponentIndex--;
            DisplayComponent(ComponentContainer);
        }
    }

    /* Tells how many components are actually in the list.
       Potential issue of null spaces in list, we shouldn't run into
        this is we're careful but this method should cover for that by 
        condensing the list.
       Logic for this taken from the whiteboard slides logic. */
    private int GetComponentCount()
    {
        int count = 0;

        for (int i = 0; i < Components.Count; i++)
        {
            // If we find a component, list is fine. Increment our count.
            if (Components[i] != null)
            {
                // If we find an empty space in our list, then we want to condense our list
                if (Components[count] == null)
                {
                    Components[count] = Components[i];
                    Components[i] = null;
                }
                count++;
            }
        }

        return count;
    }

    private GameObject SetComponentActive()
    {
        // Extract the previously instantiated component and set it active
        GameObject Component = SpawnedComponents[ComponentIndex];
        Component.SetActive(true);

        // Clear the component from the array
        SpawnedComponents[ComponentIndex] = null;

        return Component;
    }
}
