mqtt_address = "192.168.1.2"
-- mqtt_address = "raspberrypi"
washer_topic   = "home/basement/washer"
dryer_topic   = "home/basement/dryer"
debug_topic = "home/washer-dryer-display/debug"
command_topic = "home/washer-dryer-display/command"

washer_power = {} 
dryer_power =  {}
num_observations = 16
for i=1,num_observations do
	washer_power[i] = 0
	dryer_power[i] = 0
end
framebuffer = {0,0,0,0,0,0}


ws2812b_pin  = 4  -- Pin D4 on my setup

-- Set up named client with 60 sec keepalive, 
-- no username/password, 
-- and a clean session each time
m = mqtt.Client("washer-dryer-display", 60, "", "", 1) 
m:on("offline", function() print("mqtt offline");  end)

-- subscribe as soon as connected
m:on("connect", function() print("mqtt connected") subscribe_topics(m) end )

function subscribe_topics(client)
	client:subscribe(washer_topic,   0, subscribed(washer_topic))
	client:subscribe(dryer_topic,   0, subscribed(dryer_topic))
	client:subscribe(command_topic,   0, subscribed(command_topic))
end

function subscribed(topic)
	debug_info("subscribed to " .. topic)
end

function debug_info(message)
	m:publish(debug_topic, message, 0, 0) 
	-- print(message)
end

function debug_array(a)
	local s = ""
	for k,v in ipairs(a) do s=s..v end
	print(s)
end

-- Deal with incoming messages
m:on("message", function(client, topic, data) handle_message(client, topic, data) end)

function printHelp()
	debug_info("restart: restarts node")
	debug_info("ping: pong")
	debug_info("flash: lights up red, green")
	debug_info("off: all lights off")
end

-- Print out all data & display temperature data 
function handle_message(client, topic, data) 
	if data == "help" then printHelp() end
	if topic == command_topic then
		if data == "restart" then node.restart() end		
		if data == "ping" then debug_info("pong") end		
		if data == "flash" then display_flash() end		
		if data == "off" then display_off() end		
	end
	if topic == washer_topic then
		update(data, washer_power)
	end
	if topic == dryer_topic then
		update(data, dryer_power)
	end
end

function update(data, power_array)
	for i = num_observations, 2, -1 do
		power_array[i] = power_array[i-1]
	end
	power_array[1] = tonumber(data)

	-- hook something in here.  need states, machine.  blink functions.
	-- framebuffer[1] = mmax(washer_power)
	-- framebuffer[5] = maverage(dryer_power)
end


function mmax(data)
	local zz = 0
	for i = 1, #data do
		if data[i] > zz then zz=data[i] end
	end
	return zz
end

function maverage(data) 
	local a = 0
	for i = 1, #data do
		a = a + data[i]
	end
	return a / #data
end


-- local washer = mmax(washer_power)
-- local dryer = maverage(dryer_power)
function display()
	blink1()
	blink2()
	local s = ""
	for k,v in ipairs(framebuffer) do
		s = s .. string.char(v)
	end
	ws2812.writergb(ws2812b_pin, s)
end
tmr.alarm(0, 1*500, tmr.ALARM_AUTO, display)

blink2_count = 0
blink1_count = 0

function blink2()
	if blink2_count > 0 then
		blink2_count = blink2_count - 1
		if framebuffer[5] > 0 then
			framebuffer[5]=0
		else
			framebuffer[5]=50
		end
	else
		framebuffer[5]=0
	end
end

function blink1()
	if blink1_count > 0 then
		blink1_count = blink1_count - 1
		if framebuffer[1] > 0 then
			framebuffer[1]=0
		else
			framebuffer[1]=50
		end
	else
		framebuffer[1]=0
	end
end


function display_flash()
	ws2812.writergb(ws2812b_pin, string.char(255, 0, 0) .. string.char(0,255,0))
end
function display_off()
	ws2812.writergb(ws2812b_pin, string.char(0, 0, 0) .. string.char(0,0,0))
end

m:connect(mqtt_address, 1883, 0, 1) 

