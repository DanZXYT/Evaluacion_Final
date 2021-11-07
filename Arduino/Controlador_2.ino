#define sensorD2 8
#define actuadorD2 7

byte Data[10];
int sizebyte_total = 10;
String Datos;
String R;
byte crc;
byte RDatos[2];
boolean Estado = false;

long t=0;
String Password = "1a2b3c";

#include <SoftwareSerial.h>

SoftwareSerial mySerial(10, 11); // RX, TX


void setup() 
{
  mySerial.begin(9600);
  Serial.begin(9600);
  pinMode(INPUT,sensorD2);
  pinMode(OUTPUT,actuadorD2);
  
}

void B_Buff()
{
  for(int i = 0;i<sizebyte_total;i++)
  {
    Data[i]=0x0;
  }
}

void loop() 
{
  
  while(Serial.available()>0)
  {
    B_Buff();
    Serial.readBytes(Data,7);
    for(int i=0;i<7;i++)
    {
      mySerial.println(Data[i],BIN);  
    }
    
    
    
    crc=uiCrc8Cal(Data,7);
    mySerial.println(crc);
    
    
    if(crc == 0)
    {
      Data[7-1]=0x0;
      Datos = String((char *)(Data));
      mySerial.println(Datos);

      if(digitalRead(sensorD2) && Datos.equals(Password))
      {
        Estado=!Estado;
        digitalWrite(actuadorD2,Estado);
      }
    }
    
  }
  if(Estado && millis() > t + 3000)
  {
    R = digitalRead(sensorD2);
    R.getBytes(RDatos,2);
    crc=uiCrc8Cal(RDatos,1);
    RDatos[1]=crc;
    mySerial.println("Estado del Sensor = "+String(R));
    mySerial.println("Estado XX = "+String(RDatos[0])+" "+String(RDatos[1]));
    
    
    Serial.write(RDatos,2);
    t=millis();
  }
}


unsigned int uiCrc8Cal(byte pucY[], byte ucX)
{
  const uint16_t PRESET_VALUE = 0xFF;
  const uint16_t POLYNOMIAL = 0x8C;
  byte ucI, ucJ;
  unsigned short int uiCrcValue = PRESET_VALUE;
  for (ucI = 0; ucI < ucX; ucI++)
  {
    uiCrcValue = uiCrcValue ^ pucY[ucI];
    for (ucJ = 0; ucJ < 8; ucJ++)
    {
      if ((uiCrcValue & 0x0001) != 0)
      {
        uiCrcValue = (uiCrcValue >> 1) ^ POLYNOMIAL;
      }
      else
      {
        uiCrcValue = (uiCrcValue >> 1);
      }
    }
  }
  return uiCrcValue;
}
