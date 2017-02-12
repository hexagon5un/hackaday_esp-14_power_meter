\ requires timer.fs
\ requires mqtt.fs
\ requires messages.fs

\ pick which ADC channel, initialize
: washer   2 adc! ;
: dryer    3 adc! ;

\ max, min of smoothed values 
\ some rounding error here.  Meh.
: ewma ( sum -- adc + sum ) 15 * adc@ + 16 round.div ;
: minmax dup ewma min swap dup ewma max swap ;

\ measure average value, then use as baseline for max/min 
: 5ms 1 blocking.wait ;
: average 0 20 0 do adc@ + 5ms loop 20 / ;

\ 9*5 ms = two 20ms 50 Hz cycles (?) plus a bit
: one.cycle average dup 9 set.timer 
        begin minmax timer? until - ;

\ Sends off the max/min difference for washer and dryer
\ This is what you want to call periodically
: report 
	sync sync 
	mqtt.washer washer one.cycle value 
	mqtt.dryer dryer one.cycle value 
	;



