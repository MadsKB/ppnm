DIR = $(HOME)/repos/ppnm/matlib/matrix


default : Convergence.svg waveplot.svg Out.txt

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

main.exe: main.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

HSym.exe: hydrogenSym.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<
clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt *.data 
