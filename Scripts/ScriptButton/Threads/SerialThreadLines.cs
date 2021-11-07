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
public class SerialThreadLines : AbstractSerialThread
{

    public SerialThreadLines(string portName,
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

        string read = serialPort.ReadLine();
        return read;
   
    }
}
