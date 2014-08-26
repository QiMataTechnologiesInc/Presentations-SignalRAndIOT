#ifndef _DHT22_H_
#define _DHT22_H_

#include <inttypes.h>



#define THERM_PIN PINC
#define THERM_DDR DDRC
#define THERM_PORT PORTC

#define THERM_DQ PC0
/* Utils */
#define THERM_INPUT_MODE() THERM_DDR&=~(1<<THERM_DQ)
#define THERM_OUTPUT_MODE() THERM_DDR|=(1<<THERM_DQ)
#define THERM_LOW() THERM_PORT&=~(1<<THERM_DQ)
#define THERM_HIGH() THERM_PORT|=(1<<THERM_DQ)
#define THERM_READ() ((THERM_PIN&(1<<THERM_DQ))? 1 : 0)

typedef enum
{
  DHT_ERROR_NONE = 0,
  DHT_BUS_HUNG,
  DHT_ERROR_NOT_PRESENT,
  DHT_ERROR_ACK_TOO_LONG,
  DHT_ERROR_SYNC_TIMEOUT,
  DHT_ERROR_DATA_TIMEOUT,
  DHT_ERROR_CHECKSUM,
  DHT_ERROR_TOOQUICK
} DHT22_ERROR_t;


    int dht22_read(float *temperature, float *humidity);



#endif /*_DHT22_H_*/
