
\ 16 MHz clock / value : 0xABCD -> BC, AD
\ Must be written high byte first.  Don't ask.  In STM8 datasheet.
: 57600_baud  $6  $5233 c! $11 $5232 c! ;
: 9600_baud   $03 $5233 c! $68 $5232 c! ;
: 115200_baud $0b $5233 c! $08 $5232 c! ;



