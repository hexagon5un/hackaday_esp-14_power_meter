reset

nvm
include gpio_core.fs
include misc.fs

: init 57600_baud cr ." howdy!" cr pa3.init ; 
' init 'boot !

ram
\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base.ihx


include d


