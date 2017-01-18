
\ from clean start, ESP-14 image
\ need to set esp-link to 9600 baud

nvm
include gpio_core.fs
include baud.fs
include misc.fs

: init 115200_baud cr ." howdy!" cr pa3.init ; 
' init 'boot !

ram
\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200.ihx
\ from now, connect at 115,200 baud

nvm
include crc.fs
include messages.fs
include mqtt.fs
ram

\ include timer.fs
\ include power_meter.fs

\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200-mqtt.ihx


