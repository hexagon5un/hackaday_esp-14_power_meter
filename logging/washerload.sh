mosquitto_sub -t home/basement/washer -h 192.168.1.2 | \ 
# reads in the data from the washer, for instance
 ( while read f ;  
   do echo "0 k $f 3 / p " | dc | \ # compress range 0-300 into 0-100
	   mosquitto_pub -h 192.168.1.2 -t home/office/display -s; 
	   # published to another channel where I have a device listening
	   # that displays the value as blinky lights.
	   # the display is from 0-100 b/c it was a free-memory on my laptop monitor
	   # but whatever
   done )

# And that's all there is to it.
# It's so easy to build up loosely connected systems this way.


