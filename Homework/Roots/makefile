DIR = ../../matlib/matrix
MATLIB = ../../matlib
GENL = ../../Exercises/genericList

all : Out.txt BConvPlot.svg CConvPlot.svg BWavePlot.svg

BConvPlot.svg : BConvPlot.gpi BConvPlot.data
	gnuplot < $<
CConvPlot.svg : CConvPlot.gpi CConvPlot.data
	gnuplot < $<
BWavePlot.svg : BWavePlot.gpi BWavePlot.data
	gnuplot < $<




Out.txt: main.exe
	echo "Part A): \n Analytically found root of Rosenbrock's vally function: (1,1), numerically found root with starting guess (0,0):" > $@
	mono $< -A:1 >> $@
	echo "Part C): " >> $@
	echo "Ground state energy (Analytical value = -0.5)" >> $@
	mono $< -C:1 -E0:-1 -rmax:20 -conv:0 >> $@
	echo "1st excited state energy (Analytical value = -0.5/4 = -0.125)" >> $@
	mono $< -C:1 -E0:-0.2 -rmax:20 -conv:0 >> $@
	echo "2nd excited state energy (Analytical value = -0.5/9 = -1/18 = -0.055)" >> $@
	mono $< -C:1 -E0:-0.07 -rmax:50 -conv:0 >> $@
BWavePlot.data : main.exe
	mono $< -plot:1 -E0:-1 > $@

BConvPlot.data : main.exe
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

CConvPlot.data : main.exe
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


BWavePlot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key top right \n\
	set xlabel 'r' \n\
	set ylabel 'f' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Ground state (Our solution)' \
	, (x*exp(-x)) with lines linetype 2 title 'Ground state (Analytical solution)' \
	" >$@

BConvPlot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set multiplot layout 2,2 \n\
	set xlabel 'rmin' \n\
	set ylabel 'E0+0.5' \n\
	set xtics rotate by -45 \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$1):(\$$5+0.5) index 0 with points pointtype 1 title 'rmin convergence' \n\
	set xlabel 'rmax' \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$2):(\$$5+0.5) index 1 with points pointtype 1 title 'rmax convergence' \n\
	set xlabel 'acc' \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$3):(\$$5+0.5) index 2 with points pointtype 1 title 'acc convergence' \n\
	set xlabel 'eps' \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$4):(\$$5+0.5) index 3 with points pointtype 1 title 'eps convergence' \n\
	" >$@


CConvPlot.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set multiplot layout 2,2 \n\
	set xlabel 'rmin' \n\
	set ylabel 'E0+0.5' \n\
	set xtics rotate by -45 \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$1):(\$$5+0.5) index 0 with points pointtype 1 title 'rmin convergence' \n\
	set xlabel 'rmax' \n\
	plot [8:20] '$(patsubst %.gpi,%.data, $@)' using (\$$2):(\$$5+0.5) index 1 with points pointtype 1 title 'rmax convergence' \n\
	set xlabel 'acc' \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$3):(\$$5+0.5) index 2 with points pointtype 1 title 'acc convergence' \n\
	set xlabel 'eps' \n\
	plot '$(patsubst %.gpi,%.data, $@)' using (\$$4):(\$$5+0.5) index 3 with points pointtype 1 title 'eps convergence' \n\
	" >$@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(MATLIB)/lineq/QRGS.cs roots.cs vectorNorm.cs $(MATLIB)/ODE/ODE.cs $(MATLIB)/genlist/genlist.cs 
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
