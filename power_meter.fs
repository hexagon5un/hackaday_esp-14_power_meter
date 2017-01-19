\ requires timer.fs
\ requires mqtt.fs
\ requires messages.fs

\ pick which ADC channel, initialize
: washer   2 adc! ;
: dryer    3 adc! ;

\ max, min of smoothed values 
: ewma ( sum -- adc + sum ) 15 * adc@ + 16 round.div ;
: minmax dup ewma min swap dup ewma max swap ;

\ measure average value, then measure max, min relative to it
: 5ms 1 blocking.wait ;
: average 0 20 0 do adc@ + 5ms loop 20 / ;


\ avg dup initializes min/max to midpoint
\ 9*5 ms = two 20ms 50 Hz cycles (?) plus a bit

: one.cycle average dup 9 set.timer 
        begin minmax timer? until - ;

\ Note: magic constant here -- 
\ depends on amount of smoothing in EWMA
\ and minimum wattage we want to detect

: is.on? ( -- boolean ) one.cycle 7 < 0= ;

: report 
	sync sync 
	mqtt.washer washer one.cycle value 
	mqtt.dryer dryer one.cycle value 
	;



