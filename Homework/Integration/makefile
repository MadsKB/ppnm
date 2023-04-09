DIR = ../../matlib/matrix

default : Out.txt Errplot.svg 

Out.txt: main.exe
	mono $< > $@

Errplot.svg : Errplot.gpi plot.data
	gnuplot < $<

Errplot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'x' \n\
	set ylabel 'Absolute difference compared to tabulated values (Wiki)' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'plot.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Approx from plot exercise' \
	, 'plot.data' using (\$$1):(\$$3) index 0 with points pointtype 2 title 'Our function'" >$@

plot.data : erf.exe
	mono $< > $@	

Lib.dll : $(DIR)/matrix.cs $(DIR)/vector.cs integrate.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs Lib.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

erf.exe: erf.cs Lib.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<


clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
