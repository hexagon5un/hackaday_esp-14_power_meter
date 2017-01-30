\ needs crc.fs, messages.fs
\ Makes a slip-encoded, CRC'ed MQTT publish request
\ Basic method is to stack up everything all the bytes 
\  and then loop through them from depth of stack to top

\ example usage:  should start w/ sync just to be sure
\       sync  
\	mqtt.washer on 
\ 	mqtt.dryer 17 value  



: slip $c0 emit ;

\ emits whole stack, leaves it clear
\ CRC and SLIP bytes should be included at this point
: emits ( many bytes -- ) 
	\ push every byte onto return stack, keep count
	0 begin 1+ swap >r depth 1  = until 
	\ emit every byte from return stack, keep count
	begin 1 - r> emit dup 0= until drop 
;

\ appends crc, transmits as slip-escaped
: send ( many bytes -- ) crc slip emits slip ;
: sync sync send ;  \ overwrites!

\ wraps up messages, sends them
\ a complete message would look like:
\ preamble topic message qos retain send
\ this is wrapped up into, e.g.
\ mqtt.washer on qos.and.retain send

\ Note: this may be lousy structure.  What's wrong with
\       preamble topic message qos retain send?

: mqtt.washer ( -- preamble and topic on stack )
	mqtt.preamble washer.topic 
;
: mqtt.dryer ( -- preamble and topic on stack )
	mqtt.preamble dryer.topic 
;
: on ( --  "on"-message )
	message.on qos.and.retain send
;
: off ( --  "off"-message )
	message.off qos.and.retain send
;

: value ( preamble topic n -- )
	message.value qos.and.retain send ;

