DIR = ../../matlib/matrix
MATLIB = ../../matlib
GENL = ../../Exercises/genericList

all : Bplot.svg Out.txt Cplot.svg

Bplot.svg : Bplot.gpi BPlot.data
	gnuplot < $<
Cplot.svg : Cplot.gpi CPlot.data
	gnuplot < $<

Out.txt: main.exe
	mono $< -A:1 > $@
BPlot.data : main.exe
	> $@
	for N in $$(seq 0.05 0.05 0.5); do \
		mono $< -rmin:$$N >> $@;\
	done
	echo "\n" >> $@
	for N in $$(seq 8 0.5 20); do \
		mono $< -rmax:$$N >> $@;\
	done
	echo "\n" >> $@	
	for N in $$(seq 0.01 0.01 0.09); do \
		mono $< -acc:$$N >> $@;\
	done
	echo "\n" >>$@	
	for N in $$(seq 0.01 0.01 0.1); do \
		mono $< -eps:$$N >> $@;\
	done

CPlot.data : main.exe
	> $@
	for N in $$(seq 0.05 0.05 0.5); do \
		mono $< -rmin:$$N -C:1 >> $@;\
	done
	echo "\n" >> $@
	for N in $$(seq 5 0.5 20); do \
		mono $< -rmax:$$N -C:1 >> $@;\
	done
	echo "\n" >> $@	
	for N in $$(seq 0.01 0.01 0.1); do \
		mono $< -acc:$$N -C:1 >> $@;\
	done
	echo "\n" >>$@	
	for N in $$(seq 0.01 0.01 0.1); do \
		mono $< -eps:$$N -C:1 >> $@;\
	done

Bplot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set multiplot layout 2,2 \n\
	set xlabel 'rmin' \n\
	set ylabel 'E0+0.5' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	plot 'BPlot.data' using (\$$1):(\$$5+0.5) index 0 with points pointtype 1 title 'rmin convergence' \n\
	set xlabel 'rmax' \n\
	plot 'BPlot.data' using (\$$2):(\$$5+0.5) index 1 with points pointtype 1 title 'rmax convergence' \n\
	set xlabel 'acc' \n\
	plot 'BPlot.data' using (\$$3):(\$$5+0.5) index 2 with points pointtype 1 title 'acc convergence' \n\
	set xlabel 'eps' \n\
	plot 'BPlot.data' using (\$$4):(\$$5+0.5) index 3 with points pointtype 1 title 'eps convergence' \n\
	" >$@


Cplot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set multiplot layout 2,2 \n\
	set xlabel 'rmin' \n\
	set ylabel 'E0+0.5' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	plot 'CPlot.data' using (\$$1):(\$$5+0.5) index 0 with points pointtype 1 title 'rmin convergence' \n\
	set xlabel 'rmax' \n\
	plot [8:20] 'CPlot.data' using (\$$2):(\$$5+0.5) index 1 with points pointtype 1 title 'rmax convergence' \n\
	set xlabel 'acc' \n\
	plot 'CPlot.data' using (\$$3):(\$$5+0.5) index 2 with points pointtype 1 title 'acc convergence' \n\
	set xlabel 'eps' \n\
	plot 'CPlot.data' using (\$$4):(\$$5+0.5) index 3 with points pointtype 1 title 'eps convergence' \n\
	" >$@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(MATLIB)/lineq/QRGS.cs roots.cs vectorNorm.cs $(MATLIB)/ODE/ODE.cs $(MATLIB)/genlist/genlist.cs 
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
