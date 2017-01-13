\ include timer.fs
hex

: lshift 0 do 2* loop ;
: rshift  0 do 2/ $7fff and loop ;
variable crc-sum
: crc.reset 0 crc-sum ! ;

: crc+ ( new byte -- ) 
	crc-sum @ xor
	dup 8 rshift swap 8 lshift or
	dup $ff00 and 4 lshift xor
	dup 8 rshift 4 rshift xor 
	dup $ff00 and 5 rshift xor
	crc-sum !
;

: d crc+ crc-sum @ . ;
: hex. dup hex . decimal ;
: bin. dup bin . decimal ;


  \ acc ^= b;
  \ acc = (acc >> 8) | (acc << 8);
  \ acc ^= (acc & 0xff00) << 4;
  \ acc ^= (acc >> 8) >> 4;
  \ acc ^= (acc & 0xff00) >> 5;
  \ return acc;

nvm

: emits ( depth -- ) 1 + dup 0 do dup i - 2 * sp@ + @ emit loop drop ;  
  \ reverses stack, emits
: cleanup ( depth -- ) 0 do drop loop ;
: send depth emits depth cleanup ;

\ only needed for testing if ESP is up and running
\ can ignore for simple sending / if STM8 controls it
\ can do something with ?RX word?  
: sync c0 
	01 00  00 00  89 02  00 00 
	0a e0 \ $e00a 
	c0 \ 0c emits 0c cleanup
	;

\ : mqtt-setup  c0 
	\ 0a 00  04 00  00 00  00 00 
	\ 04 00  6b 02  00 00  \ length, bytes
	\ 04 00  71 02  00 00  
	\ 04 00  77 02  00 00  
	\ 04 00  7d 02  00 00 
	\ dc 43 
	\ c0 \ 24 emits 24 cleanup
	\ ;

: message0 c0 
	0b 00  05 00  00 00  00 00 
	0b 00  2f 65 73 70 
           2d 6c 69 6e 6b 2f 31 00  
	01 00  30 00  00 00  \ data
	02 00  01 00  00 00  \ len of data  
	01 00  00 00  00 00  \ qos
	01 00  00 00  00 00  \ retain
	e5 ae 
	c0 \ 32 emits 32 cleanup
	;

\ : get-time c0 
	\ 07 00  00 00  00 00  00 00  
	\ 0e 9c  
	\ c0 \ 0c emits 0c cleanup
\ ;

ram

dec

