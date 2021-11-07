using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ReadASCII : MonoBehaviour
{

    public Text Input;
    public Text PleaseHolder;

    public Text UltimaTramaRecibida;
    private byte[] ASCII = new byte[10] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
    public string ASCII_b;
    public Text UltimaTRHolder;

    public OpenASCII serial;

    void Start()
    {
        serial = GameObject.Find("SerialControllerASCII").GetComponent<OpenASCII>();
    }

    public void Read_boton()
    {
        string message = serial.ReadSerialMessage();
        
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
                    Input.text = "Estado = "+message;
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
         
    }
}
