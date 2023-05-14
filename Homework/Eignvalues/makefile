DIR = $(HOME)/repos/ppnm/matlib/matrix

default : Convergence.svg waveplot.svg Out.txt Cplot.svg

Cplot.svg : Cplot.gpi Cplot.data start.par
	gnuplot < $<

start.par : #Setting initial parameters (part C fit)
	>$@
	echo "A = 1 \n C=0.1" >> $@

Cplot.data : CTimed.exe
	> $@
	for N in $$(seq 3 1 300); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -n:$$N ;\
	done
	echo "\n" >> $@;
	for N in $$(seq 3 1 300); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -n:$$N -opt:True ;\
	done

waveplot.svg : waveplot.gpi wave.data
	gnuplot < $<

Convergence.svg : rmaxConvergence.gpi rmaxConv.data drConv.data 
	gnuplot < $<

rmaxConv.data : HSym.exe
	> $@
	for N in $$(seq 0.2 0.2 20); do \
		mono $< -rmax:$$N -dr:0.1 >> $@;\
	done	

drConv.data : HSym.exe
	> $@
	for N in $$(seq 0.01 0.01 1); do \
		mono $< -rmax:5 -dr:$$N >> $@;\
	done	


wave.data: HSym.exe
	mono $< -dr:0.1 -rmax:16 -waveOut:True -sol:0,1 >$@
Out.txt: main.exe
	mono $< > $@

jacobi_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs jacobian.cs
	mcs -target:library -out:$@ $^


CTimed.exe: CTimed.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

main.exe: main.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

HSym.exe: hydrogenSym.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<
clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt *.data 
