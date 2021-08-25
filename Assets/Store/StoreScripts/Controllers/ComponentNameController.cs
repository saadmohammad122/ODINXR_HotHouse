using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComponentNameController : MonoBehaviour
{
    private void Awake()
    {
        Transform Description = transform.Find("ComponentDescription");
        if (Description == null)
            Debug.Log("Could not find the Component Description Transform");

        Description.gameObject.SetActive(false);
    }

    public void DisplayComponentInfo(GameObject Component)
    {
        Properties properties = Component.GetComponent<Properties>();
        TextMeshProUGUI description = Component.transform.Find("ComponentDescription").Find("Container").Find("Text").GetComponent<TextMeshProUGUI>();
        Transform DescriptionGameObject = Component.transform.Find("ComponentDescription");
        if (properties != null && description != null)
        {
            string values = GetValues(properties);
            description.text = values;
            DescriptionGameObject.gameObject.SetActive(true);
        }
    }

    public void HideComponentInfo(GameObject Component)
    {
        TextMeshProUGUI description = Component.transform.Find("ComponentDescription").Find("Container").Find("Text").GetComponent<TextMeshProUGUI>();
        Transform DescriptionGameObject = Component.transform.Find("ComponentDescription");
        if (DescriptionGameObject != null)
        {
            description.text = " ";
            DescriptionGameObject.gameObject.SetActive(false);
        }
        else
            Debug.Log("Uh oh, in trouble");
    }

    private string GetValues(Properties properties)
    {
        string type = properties.Type;
        float value = properties.Value;
        string units = GetUnits(type);

        return value + " " + units + " " + type;
    }

    private string GetUnits(string type)
    {
        if (type == "Resistor")
            return "Ohms";
        else if (type == "Capacitor")
            return "F";
        else
            return ":(";
    }
}
