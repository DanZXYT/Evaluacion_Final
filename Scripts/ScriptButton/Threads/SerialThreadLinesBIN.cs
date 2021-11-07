using UnityEngine;

using System.IO.Ports;
using System.Text;
using System;

/**
 * This class contains methods that must be run from inside a thread and others
 * that must be invoked from Unity. Both types of methods are clearly marked in
 * the code, although you, the final user of this library, don't need to even
 * open this file unless you are introducing incompatibilities for upcoming
 * versions.
 * 
 * For method comments, refer to the base class.
 */
public class SerialThreadLinesBIN : AbstractSerialThreadBIN
{

    public SerialThreadLinesBIN(string portName,
                             int baudRate,
                             int delayBeforeReconnecting,
                             int maxUnreadMessages)
        : base(portName, baudRate, delayBeforeReconnecting, maxUnreadMessages, true)
    {
    }

    protected override void SendToWire(object message, SerialPort serialPort)
    {
        
        serialPort.Write(message.ToString());
    }

    protected override object ReadFromWire(SerialPort serialPort)
    {
        
        byte[] byt = new byte[2];
        string message = null;

        if (serialPort.BytesToRead==2)
        {
            serialPort.Read(byt, 0, 2);

            byte crc = ChecksumCRC8(byt);
            if (crc == 0)
            {
                int size = byt.Length;
                byt[size - 1] = 0x0;
                message = Encoding.ASCII.GetString(byt, 0, byt.Length);
            }
        }

        return message;

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
