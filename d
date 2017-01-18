\ include timer.fs
\ include power_meter.fs



nvm
include misc.fs 
include crc.fs
include messages.fs
include mqtt.fs
ram

\ savepoint: stm8flash -c stlinkv2 -p stm8s003f3 -r base-115200-mqtt.ihx



