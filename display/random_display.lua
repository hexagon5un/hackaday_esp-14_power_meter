ws2812b_pin  = 4  -- Pin D4 on my setup
num_LEDS=8

function init()
	for i=1,num_LEDS do
		red[i]=12;
		green[i]=12;
		blue[i]=12;
	end
end

function update(r,g,b)
	for i=1,num_LEDS-1 do
		red[num_LEDS-i+1]=red[num_LEDS-i]
		green[num_LEDS-i+1]=green[num_LEDS-i]
		blue[num_LEDS-i+1]=blue[num_LEDS-i]
	end
	red[1]=r
	blue[1]=b
	green[1]=g
end

function makestring()
	outstring = ""
	for i=1,num_LEDS do
		outstring = outstring .. string.char(red[i], green[i], blue[i])
	end
	return outstring
end

function randomstep(x)
	delta = math.random(11) - 6
	newx = math.max(0, math.min(255, x+delta))
	return newx
end

function display()
	update(randomstep(red[1]), randomstep(green[1]), randomstep(blue[1]))	
	ws2812.writergb(ws2812b_pin, makestring())
end

function display2()
	red[1] = randomstep(red[1])
	blue[1] = randomstep(blue[1])
	green[1] = randomstep(green[1])
	r = string.char(0,0,0) 
	s = string.char(red[1], green[1], blue[1])
	z = s
	z = z .. s
	z = z .. r
	z = z .. r
	-- r = s .. s
	ws2812.writergb(ws2812b_pin, z)
end

math.randomseed(1234)
-- Fancy temperature display! Blue-green-yellow-red fade.
red = {}
blue = {}
green = {}
init()
tmr.alarm(0, 1*200, tmr.ALARM_AUTO, display)

