using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class WriteASCII : MonoBehaviour
{
    public Text Output;

    public Text UltimaTramaEnviada;
    private byte[] ASCII = new byte[10] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
    public string ASCII_b;
    public Text UltimaTEHolder;

    public string texto;
    public OpenASCII serial;


    void Start()
    {
        serial = GameObject.Find("SerialControllerASCII").GetComponent<OpenASCII>();
    }

    public void Write_boton()
    {
        texto = Output.text;
        Debug.Log(texto);
        serial.SendSerialMessage(texto);
        UltimaTEHolder.text = "";
        ASCII = Encoding.ASCII.GetBytes(texto);
        ASCII_b = "";
        foreach ( byte b in ASCII)
        {
            ASCII_b += b.ToString();
            ASCII_b += " ";
        }
        UltimaTramaEnviada.text = ASCII_b;
    }

    
}
