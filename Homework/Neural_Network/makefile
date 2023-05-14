DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList
MATLIB = ../../matlib

all : ABplot.svg Cplot.svg

Out.txt : main.exe
	mono $< > $@

ABplot.svg : ABplot.gpi Out.txt
	gnuplot < $<
Cplot.svg : Cplot.gpi Out.txt
	gnuplot < $<

ABplot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set title 'Interpulation network evaluated on Cos(5x-1)Exp(-x^2)' \n\
	set multiplot layout 2,2 \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'Out.txt' using (\$$1):(\$$3) index 0 with lines title 'Actual Function' \
	, 'Out.txt' using (\$$1):(\$$4) index 0 with lines title 'Network before training' \
	, 'Out.txt' using (\$$1):(\$$2) index 0 with lines title 'Network after training' \n\
	set ylabel 'y'' \n\
	set title \n\
	plot 'Out.txt' using (\$$1):(\$$5) index 0 with lines title 'Analytical result' \
	, 'Out.txt' using (\$$1):(\$$6) index 0 with lines title 'Network result' \n\
	set ylabel 'y″' \n\
	plot 'Out.txt' using (\$$1):(\$$10) index 0 with lines title 'Analytical result' \
	, 'Out.txt' using (\$$1):(\$$9) index 0 with lines title 'Network result' \n\
	set ylabel '∫ydx (From 0 to x)' \n\
	plot 'Out.txt' using (\$$1):(\$$7) index 0 with lines title 'Network result' \
	, 'Out.txt' using (\$$1):(\$$8) index 0 with lines title 'Numerical result' \
	" >$@
#need to find a way to do double prime
Cplot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'y' \n\
	set title 'DE solver evaluated on 0 =2y + y″ with y(0) = 1, yʹ(0) = 0' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'Out.txt' using (\$$1):(\$$3) index 1 with lines title 'Analytical solution' \
	, 'Out.txt' using (\$$1):(\$$2) index 1 with lines title 'Network solution'" >$@
General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(GENL)/genlist.cs $(MATLIB)/mini.cs $(MATLIB)/integrate.cs ANN.cs
	mcs -target:library -out:$@ $^

ODE.dll : ODE.cs General.dll
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
