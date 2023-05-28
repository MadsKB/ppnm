DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList
all : Out.txt Plot.svg

Out.txt: main.exe higgs.data 
	mono $< -fitOut:fitOut.data < higgs.data 1> $@ 2> log

Plot.svg : Plot.gpi higgs.data Out.txt
	gnuplot < $<


Plot.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set xlabel 'E' \n\
	set ylabel 'F' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'higgs.data' using (\$$1):(\$$2):(\$$3) index 0 with yerrorbars title 'rawData' \
	, 'fitOut.data' using (\$$1):(\$$2) index 0 with lines title 'qnewton fit' \
	, 'fitOut.data' using (\$$1):(\$$2) index 1 with lines title 'simplex fit'" >$@

higgs.data :
	echo "101 -0.25 2.0\n\
	103 -0.30 2.0\n\
	105 -0.15 1.9\n\
	107 -1.71 1.9\n\
	109  0.81 1.9\n\
	111  0.65 1.9\n\
	113 -0.91 1.9\n\
	115  0.91 1.9\n\
	117  0.96 1.6\n\
	119 -2.52 1.6\n\
	121 -1.01 1.6\n\
	123  2.01 1.6\n\
	125  4.83 1.6\n\
	127  4.58 1.6\n\
	129  1.26 1.3\n\
	131  1.01 1.3\n\
	133 -1.26 1.3\n\
	135  0.45 1.3\n\
	137  0.15 1.3\n\
	139 -0.91 1.3\n\
	141 -0.81 1.1\n\
	143 -1.41 1.1\n\
	145  1.36 1.1\n\
	147  0.50 1.1\n\
	149 -0.45 1.1\n\
	151  1.61 1.1\n\
	153 -2.21 1.1\n\
	155 -1.86 0.9\n\
	157  1.76 0.9\n\
	159 -0.50 0.9" > $@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(GENL)/genlist.cs mini.cs
	mcs -target:library -out:$@ $^

ODE.dll : ODE.cs General.dll
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
