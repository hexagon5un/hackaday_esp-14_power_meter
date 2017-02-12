\ CRC accumulator
: crc+ ( running_sum new_byte -- ) 
	xor
	dup 8 rshift swap 8 lshift or
	dup $ff00 and 4 lshift xor
	dup 8 rshift 4 rshift xor 
	dup $ff00 and 5 rshift xor
;

\ Puts the low byte and then the high byte on the stack
: split8 dup $00ff and swap $ff00 and 8 rshift ; 

\ The problem here is that the bytes are on the stack in the order
\  that they need to be CRC'ed and sent, rather than reversed.
\ The brute-force solution is to push them all into the return stack
\  and then process them one at a time "in reverse".
\ I'm sure there's a better way to do this...
: crc  
        \ move each element of stack to return stack, keeping count
        0 begin 1+ swap >r depth 1  = until 
	0 swap 
	\ pull back off return stack, keep count, add to CRC 
	begin 1 - swap r> tuck  crc+ rot dup 0= until drop 
	\ append the two CRC bytes
	split8
;

