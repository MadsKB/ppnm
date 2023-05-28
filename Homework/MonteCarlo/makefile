DIR = ../../matlib/matrix
GENL = ../../Exercises/genericList
Out.txt : main.exe ErrScaling.svg
	mono $< >$@

ErrScaling.svg : ErrScaling.gpi plot.data start.par
	gnuplot < $<
start.par :
	echo "A = 1\nC = 1.0" > $@

ErrScaling.gpi :
	echo "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key top right \n\
	set xlabel 'N' \n\
	set ylabel 'error' \n\
	set title 'Error scaling of integral cos(x)*sin(y) with limits 0 to pi/2 for both x and y' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	n(x) = A/sqrt(x)+C \n\
	fit n(x) 'plot.data' using (\$$1):(\$$3) via 'start.par' \n\
	plot 'plot.data' using (\$$1):(\$$3) index 0 with points title 'Actual Error of plainmc'\
	, 'plot.data' using (\$$1):(\$$2) index 0 with points title 'Estimated Error of plainmc'\
	, 'plot.data' using (\$$1):(\$$4) index 0 with points title 'Estimated Error of quasimc'\
	, 'plot.data' using (\$$1):(\$$5) index 0 with points title 'Actual Error of quasimc'\
	, n(x) t 'Fit of actual plainmc error to A/sqrt(N)+C'" >$@

plot.data : main.exe
	mono $< -plot:true > $@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(GENL)/genlist.cs monteCarlo.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
