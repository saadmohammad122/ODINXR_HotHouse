using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FunctionGenerator : MonoBehaviour
{
    private bool onButton;
    private GameObject FunctionGeneratorUI;
    private TextMeshProUGUI InputText, InputUnits, DisplayVoltage, DisplayFrequency;
    private GameObject SinText, SqrText, RampText, PulseText;
    private string voltageType, inputType;
    private Slider slider;

    private void Awake()
    {
        onButton = false;

        FunctionGeneratorUI = transform.Find("UI").gameObject;
        if (FunctionGeneratorUI == null)
            Debug.Log("FunctionGenerator.cs - ln 21. Could not find FunctionGeneratorUI");
        FunctionGeneratorUI.SetActive(false);

        slider = transform.Find("UI").Find("LeftPanel").Find("Panel").Find("NumpadPanel").Find("Panel").Find("Slider").GetComponent<Slider>();

        // Obtain the text for the numpad
        InputText = transform.Find("UI").Find("LeftPanel").Find("Panel").Find("NumpadPanel").Find("Panel").Find("Display").Find("Panel").Find("NumberText").GetComponent<TextMeshProUGUI>();
        if (InputText == null)
            Debug.Log("FunctionGenerator.cs - ln 27. Could not find VoltageText");

        // We're going to be switching the input's units from V to Hz, so obtain the text
        InputUnits = transform.Find("UI").Find("LeftPanel").Find("Panel").Find("NumpadPanel").Find("Panel").Find("Display").Find("Panel").Find("UnitsText").GetComponent<TextMeshProUGUI>();
        if (InputUnits == null)
            Debug.Log("FunctionGenerator.cs - ln 35. Could not find InputUnits");

        // Obtain the text being displayed on the voltage source
        DisplayVoltage = transform.Find("DisplayText").Find("VoltageText").GetComponent<TextMeshProUGUI>();
        if (DisplayVoltage == null)
            Debug.Log("FunctionGenerator.cs - ln 38. Could not find DisplayText");

        DisplayFrequency = transform.Find("DisplayText").Find("FreqText").GetComponent<TextMeshProUGUI>();
        if (DisplayFrequency == null)
            Debug.Log("FunctionGenerator.cs - ln 42. Could not find DisplayFrequency");

        // Set the default voltage type to DC
        voltageType = "DC";

        // Obtain the Sin, Cos, and Tan text available in the function generator display and deactivate all of them
        SinText = transform.Find("DisplayText").Find("SinText").gameObject;
        SqrText = transform.Find("DisplayText").Find("SquareText").gameObject;
        RampText = transform.Find("DisplayText").Find("RampText").gameObject;
        PulseText = transform.Find("DisplayText").Find("PulseText").gameObject;

        if (SinText == null)
            Debug.Log("FunctionGenerator.cs - ln 42. Could not find SinText");
        if (SqrText == null)
            Debug.Log("FunctionGenerator.cs - ln 44. Could not find SqrText");
        if (RampText == null)
            Debug.Log("FunctionGenerator.cs - ln 46. Could not find RampText");
        if (PulseText == null)
            Debug.Log("FunctionGenerator.cs - ln 48. Could not find PulseText");

        ClearACTypes();

        // Set the starting voltage to "DC"
        voltageType = "DC";

        // Can switch the numpad between frequency and voltage, start by setting it to voltage
        NumpadVolts();
    }


    public void ButtonPress()
    {
        // Reverse whatever the onButton boolean was and turn on/off the UI accordingly
        onButton = !onButton;
        if (onButton)
            FunctionGeneratorUI.SetActive(true);
        else
            FunctionGeneratorUI.SetActive(false);
    }

    public void UpdateNumpad(int num)
    {
        int min, max;
        // Get the range that the numbers are allowed to go into
        if (inputType == "Volts")
        {
            min = -100;
            max = 100;
        }
        else
        {
            min = -250;
            max = 250;
        }

        bool success = int.TryParse(InputText.text.ToString(), out int currentValue);
        if (success && currentValue < max && currentValue > min)
        {
            // Check if we are switching signs of the number
            if (num == -1)
            {
                currentValue *= -1;
            }
            // Move the number over a spot and insert the appropriate number
            else
            {
                currentValue = (currentValue * 10) + num;
            }
            InputText.text = currentValue.ToString();
            UpdateSlider();
        }
    }

    public void SubmitNumpad()
    {
        // Make sure we're changing the correct value on the function generator
        if (inputType == "Volts")
            DisplayVoltage.text = InputText.text;
        else
            DisplayFrequency.text = InputText.text;

        InputText.text = "0";
    }

    public void ClearNumpad()
    {
        InputText.text = "0";
    }

    private void ClearACTypes()
    {
        SinText.SetActive(false);
        SqrText.SetActive(false);
        RampText.SetActive(false);
        PulseText.SetActive(false);
    }

    public string GetVoltageType()
    {
        return voltageType;
    }

    // For getting voltage and frequency, on error will return 1 larger than the max possible value
    public int GetVoltage()
    {
        bool success = int.TryParse(DisplayVoltage.text, out int voltage);
        if (success)
            return voltage;
        else
        {
            Debug.Log("FunctionGenerator.cs. GetVoltage() could not get the voltage");
            return 101;
        }
    }

    public int GetFreq()
    {
        bool success = int.TryParse(DisplayFrequency.text, out int frequency);
        if (success)
            return frequency;
        else
        {
            Debug.Log("FunctionGererator.cs. GetFreq() could not get the frequency");
            return 251;
        }

    }

    public void SetVoltageType(string type)
    {
        // Check the input type, make sure it's not already set
        // Set the voltageType string to properly represent
        if (type == "DC" && voltageType != "DC")
        {
            ClearACTypes();
        }
        else if (type == "Sine" && voltageType != "Sine")
        {
            ClearACTypes();
            SinText.SetActive(true);
            voltageType = "Sine";
        }
        else if (type == "Square" && voltageType != "Square")
        {
            ClearACTypes();
            SqrText.SetActive(true);
            voltageType = "Square";
        }
        else if (type == "Ramp" && voltageType != "Ramp")
        {
            ClearACTypes();
            RampText.SetActive(true);
            voltageType = "Ramp";
        }
        else if (type == "Pulse" && voltageType != "Pulse")
        {
            ClearACTypes();
            PulseText.SetActive(true);
            voltageType = "Pulse";
        }
    }

    public void UpdateSliderValue()
    {
        InputText.text = slider.value.ToString();
    }

    // When the numpad is pressed, we want to update the slider so that it is accurate
    private void UpdateSlider()
    {
        bool success = int.TryParse(InputText.text, out int value);
        if (success)
        {
            slider.value = value;
        }
    }

    // When switching between voltage and frequency input, set values to 0, change units, and change the max value range
    public void NumpadVolts()
    {
        inputType = "Volts";
        InputUnits.text = "V";
        slider.maxValue = 100;
        slider.minValue = -100;
        InputText.text = "0";
        UpdateSlider();
    }

    public void NumpadFreq()
    {
        inputType = "Hertz";
        InputUnits.text = "Hz";
        slider.maxValue = 250;
        slider.minValue = -250;
        InputText.text = "0";
        UpdateSlider();
    }
}
