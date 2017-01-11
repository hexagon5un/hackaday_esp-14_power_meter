include timer.fs

nvm
: chan.a!   2 adc! ;
: chan.b!   3 adc! ;

\ timer ticks are approx 5 ms
\ : tick tim begin dup tim 31 and = until ;

\ : amax ( times  -- max ADC ) 0 swap 0 do chan.a adc  max loop ;
\ : amin ( times  -- min ADC ) 1000 swap 0 do chan.a adc  min loop ;
\ : arange ( times -- range ) dup amax swap amin - ;
\ : bmax ( times  -- max ADC ) 0 swap 0 do chan.b adc  max loop ;
\ : bmin ( times  -- min ADC ) 1000 swap 0 do chan.b adc  min loop ;
\ : brange ( times -- range ) dup bmax swap bmin - ;

\ seems to be working, needs delay -- reading too fast.  was using dup and print, which works, but wastes juice

\ : f pa3.on 500 10 0 do ewma loop . pa3.off ;
\ : tt pa3.on adc@ 15 0 do adc@ + loop 16 / pa3.off ; 
\ : 3ewma ewma dup ewma dup ewma ;
\ : 3emit rot . swap . dup . cr ;
\ : foo adc@ begin pa3.toggle 3ewma pa3.toggle 3emit ?key until ;

\ what if don't set the channel?
\ : minmax adc@ min swap adc@ max swap ;
\ : ff pa3.on 0 1024 1200 0 do minmax loop pa3.off - . ;

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




