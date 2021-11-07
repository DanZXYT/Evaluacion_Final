using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class WriteBIN : MonoBehaviour
{

    public Text Output;

    public Text UltimaTramaEnviada;
    private byte[] BIN = new byte[10] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
    byte crc;

    public string BIN_b;
    public Text UltimaTEHolder;

    public string texto;
    public OpenBinario serial;
    int size;

    void Start()
    {
        serial = GameObject.Find("SerialControllerBinario").GetComponent<OpenBinario>();
    }

    public void Write_boton()
    {
        texto = Output.text;
        Debug.Log(texto);
        UltimaTEHolder.text = "";
        BIN = Encoding.ASCII.GetBytes(texto);
        
        crc = ChecksumCRC8(BIN);
        Debug.Log("crc = "+crc);
        size = BIN.Length;
        Debug.Log("size = "+size);
        Array.Resize(ref BIN, size+1);
        BIN[size] = crc;
        
        foreach(byte b in BIN)
        { 
            Debug.Log(b);
        }

        Debug.Log("BIN = "+Encoding.ASCII.GetString(BIN, 0, BIN.Length));

        serial.SendSerialMessage(Encoding.ASCII.GetString(BIN,0,BIN.Length));


        BIN_b = "";
        foreach (byte b in BIN)
        {
            BIN_b += Convert.ToString(b,2);
        }
        Debug.Log(BIN_b);
        UltimaTramaEnviada.text = BIN_b;
    }


    static byte ChecksumCRC8(byte[] pucY)
    {
        byte preset_value = 0xFF; 
        byte Polinomio = 0x8C;    

        byte uiCrcValue = preset_value; 


        for (byte ucI = 0; ucI < pucY.Length; ucI++)
        {
            uiCrcValue = (byte)(uiCrcValue ^ pucY[ucI]); 
            for (byte ucJ = 0; ucJ < 8; ucJ++)
            {
                if ((uiCrcValue & 0x0001) != 0) 
                {
                    
                    uiCrcValue = (byte)((uiCrcValue >> 1) ^ Polinomio);
                }
                else
                {
                    
                    uiCrcValue = (byte)(uiCrcValue >> 1);
                }
            }
        }

        return uiCrcValue;
    }
}
