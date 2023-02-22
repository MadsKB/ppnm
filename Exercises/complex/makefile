DIR = $(HOME)/repos/ppnm/complex


Out.txt: main.exe
	mono $< > $@

main.exe : main.cs cmath.dll
	mcs -reference:$> $<

cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^
