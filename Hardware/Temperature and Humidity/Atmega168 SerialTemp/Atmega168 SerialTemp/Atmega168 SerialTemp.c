/*
 * Atmega168_SerialTemp.c
 *
 * Created: 8/18/2014 6:44:12 PM
 *  Author: Jenner
 */ 


#include <stdio.h>
#include <stdlib.h>
#include <avr/interrupt.h>
#include <string.h>

#include <avr/io.h>
#include "uart/uart.h"
#include <util/delay.h>
#define RELAY_PIN PB1
#define LED_PIN PB0


#include "dht/dht.h"

int main( void )
{
	USART_Init(MYUBRR);
	DDRB |= (1 << LED_PIN);
	DDRB |= (1 << RELAY_PIN);
	
		
	//char * helloWorld = "Hello World!\r\n";
	const char printbuff[100];

	#if DHT_FLOAT == 0
	int8_t temperature = 0;
	int8_t humidity = 0;
	#elif DHT_FLOAT == 1
	float temperature = 0;
	float humidity = 0;
	#endif
	
	while (1)
	{	
		int8_t result = dht_gettemperaturehumidity(&temperature, &humidity);
		if(result != -1 && result!= -2) {
				//Temperature in C
				#if DHT_FLOAT == 0
				itoa(temperature, printbuff, 10);
				#elif DHT_FLOAT == 1
				dtostrf(temperature, 3, 3, printbuff);
				#endif
				//Temperature in F
				#if DHT_FLOAT == 0
				itoa((((temperature*9)/5)+32), printbuff, 10);
				#elif DHT_FLOAT == 1
				dtostrf(temperature, 3, 3, printbuff);
				#endif
				USART_Transmit("Temperature,"); USART_Transmit(printbuff); USART_Transmit("\r\n");
				//Relative Humidity in %				
				#if DHT_FLOAT == 0
				itoa(humidity, printbuff, 10);
				#elif DHT_FLOAT == 1
				dtostrf(humidity, 3, 3, printbuff);
				#endif
				USART_Transmit("Humidity,"); USART_Transmit(printbuff);USART_Transmit("\r\n");
				if ((((temperature*9)/5)+32) > 90)
				{
					PORTB &= ~(1 << RELAY_PIN);
				}
				else
				{
					PORTB |= (1 << RELAY_PIN);
				}

		}	
		_delay_ms(100);
		PORTB ^= (1 << LED_PIN);
	}
}