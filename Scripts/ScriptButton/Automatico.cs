using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Automatico : MonoBehaviour
{

    public Text Input;
    public Text PleaseHolder;

    public Text UltimaTramaRecibida;
    private byte[] ASCII = new byte[10] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
    public string ASCII_b;
    public Text UltimaTRHolder;

    public OpenASCII serial;
    
    bool ModoAutomatico = false;
    static public Button Button;
    public Text BotonText;


    public OpenBinario serialB;
    string BIN_b;
    public Text InputB;
    public Text PleaseHolderB;

    public Text UltimaTramaRecibidaB;

    public Text UltimaTRHolderB;


    void Start()
    {
        serial = GameObject.Find("SerialControllerASCII").GetComponent<OpenASCII>();
        serialB = GameObject.Find("SerialControllerBinario").GetComponent<OpenBinario>();
    }

    public void ModoBoton()
    {
        

        ModoAutomatico = !ModoAutomatico;
        if(ModoAutomatico)
        {
            BotonText.text = "MODO AUTOMATICO ACTIVADO";
        }
        else
        {
            BotonText.text = "MODO AUTOMATICO DESACTIVADO";
        }
    }



    // Update is called once per frame
    void Update()
    {
        string message = null;

        string messageB = null;


        if (ModoAutomatico)
        {
            message = serial.ReadSerialMessage();
            messageB = serialB.ReadSerialMessage();
        }
        

        if (message != null )
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
                    ASCII = Encoding.ASCII.GetBytes(message);
                    ASCII_b = "";
                    foreach (byte b in ASCII)
                    {
                        ASCII_b += b.ToString();
                        ASCII_b += " ";
                    }
                    UltimaTramaRecibida.text = ASCII_b;
                    Debug.Log("Message arrived: " + message);

                }
            }
        }


        if (messageB != null)
        {
            if (ReferenceEquals(messageB, "__Connected__"))
            {
                Debug.Log("Connection established");
            }
            else
            {
                if (ReferenceEquals(messageB, "__Disconnected__"))
                {
                    Debug.Log("Connection attempt failed or disconnection detected");
                }
                else
                {

                    PleaseHolderB.text = "";
                    InputB.text = "Estado = " + messageB;

                    UltimaTRHolderB.text = "";

                    BIN_b = "";
                    BIN_b = Convert.ToString(Encoding.ASCII.GetBytes(messageB)[0], 2);
                    UltimaTramaRecibidaB.text = BIN_b;
                    Debug.Log("Message arrived: " + messageB);


                }
            }
        }



    }
}
