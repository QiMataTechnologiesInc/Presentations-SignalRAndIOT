/*
 * CFile1.c
 *
 * Created: 8/20/2014 8:43:16 PM
 *  Author: Jenner
 */ 
#include <avr/io.h>
#include "uart.h"

void USART_Init( unsigned int ubrr)
{
	//	Set baud rate
	UBRR0H = (unsigned char)(ubrr>>8);
	UBRR0L = (unsigned char)ubrr;
	//	Enable receiver and transmitter
	UCSR0B = (1<<RXEN0)|(1<<TXEN0);
	//	Set frame format: 8data, 2stop bit
	UCSR0C = (1<<USBS0)|(3<<UCSZ00);
}

void USART_Transmit( const char* data )
{
	for (int i = 0; i < strlen(data); i++)
	{
		while ( !( UCSR0A & (1<<UDRE0)) )
		;
		//	Put data into buffer, sends the data
		UDR0 = data[i];
	}
	//	Wait for empty transmit buffer
	

}