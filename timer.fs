
variable times.up
: set.timer ( how many 5ms to wait -- ) tim + times.up ! ;
: timer? tim times.up @ = ;
: blocking.wait set.timer begin timer? until ;

\ : blink pa3.on 5 blocking.wait pa3.off 20 blocking.wait ;


