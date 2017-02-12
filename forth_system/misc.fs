\ Utility functions, debug and bitshift


: hex. dup hex . decimal ;
: tuck ( a b -- b a b ) swap over ;

\ hexdumps a page of memory
: dd ( addr -- ) $1ff dump ;

: lshift 0 do 2* loop ;
: rshift  0 do 2/ $7fff and loop ; 
\ 2/ shifts in 1's b/c twos-complement
\ $7fff and zeros out the MSB.

\ integer division with almost correct rounding
: round.div ( numerator divisor -- product ) dup 2/ rot + swap / ; 

