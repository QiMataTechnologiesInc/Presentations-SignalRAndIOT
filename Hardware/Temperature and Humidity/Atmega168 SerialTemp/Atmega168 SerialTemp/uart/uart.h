/*
 * IncFile1.h
 *
 * Created: 8/20/2014 8:43:04 PM
 *  Author: Jenner
 */ 


#ifndef uart_H_
#define uart_H_

#define F_CPU 16000000UL // Clock Speed
//#define F_CPU 1000000UL // Clock Speed

#define FOSC F_CPU // Clock Speed
#define BAUD 9600
#define MYUBRR ((FOSC/16/BAUD)-1)


void USART_Init( unsigned int ubrr);
void USART_Transmit( const char* data );





#endif /* uart_H_ */
