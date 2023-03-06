DIR = $(HOME)/repos/ppnm/matlib/matrix



Out.txt: main.exe
	mono $< > $@
time.data: time.exe
	> $@
	time -o $@ -f %U mono $< -N:4 -M:4
QRGS_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs QRGS.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs QRGS_and_Matrix.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

time.exe: timed.cs QRGS_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt *.data
