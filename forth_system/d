
\ Adds boot-time delay to opt out of endless reportloop

nvm

: reportloop begin report 2000 blocking.wait again ;

: init-timeout 
	." Entering endless report loop in 10 sec." cr
	." Press any key to escape." cr 
	-1 
	2000 tim + 
	begin 
	?key if drop drop drop 0 ." Aborting..." exit then
	dup tim = until drop ;

: init2 init init-timeout if ." GO!" cr reportloop then ; 

' init2 'boot !

ram




