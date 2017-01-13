#include <stdio.h>
#include <stdint.h>


uint16_t crc16Add(unsigned char b, uint16_t acc)
{

  printf("zero: %d, %d\n", acc, b);
  acc ^= b;
  printf("one: %d\n", acc);
  printf ("rshift: %d, lshift: %d\n", (acc >> 8), (acc << 8));
  acc = (acc >> 8) | (acc << 8);
  printf("two: %d\n", acc);
  acc ^= (acc & 0xff00) << 4;
  printf("three: %d,%d,%d,%d,%d \n", acc, acc>>8, (acc>>8)>>4, (acc>>12), acc^((acc>>8)>>4));
  acc ^= (acc >> 8) >> 4; // order of ops: shift 8, xor accum, shift 4?
  // or shift 8 shift 4 (= shift 12?) xor acc?

  printf("four: %d\n", acc);
  acc ^= (acc & 0xff00) >> 5;
  printf("five: %d\n", acc);
  return acc;
}


int main(void){

	uint8_t c = 0x42;
	uint16_t acc = 0;
	uint8_t data[]={1,0,0,0,0x89, 2,0,0};
	uint8_t i;
	for (i=0; i<8; i++){
		printf("-------- round: %d\n", i);
		acc = crc16Add(data[i], acc);
	}

  printf("----- end: %d\n", acc);


	return(0);

}


