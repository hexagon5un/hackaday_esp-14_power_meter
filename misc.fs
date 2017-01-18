: hex. dup hex . decimal ;
: tuck swap over ;

: lshift 0 do 2* loop ;
: rshift  0 do 2/ $7fff and loop ; 

: round.div ( numerator divisor -- product ) dup 2/ rot + swap / ; 

