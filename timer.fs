
\ workaround to store variables callable from flash
\ can only access with setter, getter
\ not sure what happens to ram pointer after cold reset.  
\   this solution scares me b/c it may get overwritten or overwrite.
\ works as long as no functions stored in RAM
\ should initialize this on boot rather than here so that
\   it happens every time.
here dup
0 ,
: tset literal ! ;
: tget literal @ ;

: set.timer ( how many 5ms to wait -- ) tim + tset ;
: timer? tim tget = ;
: blocking.wait set.timer begin timer? until ;



