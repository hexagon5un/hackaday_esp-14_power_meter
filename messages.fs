
hex
\ nvm

\ only needed for testing if ESP is up and running
\ can ignore for simple sending / if STM8 controls it
\ can do something with ?RX word?  
: sync \ c0 
	01 00  00 00  89 02  00 00 
	\ 0a e0 \ $e00a 
	\ c0
	;

: message0 \ c0 
	0b 00  05 00  00 00  00 00 \ 5 arguments, no callback
	0b 00  2f 65 73 70 
           2d 6c 69 6e 6b 2f 31 00  
	01 00  30 00  00 00  \ data
	02 00  01 00  00 00  \ len of data  
	01 00  00 00  00 00  \ qos
	01 00  00 00  00 00  \ retain
	\ e5 ae \ $aee5 is checksum
	\ c0 
	\ 32 emits 32 cleanup
	;

: m0
	0b 00 
	2f 65 73 70 
	2d 6c 69 6e 
	6b 2f 31 00  
	;

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

ram
dec

\ : mqtt-setup  c0 
	\ 0a 00  04 00  00 00  00 00 
	\ 04 00  6b 02  00 00  \ length, bytes
	\ 04 00  71 02  00 00  
	\ 04 00  77 02  00 00  
	\ 04 00  7d 02  00 00 
	\ dc 43 
	\ c0 \ 24 emits 24 cleanup
	\ ;

\ : get-time  
	\ 07 00  00 00  00 00  00 00  
	\ 0e 9c  
	\ c0 \ 0c emits 0c cleanup
\ ;

