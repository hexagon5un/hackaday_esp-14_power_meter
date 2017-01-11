include timer.fs

nvm
: chan.a!   2 adc! ;
: chan.b!   3 adc! ;

: 5ms tim 1+ begin dup tim = until drop ;
: average 0 10 0 do adc@ + 5ms loop 10 /  ;
ram

: round.div ( numerator divisor -- product ) dup 2/ rot + swap / ; 
variable ewma_factor 
: factor-1 ewma_factor @ 1- ;
: factor/ ewma_factor @ round.div ;
: ewma ( sum -- adc + sum ) factor-1 * adc@ + factor/ ;
: minmax dup ewma min swap dup ewma max swap ;

variable avg
: one.cycle avg @ dup 5 set.timer begin minmax timer? until - ;

variable threshold 
: is.on? ( -- boolean ) one.cycle threshold @ < not ;

\ and there you have it.  
\ switch channels (chan.a! chan.b!) and then is.on?
: rr.single is.on? if pa3.on else pa3.off average avg ! then ;
: rr  begin rr.single ?key until ;

chan.a!  

16 ewma_factor !
7 threshold !
average avg !




