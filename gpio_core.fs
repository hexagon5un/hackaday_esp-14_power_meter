

: set dup c@ rot or swap c! ; 
: toggle dup c@ rot xor swap c! ; 
: clear dup c@ rot not and swap c! ;
: bit dup if 1 swap 0 do 2 * loop else drop 1 then ;
\ pause, b/c nvm writes takes too long
: bin 2 base ! ;
: dec 10 base ! ;
: bin. bin . dec ;

: pa3 8 ;
: pa3.init pa3 dup pa_ddr set pa_cr1 set ;
: pa3.on pa3 pa_odr set ;
: pa3.toggle pa3 pa_odr toggle ;
: pa3.off pa3 pa_odr clear ;


\ : porta@ 
	\ cr ." DDR: " pa_ddr c@ bin. 
	\ cr ." ODR: " pa_odr c@ bin. 
	\ cr ." CR1: " pa_cr1 c@ bin. 
	\ cr ." CR2: " pa_cr2 c@ bin. 
	\ cr
	\ ;

