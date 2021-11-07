using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.IO.Ports;

public class DropdownSerialPort1 : MonoBehaviour
{
    public static string PUERTO;
        
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        
        // listamos los puertos COM
        string[] ports = SerialPort.GetPortNames();
        foreach(string COM in ports)
        {
            items.Add(COM);
        }

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate{ DropdownItemSelected(dropdown);});
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        PUERTO = dropdown.options[index].text;
    }

    public string PuertoSelect()
    {
        return PUERTO;
    }




}
