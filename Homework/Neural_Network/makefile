DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList
MATLIB = ../../matlib

all : plot.svg

Out.txt : main.exe
	mono $< > $@

Aplot.svg : Aplot.gpi plot.data
	gnuplot < $<
plot.svg : plot.gpi Out.txt
	gnuplot < $<


plot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'Out.txt' using (\$$1):(\$$3) index 0 with lines title 'ActualFunction' \
	, 'Out.txt' using (\$$1):(\$$4) index 0 with lines title 'Network before training' \
	, 'Out.txt' using (\$$1):(\$$2) index 0 with lines title 'Network after training' \
	, 'Out.txt' using (\$$1):(\$$5) index 0 with lines title 'Prime of function' \
	, 'Out.txt' using (\$$1):(\$$6) index 0 with lines title 'Network Prime' \
	, 'Out.txt' using (\$$1):(\$$7) index 0 with lines title 'Network Integral'" >$@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(GENL)/genlist.cs $(MATLIB)/mini.cs ANN.cs
	mcs -target:library -out:$@ $^

ODE.dll : ODE.cs General.dll
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
