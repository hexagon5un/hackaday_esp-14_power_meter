
: crc+ ( running_sum new_byte -- ) 
	xor
	dup 8 rshift swap 8 lshift or
	dup $ff00 and 4 lshift xor
	dup 8 rshift 4 rshift xor 
	dup $ff00 and 5 rshift xor
;

: split8 dup $00ff and swap $ff00 and 8 rshift ; 

\ load off to return stack to reverse
\ juggling last_value crc byte_count
: crc 0 begin 1+ swap >r depth 1  = until 
	0 swap 
	begin 1 - swap r> tuck  crc+ rot dup 0= until drop 
	split8
;

