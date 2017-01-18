
hex

\ only needed for testing if ESP is up and running
\ can ignore for simple sending / if STM8 controls it
\ can do something with ?RX word?  

: sync 01 00  00 00  89 02  00 00 ;

: mqtt.preamble 
	0b 00  05 00  00 00  00 00 \ 5 arguments, no callback 
;

\ 00000000: 0a68 6f6d 652f 6261 7365 6d65 6e74 2f77  .home/basement/w
\ 00000010: 6173 6865 720a 686f 6d65 2f62 6173 656d  asher.home/basem
\ 00000020: 656e 742f 6472 7965 720a 0a              ent/dryer..

: washer.topic 
	14 00 
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
	79 65 72 00
;

\ think about extending this to send arbitrary data...

: message.on 
	02 00  6f 6e  00 00  \ data
	02 00  02 00  00 00  \ len of data  
;

: message.off 
	03 00  6f 66  66 00  \ data
	02 00  03 00  00 00  \ len of data  
;

: qos.and.retain 
	01 00  00 00  00 00  \ qos
	01 00  00 00  00 00  \ retain
;

dec


