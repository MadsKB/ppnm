Gamma.svg : gamma.gpi gamma.data factorials.data
	gnuplot $<
gamma.data : main.exe
	mono $< > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -refrerence: $(filter %.dll,$^))

sfuns.dll : gamma.cs erf.cs
	mcs -target:library -out:$@ $<

factorials.data : makefile
	>$@
	echo 0 1 >> $@
	echo 1 1 >> $@
	echo 2 2 >> $@
	echo 3 6 >> $@
	echo 4 24 >> $@
	echo 5 120 >> $@
	echo 6 'echo '6*120' | bc' >> $@

main.exe: main.cs sfuns.dll
	mcs -target:exe -out:main.exe -reference:sfuns.dll main.cs

test:
	mcs --version
clean:
	rm --force main.exe Out.txt sfuns.dll
