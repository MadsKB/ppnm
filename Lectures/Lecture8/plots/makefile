default : Gamma.svg Error.svg

Gamma.svg : gamma.gpi gamma.data factorials.data
	gnuplot $<
gamma.data : main.exe
	mono $< > $@

Error.svg : error.gpi error.data errorFunc.data
	gnuplot $<
error.data : main_error.exe
	mono $< > $@

main_error.exe : main_error.cs sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -reference:,$(filter %.dll,$^))

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -reference:,$(filter %.dll,$^))

sfuns.dll : gamma.cs erf.cs
	mcs -target:library -out:$@ $^

factorials.data : makefile
	>$@
	echo 0 1 >> $@
	echo 1 1 >> $@
	echo 2 2 >> $@
	echo 3 6 >> $@
	echo 4 24 >> $@
	echo 5 120 >> $@
	echo 6 'echo '6*120' | bc' >> $@

errorFunc.data : makefile
	> $@ #Values taken from wikipedia, (didn't type all this in manually)
	echo 0 0 >> $@ 	
	echo 0.02 0.022564575 >> $@
	echo 0.04 0.045111106 >> $@
	echo 0.06 0.067621594 >> $@
	echo 0.08 0.090078126 >> $@
	echo 0.1 0.112462916 >> $@
	echo 0.2 0.222702589 >> $@
	echo 0.3 0.328626759 >> $@
	echo 0.4 0.428392355 >> $@
	echo 0.5 0.520499878 >> $@
	echo 0.6 0.603856091 >> $@
	echo 0.7 0.677801194 >> $@
	echo 0.8 0.742100965 >> $@
	echo 0.9 0.796908212 >> $@
	echo 1 0.842700793 >> $@
	echo 1.1 0.880205070 >> $@
	echo 1.2 0.910313978 >> $@
	echo 1.3 0.934007945 >> $@
	echo 1.4 0.952285120 >> $@
	echo 1.5 0.966105146 >> $@
	echo 1.6 0.976348383 >> $@
	echo 1.7 0.983790459 >> $@
	echo 1.8 0.989090502 >> $@
	echo 1.9 0.992790429 >> $@
	echo 2 0.995322265 >> $@
	echo 2.1 0.997020533 >> $@
	echo 2.2 0.998137154 >> $@
	echo 2.3 0.998856823 >> $@
	echo 2.4 0.999311486 >> $@
	echo 2.5 0.999593048 >> $@
	echo 3 0.999977910 >> $@
	echo 3.5 0.999999257 >> $@


main.exe: main.cs sfuns.dll
	mcs -target:exe -out:main.exe -reference:sfuns.dll main.cs

test:
	mcs --version
clean:
	$(RM) *.exe *.dll *.data *.svg
