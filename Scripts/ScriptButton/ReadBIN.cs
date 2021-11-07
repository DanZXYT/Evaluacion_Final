using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ReadBIN : MonoBehaviour
{

    public Text Input;
    public Text PleaseHolder;

    public Text UltimaTramaRecibida;
    
    public string BIN_b;
    public Text UltimaTRHolder;

    public OpenBinario serial;

    void Start()
    {
        serial = GameObject.Find("SerialControllerBinario").GetComponent<OpenBinario>();
    }

    public void Read_boton()
    {
        
        string message = serial.ReadSerialMessage();
        Debug.Log("________________ llego = "+message);
        if (message != null)
        {
            if (ReferenceEquals(message, "__Connected__"))
            {
                Debug.Log("Connection established");
            }
            else
            {
                if (ReferenceEquals(message, "__Disconnected__"))
                {
                    Debug.Log("Connection attempt failed or disconnection detected");
                }
                else
                {

                    PleaseHolder.text = "";
                    Input.text = "Estado = " + message;

                    UltimaTRHolder.text = "";

                    BIN_b = "";
                    BIN_b = Convert.ToString( Encoding.ASCII.GetBytes(message)[0] , 2);
                    UltimaTramaRecibida.text = BIN_b;
                    Debug.Log("Message arrived: " + message);


                }
            }
        }

    }


    


}
