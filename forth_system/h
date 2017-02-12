
\ from clean start, ESP-14 image
\ need to set esp-link to 9600 baud

nvm
\ include gpio_core.fs -- removed for the moment. requires too many defs.  need to trim.
include baud.fs
include misc.fs
include timer.fs

\ : init 115200_baud cr ." howdy!" cr pa3.init ; 
\ removed PA3 output for now.  will work back in
: init 115200_baud cr ." Power Meter!" cr ; 
' init 'boot !

ram
\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200.ihx
\ from now, connect at 115,200 baud

nvm
include crc.fs
include messages.fs
include mqtt.fs
ram

\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200-mqtt.ihx

nvm
include power_meter.fs
ram

\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200-mqtt-power.ihx

