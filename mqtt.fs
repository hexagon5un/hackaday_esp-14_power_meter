\ needs crc.fs, messages.fs
: slip $c0 emit ;

\ emits whole stack
: emits  0 begin 1+ swap >r depth 1  = until 
	begin 1 - r> emit dup 0= until drop 
;

\ appends crc, transmits as slip-escaped
: send crc slip emits slip ;
: sync sync send ;  \ overwrites to make easier

\ wraps up messages, sends them
\ example: sync washer on dryer off
: mqtt.washer
	mqtt.preamble washer.topic 
;
: mqtt.dryer
	mqtt.preamble dryer.topic 
;
: on
	message.on qos.and.retain send
;
: off
	message.off qos.and.retain send
;

: value ( n -- ) message.value qos.and.retain send ;

