DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList

all : Aplot.svg Bplot.svg Cplot.svg

Aplot.svg : Aplot.gpi plot.data
	gnuplot < $<
Bplot.svg : Bplot.gpi plot.data
	gnuplot < $<
Cplot.svg : Cplot.gpi plot.data
	gnuplot < $<


Bplot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set title 'Motion of a planet around a star in GR' \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 2 with lines title 'i' \
	, 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 3 with lines title 'ii' \
	, 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 4 with lines title 'iii'" >$@

Aplot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 't' \n\
	set ylabel '' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title '3. solution to y\" = -y' \
	, 'plot.data' using (\$$1):(\$$2) index 1 with points pointtype 1 title '4. theta(t)' \
	, 'plot.data' using (\$$1):(\$$3) index 1 with points pointtype 1 title '4. omega(t)'" >$@

Cplot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set xrange [-1.5:1.5] \n\
	set yrange [-1.5:1.5] \n\
	set samples 1600 \n\
	plot 'plot.data' using (\$$2):(\$$3) index 5 with points title 'body 1 trace' \
	, 'plot.data' using (\$$6):(\$$7) index 5 with points title 'body 2 trace' \
	, 'plot.data' using (\$$10):(\$$11) index 5 with points title 'body 3 trace'" >$@
plot.data : main.exe
	mono $< > $@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(GENL)/genlist.cs ODE.cs
	mcs -target:library -out:$@ $^

ODE.dll : ODE.cs General.dll
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
