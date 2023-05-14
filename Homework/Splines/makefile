DIR = ../../matlib/matrix

all : Aplot.svg Cplot.svg BOut.txt

BOut.txt: main.exe
	mono $< > $@
Cplot.svg : Cplot.gpi plot.data
	gnuplot < $<

Aplot.svg : Aplot.gpi plot.data
	gnuplot < $<

plot.data : datagen.exe
	mono $< > $@

Spline_and_Matrix.dll : $(DIR)/matrix.cs $(DIR)/vector.cs spline.cs
	mcs -target:library -out:$@ $^

datagen.exe : datagen.cs Spline_and_Matrix.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<

main.exe: main.cs Spline_and_Matrix.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par
