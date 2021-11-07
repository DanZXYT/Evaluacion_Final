#define sensorD1 8
#define actuadorD1 7

#include <SoftwareSerial.h>

SoftwareSerial mySerial(10, 11); // RX, TX

boolean Estado = true;
String password = "6w7x8y9z";
String Dato;

long t;

void setup() 
{
  mySerial.begin(9600);
  Serial.begin(9600);
  pinMode(INPUT,sensorD1);
  pinMode(OUTPUT,actuadorD1);
  
}

void loop() 
{
  
  if(Serial.available())
  {
    Dato = Serial.readString();
    mySerial.println(Dato);
    mySerial.println(password);
    mySerial.println(String(Dato.equals(password))+" -- "+String(digitalRead(sensorD1)));
  }

  
  while(Dato.equals(password) && digitalRead(sensorD1))
  {
    if(millis() > t + 2000)
    {
      digitalWrite(actuadorD1,Estado);
      Serial.println(Estado);
      Estado=!Estado;
      
      t = millis();
    }
  }

  Dato = " ";

  
}
