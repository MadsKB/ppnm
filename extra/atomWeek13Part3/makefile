DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList

all : Plot.svg

Plot.svg : Plot.gpi plot.data
	gnuplot < $<

Plot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'd/wait' \n\
	set ylabel 'p22' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (\$$1):(\$$2) index 1 with points pointtype 1 title 'd variable, wait=1' \
	, 'plot.data' using (\$$1):(\$$2) index 2 with points pointtype 1 title 'wait = variable, d =1'" >$@

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
