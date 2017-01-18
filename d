include timer.fs
include power_meter.fs

\ default -- have to pick a channel
\ washer is.on? .

: report sync washer is.on? if mqtt.washer on else mqtt.washer off then
                dryer is.on? if mqtt.dryer on else mqtt.dryer off then ;







