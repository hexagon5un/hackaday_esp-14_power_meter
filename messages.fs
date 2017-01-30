\ These data are most obviously entered in hexadecimal
hex

\ Command to sync up the esp-link 
: sync 01 00  00 00  89 02  00 00 ;

\ esp-link's MQTT send command preamble
: mqtt.preamble 
	0b 00  05 00  00 00  00 00 \ 5 arguments, no callback 
;

\ The reset of the command's arguments are: 
\  topic, message, qos, and retain flag

\ All arguments start with (16-bit, LSB first) length of argument
\  and then follow with the argument itself, padded out to 4 bytes

\ 00000000: 0a68 6f6d 652f 6261 7365 6d65 6e74 2f77  .home/basement/w
\ 00000010: 6173 6865 720a 686f 6d65 2f62 6173 656d  asher.home/basem
\ 00000020: 656e 742f 6472 7965 720a 0a              ent/dryer..

: washer.topic 
	14 00  \ length
	68 6f 6d 65 
	2f 62 61 73 
	65 6d 65 6e 
	74 2f 77 61 
	73 68 65 72
;

: dryer.topic
	13 00 
	68 6f 6d 65 
	2f 62 61 73
	65 6d 65 6e 
	74 2f 64 72 
	79 65 72 00 \ 0-padded to $14 bytes
;

\ think about extending this to send arbitrary data...

: message.on 
	02 00  6f 6e  00 00  \ "on"
	02 00  02 00  00 00  \ len of data  
;

: message.off 
	03 00  6f 66  66 00  \ "off"
	02 00  03 00  00 00  \ len of data  
;

: qos.and.retain 
	01 00  00 00  00 00  \ qos = 0
	01 00  00 00  00 00  \ retain = 0
;

decimal

: toascii 10 /mod 10 /mod \ base-ten remainders: 123 -> 3 2 1
  48 + rot 48 + rot 48  + swap ; \ reverses string, adds ascii 0
hex

: message.value \ build up a 3-digit number
        >r 
	03 00  r> toascii 00  \ data
	02 00  03 00   00 00  \ len of data  
;




