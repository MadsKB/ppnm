DIR = $(HOME)/repos/ppnm/mathlib/matrix

Out.txt: main.exe
	mono $< > $@

QRGS_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs QRGS.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs QRGS_and_Matrix.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^))
clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt
