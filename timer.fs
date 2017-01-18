
\ workaround to store variables callable from flash
\ can only access with setter, getter
here dup
0 ,
: tset literal ! ;
: tget literal @ ;

: set.timer ( how many 5ms to wait -- ) tim + tset ;
: timer? tim tget = ;
: blocking.wait set.timer begin timer? until ;

\ : blink pa3.on 5 blocking.wait pa3.off 20 blocking.wait ;


