DIR = $(HOME)/repos/ppnm/matlib/matrix

All : Out.txt Plot.svg

Out.txt : main.exe
	mono $< -x:1,2,3,4,6,9,10,13,15 -y:117,100,88,72,53,29.5,25.2,15.2,11.1 -dy:5,5,5,4,4,3,3,2,2 -fitOut:False -start:0 -end:16 > $@
Plot.svg : plot.gpi fit.data raw.data
	gnuplot $< > $@
fit.data : main.exe
	mono $< -x:1,2,3,4,6,9,10,13,15 -y:117,100,88,72,53,29.5,25.2,15.2,11.1 -dy:5,5,5,4,4,3,3,2,2 -fitOut:True -start:0 -end:16 > $@
raw.data :
	>$@
	echo "1 117 5">>$@
	echo "2 100 5">>$@
	echo "3 88 5">>$@
	echo "4 72 4">>$@
	echo "6 53 4">>$@
	echo "9 29.5 3">>$@
	echo "10 25.2 3">>$@
	echo "13 15.2 2">>$@
	echo "15 11.1 2">>$@

LeastSquares.dll : $(DIR)/matrix.cs $(DIR)/vector.cs Least-squares.cs QRGS.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs LeastSquares.dll
	mcs -target:exe -out:$@ $(addprefix -reference:,$(filter %.dll,$^)) $<
clean:
	$(RM) *.exe *.dll *log [Oo]ut.txt *.data *.svg
