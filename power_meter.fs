
: round.div ( numerator divisor -- product ) dup 2/ rot + swap / ; 
variable ewma_factor 
16 ewma_factor !
: factor-1 ewma_factor @ 1- ;
: factor/ ewma_factor @ round.div ;
: ewma ( sum -- adc + sum ) factor-1 * adc@ + factor/ ;
: minmax dup ewma min swap dup ewma max swap ;

variable avg
: 5ms tim 1+ begin dup tim = until drop ;
: average 0 10 0 do adc@ + 5ms loop 10 / ;
: set.average average avg ! ;
: one.cycle avg @ dup 9 set.timer begin minmax timer? until - ;
\ pick which ADC, initialize
: washer   2 adc! set.average ;
: dryer   3 adc! set.average ;

variable threshold 
7 threshold !
: is.on? ( -- boolean ) one.cycle threshold @ < not ;

\ and there you have it.  
\ switch channels (chan.a! chan.b!) and then is.on?
: rr.single set.average is.on? if pa3.on else pa3.off set.average then ;
: rr  begin rr.single ?key until ;

\ default -- have to pick a channel
\ washer is.on? .






