
: crc+ ( running_sum new_byte -- ) 
	xor
	dup 8 rshift swap 8 lshift or
	dup $ff00 and 4 lshift xor
	dup 8 rshift 4 rshift xor 
	dup $ff00 and 5 rshift xor
;

: split8 dup $00ff and swap $ff00 and 8 rshift ; 

: crc 0 begin 1+ swap >r depth 1  = until 
	0 swap 
	begin 1 - swap r> tuck  crc+ rot dup 0= until drop 
	split8
;

\ : crc 0 begin 1+ swap >r depth 1  = until \ push all on return stack
	\ 0 swap \ put running crc under the count 
	\ begin 1 - swap \ decrement count, swap under CRC
		\ r> tuck  \ save copy of value on stack
		\ crc+ rot \ before rot: count latest_val crc
		\ dup 0= 
	\ until drop 
	\ split8
	\ ;



