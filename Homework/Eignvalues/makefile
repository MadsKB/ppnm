DIR = $(HOME)/repos/ppnm/matlib/matrix



Out.txt: main.exe
	mono $< > $@

jacobi_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs jacobian.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs jacobi_and_Matrix.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt *.data *.svg
