#define trigPin 2
#define echoPin 3
#define REDled 11
#define YELled 10
#define GRNled 9

void setup() {
  Serial1.begin (9600);
  Serial.begin (9600);
//  while (Serial1);
  pinMode(trigPin, OUTPUT);
  pinMode(echoPin, INPUT);
  pinMode(REDled, OUTPUT);
  pinMode(YELled, OUTPUT);
  pinMode(GRNled, OUTPUT);
  
}

void loop() {
  long duration, distance;
  digitalWrite(trigPin, LOW);  // Added this line
  delayMicroseconds(2); // Added this line
  digitalWrite(trigPin, HIGH);
//  delayMicroseconds(1000); - Removed this line
  delayMicroseconds(10); // Added this line
  digitalWrite(trigPin, LOW);
  duration = pulseIn(echoPin, HIGH);
//  delayMicroseconds(20);
  distance = (duration/2) / 29.1;
 /*if (distance < 50 & distance > 20) {  // This is where the LED On/Off happens
    digitalWrite(REDled,LOW); // 
    digitalWrite(YELled,LOW);
    digitalWrite(GRNled,HIGH);
}
 else if (distance < 20 & distance > 10) {  // This is where the LED On/Off happens
    digitalWrite(REDled,LOW); // 
    digitalWrite(YELled,HIGH);
    digitalWrite(GRNled,LOW);
}
 else if (distance < 10) {  // This is where the LED On/Off happens
    digitalWrite(REDled,HIGH); // 
    digitalWrite(YELled,LOW);
    digitalWrite(GRNled,LOW);
}
  
  else {
    digitalWrite(REDled,LOW);
    digitalWrite(YELled,LOW);
    digitalWrite(GRNled,LOW);
  }*/
  if (distance >= 200 || distance <= 0){
    Serial1.println("Out of range");
    Serial.println("Out of range");
  }
  else {
    Serial1.print("Distance,");
    Serial1.println(distance);
  }
  delay(500);
}
