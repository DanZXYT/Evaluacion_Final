using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownSpeed2 : MonoBehaviour
{

    public static string SPEED;
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();

        dropdown.options.Clear();

        List<string> items = new List<string>();
        items.Add("300");
        items.Add("1200");
        items.Add("2400");
        items.Add("4800");
        items.Add("9600");
        items.Add("19200");
        items.Add("38400");
        items.Add("57600");
        items.Add("74880");
        items.Add("115200");
        items.Add("230400");
        items.Add("250000");
        items.Add("500000");
        items.Add("1000000");
        items.Add("2000000");

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownItemSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;

        SPEED = dropdown.options[index].text;
    }

    public string SpeedSelect()
    {
        return SPEED;
    }
}
