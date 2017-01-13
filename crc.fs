
: lshift 0 do 2* loop ;
: rshift  0 do 2/ $7fff and loop ;

variable crc-sum
: crc.reset 0 crc-sum ! ;

\ can refactor variable away.  would be big win.
( crc-sum new-byte -- crc-sum ) 
: crc+ ( new byte -- ) 
	crc-sum @ xor
	dup 8 rshift swap 8 lshift or
	dup $ff00 and 4 lshift xor
	dup 8 rshift 4 rshift xor 
	dup $ff00 and 5 rshift xor
	crc-sum !
;

: hex. dup hex . decimal ;
: bin. dup bin . decimal ;

: crcs ( depth -- ) crc.reset dup  0 
	do dup 1 + i - 2 * sp@ + @ dup . crc+ loop drop crc-sum @ ;  

hex
: sync  01 00  00 00  89 02  00 00 ;
decimal

xx
sync depth crcs hex.


