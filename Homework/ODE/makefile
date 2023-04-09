DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList

all : Aplot.svg Bplot.svg

Aplot.svg : Aplot.gpi plot.data
	gnuplot < $<
Bplot.svg : Bplot.gpi plot.data
	gnuplot < $<


Bplot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 1 with lines title 'i' \
	, 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 2 with lines title 'ii' \
	, 'plot.data' using (1/\$$2)*cos(\$$1):(1/\$$2)*sin(\$$1) index 3 with lines title 'iii'" >$@

Aplot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'solution to y\" = -y" >$@

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
