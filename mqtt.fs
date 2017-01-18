\ needs crc.fs

\ needs refactor to remove leading, trailing c0, checksum
\ : emits ( depth -- ) 1 + dup 0 do dup i - 2 * sp@ + @ emit loop drop ;  

: slip $c0 emit ;
\ : cleanup ( depth -- ) 0 do drop loop ;

\ emits whole stack
: emits  0 begin 1+ swap >r depth 1  = until 
	begin 1 - r> emit dup 0= until drop 
;

\ appends crc, transmits as slip-escaped
: send crc slip emits slip ;

