DIR = /../../matlib/matrix

all : Out.txt plot.svg

Out.txt: main.exe
	mono $< > $@

plot.svg : n3.gpi time.data start.par
	gnuplot < $<

start.par : #Setting initial parameters 
	>$@
	echo "A = 1 \nC = 0.1" >> $@

time.data: time.exe
	> $@
	for N in $$(seq 100 20 600); do \
		time --format "$$N %e" --output $@ --append \
		mono $< -M:$$N -N:$$N 1>out 2>errlog;\
	done
QRGS_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs QRGS.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs QRGS_and_Matrix.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

time.exe: timed.cs QRGS_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par
