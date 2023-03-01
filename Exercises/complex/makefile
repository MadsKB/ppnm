DIR = $(HOME)/repos/ppnm/matlib/complex


Out.txt: main.exe
	mono $< > $@

main.exe : main.cs cmath.dll
	mcs $(addprefix -reference:,$(filter %.dll,$^)) $<

cmath.dll : $(DIR)/cmath.cs $(DIR)/complex.cs
	mcs -target:library -out:./cmath.dll $^

clean :
	$(RM) *.exe *.dll [Oo]ut* log*
