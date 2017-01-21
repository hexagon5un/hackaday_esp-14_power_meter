dryers = dir(pattern="dryer_log_\\d{4}-\\d{2}-\\d{2}-\\d{6}$")

for (f in dryers){
	png(paste(f, ".png", sep=""), width=1024, height=800)
	tt <- read.csv(f, col.names=c("time", "power"))
	tt$time <- as.POSIXct(tt$time, origin="1970-01-01")
	plot(tt, type="b")
	dev.off()
}

washers = dir(pattern="washer_log_\\d{4}-\\d{2}-\\d{2}-\\d{6}$")

for (f in washers){
	png(paste(f, ".png", sep=""), width=1024, height=800)
	tt <- read.csv(f, col.names=c("time", "power"))
	tt$time <- as.POSIXct(tt$time, origin="1970-01-01")
	plot(tt, type="b")
	dev.off()
}

